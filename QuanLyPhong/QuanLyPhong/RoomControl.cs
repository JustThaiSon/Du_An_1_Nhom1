using BUS.IService;
using BUS.Service;
using BUS.ViewModels;
using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhong
{
    public partial class RoomControl : UserControl
    {
        public RoomViewModels Room { get; set; }
        private IRoomService roomService;
        public RoomControl()
        {
            InitializeComponent();
            InitializeContextMenu();
            roomService = new RoomService();
        }

        private void InitializeContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            var bookRoomMenuItem = new ToolStripMenuItem("Booking room");
            var moveRoomMenuItem = new ToolStripMenuItem("Changing the room");
            var viewBillMenuItem = new ToolStripMenuItem("See the bill");
            var cleanRoomMenuItem = new ToolStripMenuItem("Clean Room");
            // Add event handlers for menu items
            bookRoomMenuItem.Click += bookRoomMenuItem_Click;
            moveRoomMenuItem.Click += moveRoomMenuItem_Click;
            viewBillMenuItem.Click += viewBillMenuItem_Click;
            cleanRoomMenuItem.Click += cleanRoomMenuItem_Click;


			contextMenu.Items.AddRange(new ToolStripItem[] { bookRoomMenuItem, moveRoomMenuItem, viewBillMenuItem, cleanRoomMenuItem });
			ptRoom.ContextMenuStrip = contextMenu;
        }

		private void cleanRoomMenuItem_Click(object? sender, EventArgs e)
		{
			if (Room.Status != RoomStatus.CleanUp)
			{
				MessageBox.Show($"Room  {Room.RoomName} is not in a clean state!");
				return;
			}
			var result = MessageBox.Show($"Are you sure that room '{Room.RoomName}' is finished cleaning?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				if (Room.Status == RoomStatus.CleanUp)
				{
					Room.Status = RoomStatus.Available;
					roomService.UpdateRoom(Room);
					MessageBox.Show($"Room  {Room.RoomName} cleanup complete!");
					ptRoom.Image = Properties.Resources.Available;
				}
			}
		}

		private void bookRoomMenuItem_Click(object? sender, EventArgs e)
        {
            if (Room.Status == RoomStatus.UnAvailable)
            {
                MessageBox.Show("Room Booked " + Room.RoomName);
                return;
            }
            if (Room.Status == RoomStatus.CleanUp)
            {
                MessageBox.Show("Room is being cleaned " + Room.RoomName);
                return;
            }
           
            frmRoomBookingReceipt frm = new frmRoomBookingReceipt(Room.Id);
            frm.OnBookRoom += BookRoom;
            frm.Show();
        }

        private void moveRoomMenuItem_Click(object? sender, EventArgs e)
        {
            if (Room.Status == RoomStatus.Available)
            {
                MessageBox.Show(Room.RoomName + " Not yet set");
                return;
            }
            if (Room.Status == RoomStatus.CleanUp)
            {
                MessageBox.Show(Room.RoomName + " Cleaning up");
                return;
            }
            frmTransfer frm = new frmTransfer(Room.Id);
			frm.OnTransferCompleted += HandleTransferCompleted;
			frm.Show();
        }

        private void HandleTransferCompleted()
        {
            RefreshRoomDetails();
        }
        private void RefreshRoomDetails()
        {
			var updatedRoom = roomService.GetAllRooms().FirstOrDefault(x => x.Id == Room.Id);
			if (updatedRoom != null)
			{
				SetRoom(new RoomViewModels
				{
					Id = updatedRoom.Id,
					RoomName = updatedRoom.RoomName,
					Status = updatedRoom.Status,
					PricePerDay = updatedRoom.PricePerDay
				});
			}
		}


   

        private void viewBillMenuItem_Click(object? sender, EventArgs e)
        {
            if (Room.Status == RoomStatus.Available)
            {
                MessageBox.Show("Room has not been booked yet " + Room.RoomName);
                return;

            }
            frmRoomBookingReceipt frm = new frmRoomBookingReceipt(Room.Id);
            frm.OnBookRoom += BookRoom;
            frm.OnCheckOut += CheckOut;

			frm.Show();
        }

        public void SetRoom(RoomViewModels room)
        {
            Room = room;
            var Price = roomService.GetAllRooms().Where(x => x.Id == room.Id).Select(x => new { x.PriceByHour, x.PricePerDay }).FirstOrDefault();
            lbName.Text = room.RoomName;
            lbGia.Text = $"Price: {room.PricePerDay.ToString("#,0")} VND";
            switch (room.Status)
            {
                case RoomStatus.Available:
                    ptRoom.Image = Properties.Resources.Available;
                    break;
                case RoomStatus.UnAvailable:
                    ptRoom.Image = Properties.Resources.UnAvailable;
                    break;
                case RoomStatus.CleanUp:
                    ptRoom.Image = Properties.Resources.UnderMaintenance;
                    break;
				//case RoomStatus.Unknown:
				//	ptRoom.Image = Properties.Resources.UnKnow;
				//	break;
			}
        }
        private void CheckOut()
        {
            if (Room.Status == RoomStatus.UnAvailable)
            {
                Room.Status = RoomStatus.CleanUp;
                roomService.UpdateRoom(Room);
                //MessageBox.Show($"Thanh toán {Room.RoomName} thành công!");
                ptRoom.Image = Properties.Resources.UnderMaintenance;
            }
        }
        private void BookRoom()
        {
            if (Room.Status == RoomStatus.Available)
            {
                Room.Status = RoomStatus.UnAvailable;
                roomService.UpdateRoom(Room);
                //MessageBox.Show($"Đặt phòng {Room.RoomName} thành công!");
                ptRoom.Image = Properties.Resources.UnAvailable;
            }
        }
        private void lbName_Click(object sender, EventArgs e)
        {

        }

        private void ptRoom_Click(object sender, EventArgs e)
        {
            
        }
    }
}

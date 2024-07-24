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
			var bookRoomMenuItem = new ToolStripMenuItem("Đặt phòng");
			var payRoomMenuItem = new ToolStripMenuItem("Thanh toán");
			var moveRoomMenuItem = new ToolStripMenuItem("Chuyển phòng");
			var viewBillMenuItem = new ToolStripMenuItem("Xem hóa đơn");

			// Add event handlers for menu items
			bookRoomMenuItem.Click += bookRoomMenuItem_Click;
			payRoomMenuItem.Click += payRoomMenuItem_Click;
			moveRoomMenuItem.Click += moveRoomMenuItem_Click;
			viewBillMenuItem.Click += viewBillMenuItem_Click;

			contextMenu.Items.AddRange(new ToolStripItem[] { bookRoomMenuItem, payRoomMenuItem, moveRoomMenuItem, viewBillMenuItem });

			ptRoom.ContextMenuStrip = contextMenu;
		}

		private void bookRoomMenuItem_Click(object? sender, EventArgs e)
        {
            frmRoomBookingReceipt frm = new frmRoomBookingReceipt();
            frm.OnBookRoom += BookRoom;
            frm.Show();
        }

		private void moveRoomMenuItem_Click(object? sender, EventArgs e)
		{
			MessageBox.Show("Chuyển phòng" + Room.RoomName);
		}

		private void payRoomMenuItem_Click(object? sender, EventArgs e)
		{
			CkeckoutRoom();
		}

		private void viewBillMenuItem_Click(object? sender, EventArgs e)
		{
			MessageBox.Show("View Detail");

		}

		public void SetRoom(RoomViewModels room)
		{
			Room = room;

			lbName.Text = room.RoomName;
			lbGia.Text = $"Giá: {room.Price.ToString("#,0")} VND";
			switch (room.Status)
			{
				case RoomStatus.Available:
					ptRoom.Image = Properties.Resources.Available;
					break;
				case RoomStatus.UnAvailable:
					ptRoom.Image = Properties.Resources.UnAvailable;
					break;
				case RoomStatus.UnderMaintenance:
					ptRoom.Image = Properties.Resources.UnderMaintenance;
					break;
			}
		}
		private void CkeckoutRoom()
		{
            if (Room.Status == RoomStatus.UnAvailable)
            {
                Room.Status = RoomStatus.Available;
                roomService.UpdateRoom(Room);
                MessageBox.Show($"Thanh toán {Room.RoomName} thành công!");
                ptRoom.Image = Properties.Resources.Available;
            }
        }
		private void BookRoom()
		{
			if (Room.Status == RoomStatus.Available)
			{
				Room.Status = RoomStatus.UnAvailable;
				roomService.UpdateRoom(Room);
				MessageBox.Show($"Đặt phòng {Room.RoomName} thành công!");
				ptRoom.Image = Properties.Resources.UnAvailable;
			}
		}

		private void lbName_Click(object sender, EventArgs e)
		{

		}
	}
}

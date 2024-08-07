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
    public partial class frmTransfer : Form
    {
		//public event Action OnTransferCompleted;

		public delegate void OnTransferCompletedHandler();
		public event OnTransferCompletedHandler OnTransferCompleted;


		private IRoomService _roomService;
        private IOrderService _orderService;
        public Guid RoomId { get; set; }

        public frmTransfer(Guid RoomId)
        {
            InitializeComponent();
            _roomService = new RoomService();
            this.RoomId = RoomId;
            loadNameroom(); LoadAvailableRooms();
            tb_roomnow.Enabled = false;
            _orderService = new OrderServicess();
        }

        void loadNameroom()
        {
            var NameRoom = _roomService.GetAllRooms().Where(x => x.Id == RoomId).Select(x => x.RoomName).FirstOrDefault();
            tb_roomnow.Text = NameRoom;
        }
        private void LoadAvailableRooms()
        {
            cbb_roomchange.Items.Clear();
            var availableRooms = _roomService.GetAllRoomsFromDb().Where(x => x.Status == RoomStatus.Available);
            foreach (var room in availableRooms)
            {
                cbb_roomchange.Items.Add(room.RoomName);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            var availableRooms = _roomService.GetAllRoomsFromDb().Where(x => x.Status == RoomStatus.Available);
            if (availableRooms.Any())
            {
                var newRoomName = cbb_roomchange.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(newRoomName))
                {
                    MessageBox.Show("Vui lòng chọn phòng mới.");
                    return;
                }

                var newRoom = _roomService.GetAllRoomsFromDb().FirstOrDefault(x => x.RoomName == newRoomName);
                if (newRoom == null)
                {
                    MessageBox.Show("Phòng được chọn không tồn tại.");
                    return;
                }

                if (newRoom.Status != RoomStatus.Available)
                {
                    MessageBox.Show("Phòng được chọn không khả dụng.");
                    return;
                }

                ChuyenPhong(RoomId, newRoom.Id);
				OnTransferCompleted?.Invoke();
				this.Close();
            }
            else
            {
                MessageBox.Show("Không có phòng trống.");
            }
        }
        public void ChuyenPhong(Guid oldRoomId, Guid newRoomId)
        {
            try
            {
                var oldRoom = _roomService.GetAllRoomsFromDb().FirstOrDefault(x => x.Id == oldRoomId);
                var newRoom = _roomService.GetAllRoomsFromDb().FirstOrDefault(x => x.Id == newRoomId);
                if(oldRoom.KindOfRoomId != newRoom.KindOfRoomId)
                {
					MessageBox.Show("Chỉ Được Chuyển Phòng Khi Cùng Loại Phòng", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}
                if (oldRoom == null)
                {
                    MessageBox.Show("Phòng cũ không tồn tại.");
                    return;
                }

                if (newRoom == null)
                {
                    MessageBox.Show("Phòng mới không tồn tại.");
                    return;
                }

                if (newRoom.Status != RoomStatus.Available)
                {
                    MessageBox.Show("Phòng mới không khả dụng.");
                    return;
                }

                var ordersInOldRoom = _orderService.GetByRoomId(oldRoomId);
                foreach (var order in ordersInOldRoom)
                {
                    order.RoomId = newRoomId;
                    _orderService.UpdateOrders(order);
                }

                oldRoom.Status = RoomStatus.Available;
                newRoom.Status = RoomStatus.UnAvailable;

                _roomService.UpdateRoom(oldRoom);
                _roomService.UpdateRoom(newRoom);

                MessageBox.Show("Chuyển phòng thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuyển phòng: " + ex.Message);
            }
        }

        private void frmTransfer_Load(object sender, EventArgs e)
        {

        }
    }
}

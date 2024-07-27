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
        private IServiceSevice _serviceSevice;

        private IRoomService _roomService;
        public Guid RoomId { get; set; }

        public frmTransfer(Guid RoomId)
        {
            InitializeComponent();
            _roomService = new RoomService();
            this.RoomId = RoomId;
            loadNameroom(); loadcbbroomchang();
            tb_roomnow.Enabled = false;

        }

        void loadNameroom()
        {
            var NameRoom = _roomService.GetAllRooms().Where(x => x.Id == RoomId).Select(x => x.RoomName).FirstOrDefault();
            tb_roomnow.Text = NameRoom;
        }
        void loadcbbroomchang()
        {
            cbb_roomchange.Items.Clear();
            foreach (var item in _roomService.GetAllRoomsFromDb().Where(x=>x.Status.ToString() == ServiceStatus.Available.ToString()))
            {
                cbb_roomchange.Items.Add(item.RoomName);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            var sttRoom = _roomService.GetAllRoomsFromDb().Where(x => x.Status.ToString() == ServiceStatus.Available.ToString());
            if (sttRoom != null)
            {
                var newroomName = cbb_roomchange.SelectedItem.ToString();
                var newroom = _roomService.GetAllRoomsFromDb().FirstOrDefault(x => x.RoomName == newroomName);
                if (newroom == null) 
                {
                    MessageBox.Show("Phòng được chọn không tồn tại.");
                    return;
                }
                if (newroom.Status != RoomStatus.Available)
                {
                    MessageBox.Show("Phòng được chọn không khả dụng.");
                    return;
                }
                ChuyenPhong(RoomId, newroom.Id);
            }
        }
        public void ChuyenPhong(Guid IdRoomCu , Guid IdRoomMoi)
        {

        }
    }
}

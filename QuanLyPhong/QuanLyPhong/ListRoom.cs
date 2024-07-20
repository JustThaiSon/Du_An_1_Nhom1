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
    public partial class ListRoom : Form
    {
        Guid IdCell = Guid.Empty;
        private IRoomService _roomService;
        private IFloorService _floorService;
        private IKindOfRoomService _kindOfRoomService;
        public ListRoom()
        {
            InitializeComponent();
            _roomService = new RoomService();
            _floorService = new FloorService();
            _kindOfRoomService = new KindOfRoomService();
            LoadCbbStatus();
            LoadCbbFloor();
            LoadCbbKindOfRoom();
            loadDtg();
            Clear();
        }

        void LoadCbbStatus()
        {
            cbbStatus.DataSource = Enum.GetValues(typeof(RoomStatus));
            cbbStatus.SelectedItem = RoomStatus.Unknown;
        }
        void LoadCbbFloor()
        {
            var floors = _floorService.GetAllFloorFromDb();

            cbb_floor.DataSource = floors;
            cbb_floor.DisplayMember = "FloorName";

            cbb_floor.SelectedIndex = -1;
        }
        void LoadCbbKindOfRoom()
        {
            var kindsOfRoom = _kindOfRoomService.GetAllKindOfRoomFromDb();

            cbb_typeroom.DataSource = kindsOfRoom;
            cbb_typeroom.DisplayMember = "KindOfRoomName";

            cbb_typeroom.SelectedIndex = -1;
        }

        void loadDtg()
        {
            dtgPhong.ColumnCount = 7;
            dtgPhong.Columns[0].Name = "Id";
            dtgPhong.Columns[0].Visible = false;
            dtgPhong.Columns[1].Name = "STT";
            dtgPhong.Columns[2].Name = "RoomName";
            dtgPhong.Columns[3].Name = "Status";
            dtgPhong.Columns[4].Name = "FloorName";
            dtgPhong.Columns[5].Name = "KindOFRoomName";
            dtgPhong.Columns[6].Name = "Price";
            dtgPhong.Rows.Clear();
            int Count = 0;
            foreach (var item in _roomService.GetAllRooms())
            {
                Count++;
                dtgPhong.Rows.Add(item.Id, Count, item.RoomName, item.Status, item.FloorName, item.KindOfRoomName, item.Price);
            }
        }
        void Clear()
        {
            tb_nameroom.Clear();
            cbbStatus.SelectedIndex = -1;
            cbb_floor.SelectedIndex = -1;
            cbb_typeroom.SelectedIndex = -1;
            tb_price.Clear();
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        bool Validate()
        {
            if (string.IsNullOrWhiteSpace(tb_nameroom.Text) ||
                cbbStatus.SelectedIndex == -1 ||
                cbb_floor.SelectedIndex == -1 ||
                cbb_typeroom.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(tb_price.Text))
            {
                return false;
            }
            return true;
        }
        bool ValidateName()
        {
            var Exists = _roomService.GetAllRoomsFromDb().Any(x => x.RoomName == tb_nameroom.Text);
            if (Exists)
            {
                return false;
            }
            return true;
        }

        private void btn_addRoom_Click(object sender, EventArgs e)
        {

            if (!Validate())
            {
                MessageBox.Show("Not be empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateName())
            {
                MessageBox.Show("Room name already exists", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (decimal.Parse(tb_price.Text) < 0)
            {
                MessageBox.Show(" The selling price must be greater than 0 VND!!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb_price.Focus();
                return;
            }
            var Room = new RoomViewModels()
            {
                RoomName = tb_nameroom.Text,
                Status = Enum.TryParse(cbbStatus.Text, out RoomStatus status) ? status : RoomStatus.Unknown,
                FloorId = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == cbb_floor.Text).Select(x => x.Id).FirstOrDefault(),
                KindOfRoomId = _kindOfRoomService.GetAllKindOfRoomFromDb().Where(x => x.KindOfRoomName == cbb_typeroom.Text).Select(x => x.Id).FirstOrDefault(),
                Price = decimal.Parse(tb_price.Text)
            };
            if (MessageBox.Show("Do you want to add this room?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _roomService.AddRoom(Room);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loadDtg();
            Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var exists = _roomService.GetAllRoomsFromDb().Any(x => x.Id == IdCell);
            if (!exists)
            {
                MessageBox.Show("Room not exists");
                return;
            }
            if (MessageBox.Show("Do you want to delete this room?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _roomService.RemoveRoom(IdCell);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loadDtg();
            Clear();
        }

        private void dtgPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selected = dtgPhong.SelectedRows[0];
                tb_nameroom.Text = selected.Cells[2].Value?.ToString() ?? "";
                cbbStatus.Text = selected.Cells[3].Value?.ToString() ?? "";
                cbb_floor.Text = selected.Cells[4].Value?.ToString() ?? "";
                cbb_typeroom.Text = selected.Cells[5].Value?.ToString() ?? "";
                tb_price.Text = selected.Cells[6].Value?.ToString() ?? "";
                IdCell = Guid.Parse(selected.Cells[0].Value?.ToString() ?? "");
            }
            catch (Exception)
            {

                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var exists = _roomService.GetAllRoomsFromDb().Any(x => x.Id == IdCell);
            if (!exists)
            {
                MessageBox.Show("Room not exists");
                return;
            }
            if (!Validate())
            {
                MessageBox.Show("Not be empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!exists && !ValidateName())
            {
                MessageBox.Show("Room name already exists", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (decimal.Parse(tb_price.Text) < 0)
            {
                MessageBox.Show(" The selling price must be greater than 0 VND!!!");
                tb_price.Focus();
                return;
            }
            var Room = new RoomViewModels()
            {
                Id = IdCell,
                RoomName = tb_nameroom.Text,
                Status = Enum.TryParse(cbbStatus.Text, out RoomStatus status) ? status : RoomStatus.Unknown,
                FloorId = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == cbb_floor.Text).Select(x => x.Id).FirstOrDefault(),
                KindOfRoomId = _kindOfRoomService.GetAllKindOfRoomFromDb().Where(x => x.KindOfRoomName == cbb_typeroom.Text).Select(x => x.Id).FirstOrDefault(),
                Price = decimal.Parse(tb_price.Text)
            };
            if (MessageBox.Show("Do you want to add this room?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _roomService.UpdateRoom(Room);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loadDtg();
            Clear();
        }

        private void tb_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

        }

        private void btn_addFloor_Click(object sender, EventArgs e)
        {
            AddFloor addFloor = new AddFloor();
            addFloor.Show();
        }

        private void btn_addTypeRoom_Click(object sender, EventArgs e)
        {
            AddTypeRoom addTypeRoom = new AddTypeRoom();
            addTypeRoom.Show();
        }
    }
}
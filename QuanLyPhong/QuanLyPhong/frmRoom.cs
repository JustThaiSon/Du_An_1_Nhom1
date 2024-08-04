using BUS.IService;
using BUS.Service;
using BUS.ViewModels;
using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhong
{
    public partial class frmRoom : Form
    {
        Guid IdCell = Guid.Empty;
        private IRoomService _roomService;
        private IFloorService _floorService;
        private IKindOfRoomService _kindOfRoomService;

        public frmRoom()
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

			if (Session.RoleCode == "emp")
			{
				btn_addFloor.Enabled = false;
				btn_addTypeRoom.Enabled = false;
			}
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
            dtgPhong.ColumnCount = 8;
            dtgPhong.Columns[0].Name = "Id";
            dtgPhong.Columns[0].Visible = false;
            dtgPhong.Columns[1].Name = "STT";
            dtgPhong.Columns[2].Name = "RoomName";
            dtgPhong.Columns[3].Name = "Status";
            dtgPhong.Columns[4].Name = "FloorName";
            dtgPhong.Columns[5].Name = "KindOFRoomName";
            dtgPhong.Columns[6].Name = "PricePerDay";
            dtgPhong.Columns[7].Name = "PriceByHour";
            dtgPhong.Rows.Clear();
            int Count = 0;
            foreach (var item in _roomService.GetAllRooms())
            {
                Count++;
                dtgPhong.Rows.Add(item.Id, Count, item.RoomName, item.Status, item.FloorName, item.KindOfRoomName, item.PricePerDay, item.PriceByHour);
            }
        }

        void Clear()
        {
            tb_nameroom.Clear();
            cbbStatus.SelectedIndex = -1;
            cbb_floor.SelectedIndex = -1;
            cbb_typeroom.SelectedIndex = -1;
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
                cbb_typeroom.SelectedIndex == -1)
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
                MessageBox.Show("Không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidateName())
            {
                MessageBox.Show("Tên phòng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var Room = new RoomViewModels()
            {
                RoomName = tb_nameroom.Text,
                Status = Enum.TryParse(cbbStatus.Text, out RoomStatus status) ? status : RoomStatus.Unknown,
                FloorId = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == cbb_floor.Text).Select(x => x.Id).FirstOrDefault(),
                KindOfRoomId = _kindOfRoomService.GetAllKindOfRoomFromDb().Where(x => x.KindOfRoomName == cbb_typeroom.Text).Select(x => x.Id).FirstOrDefault(),
            };

            if (MessageBox.Show("Bạn có muốn thêm phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _roomService.AddRoom(Room);
                MessageBox.Show(result, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            loadDtg();
            Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var exists = _roomService.GetAllRoomsFromDb().Any(x => x.Id == IdCell);
            if (!exists)
            {
                MessageBox.Show("Phòng không tồn tại");
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _roomService.RemoveRoom(IdCell);
                MessageBox.Show(result, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Phòng không tồn tại");
                return;
            }

            if (!Validate())
            {
                MessageBox.Show("Không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!exists && !ValidateName())
            {
                MessageBox.Show("Tên phòng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var Room = new RoomViewModels()
            {
                Id = IdCell,
                RoomName = tb_nameroom.Text,
                Status = Enum.TryParse(cbbStatus.Text, out RoomStatus status) ? status : RoomStatus.Unknown,
                FloorId = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == cbb_floor.Text).Select(x => x.Id).FirstOrDefault(),
                KindOfRoomId = _kindOfRoomService.GetAllKindOfRoomFromDb().Where(x => x.KindOfRoomName == cbb_typeroom.Text).Select(x => x.Id).FirstOrDefault(),
            };

            if (MessageBox.Show("Bạn có muốn cập nhật phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _roomService.UpdateRoom(Room);
                MessageBox.Show(result, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            frmFloor addFloor = new frmFloor();
            // Đăng ký sự kiện FloorUpdated
            addFloor.FloorUpdated += new frmFloor.FloorUpdatedEventHandler(OnFloorUpdated);
            addFloor.Show();
        }

        private void btn_addTypeRoom_Click(object sender, EventArgs e)
        {
            frmKindOfRoom addTypeRoom = new frmKindOfRoom();
            addTypeRoom.Show();
        }

        private void frmRoom_Load(object sender, EventArgs e)
        {

        }

        // Xử lý sự kiện FloorUpdated
        private void OnFloorUpdated(object sender, EventArgs e)
        {
            LoadCbbFloor();
        }
    }
}

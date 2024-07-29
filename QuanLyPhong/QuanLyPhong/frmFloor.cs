using BUS.IService;
using BUS.Service;
using DAL.Entities;
using System;
using System.Windows.Forms;
using IFloorService = BUS.IService.IFloorService;

namespace QuanLyPhong
{
    public partial class frmFloor : Form
    {
        private readonly IFloorService _floorService;
        Guid IdOfedit;

        // Tạo delegate và event cho sự kiện cập nhật tầng
        public delegate void FloorUpdatedEventHandler(object sender, EventArgs e);
        public event FloorUpdatedEventHandler FloorUpdated;

        public frmFloor()
        {
            InitializeComponent();
            _floorService = new FloorService();
            LoadFloors();
        }

        private void tb_nameroom_TextChanged(object sender, EventArgs e)
        {

        }

        public void LoadFloors()
        {
            DTgrvFloor.ColumnCount = 3;
            DTgrvFloor.Columns[0].Name = "STT";
            DTgrvFloor.Columns[1].Name = "Tầng";
            DTgrvFloor.Columns[2].Name = "Id";
            DTgrvFloor.Columns[2].Visible = false;
            DTgrvFloor.Rows.Clear();
            int stt = 0;
            foreach (var item in _floorService.GetAllFloorFromDb())
            {
                stt++;
                DTgrvFloor.Rows.Add(stt, item.FloorName, item.Id);
            }
        }

        public void Add()
        {
            if (string.IsNullOrWhiteSpace(txtFloorName.Text))
            {
                MessageBox.Show("Tên tầng không được để trống.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var floor = new Floor()
            {
                FloorName = txtFloorName.Text
            };

            if (MessageBox.Show("Bạn có muốn thêm tầng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _floorService.AddFloor(floor);
                MessageBox.Show(result, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadFloors();
            Clear();

            // Kích hoạt sự kiện cập nhật tầng
            FloorUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            txtFloorName.Clear();
        }

        public void Edit()
        {
            if (string.IsNullOrWhiteSpace(txtFloorName.Text))
            {
                MessageBox.Show("Tên tầng không được để trống.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var floor = new Floor()
            {
                Id = IdOfedit,
                FloorName = txtFloorName.Text
            };

            if (MessageBox.Show("Bạn có muốn cập nhật tầng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _floorService.UpdateFloor(floor);
                MessageBox.Show(result, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadFloors();
            Clear();
        }

        public void Remove()
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string result = _floorService.RemoveFloor(IdOfedit);
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void DTgrvFloor_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selected = DTgrvFloor.SelectedRows[0];
                txtFloorName.Text = selected.Cells[1].Value?.ToString() ?? "";
                IdOfedit = Guid.Parse(selected.Cells[2].Value?.ToString() ?? "");
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}

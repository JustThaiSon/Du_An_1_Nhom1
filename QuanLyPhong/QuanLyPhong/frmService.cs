using BUS.IService;
using BUS.Service;
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
    public partial class frmService : Form
    {
        private IServiceService _serSevice;
        private System.Timers.Timer _timer;
        Guid IdCell = Guid.Empty;

        public frmService()
        {
            InitializeComponent();
            _serSevice = new IServiceService();
            loadDtgv();
            LoadCbbSatus();
            Css();
            Clear();
            dtDate.Enabled = false;

        }
        void Css()
        {
            dtDate.Format = DateTimePickerFormat.Custom;
            dtDate.CustomFormat = "dd/MM/yyyy";
        }
        private void Clear()
        {
            tb_SerName.Clear();
            tb_price.Clear();
            tb_Quantity.Clear();
            tbtype.Clear();
            tb_des.Clear();
            dtDate.Value = DateTime.Now;
            cbbStatus.Enabled = false; // Vô hiệu hóa ComboBox
            cbbStatus.DropDownStyle = ComboBoxStyle.DropDownList; // Đặt ComboBox vào chế độ chỉ chọn, không nhập liệu
            cbbStatus.SelectedIndex = -1; // Đặt chọn mục về -1 để không có mục nào được chọn
            cbbStatus.TabStop = false; // Ngăn ComboBox nhận lấy focus
        }
        void LoadCbbSatus()
        {
            foreach (ServiceStatus status in Enum.GetValues(typeof(ServiceStatus)))
            {
                cbbStatus.Items.Add(status);
            }
        }
        void loadDtgv()
        {
            dtgDanhSach.ColumnCount = 9;
            dtgDanhSach.Columns[0].Name = "Id";
            dtgDanhSach.Columns[0].Visible = false;
            dtgDanhSach.Columns[1].Name = "STT";
            dtgDanhSach.Columns[2].Name = "ServiceName";
            dtgDanhSach.Columns[3].Name = "Price";
            dtgDanhSach.Columns[4].Name = "Type";
            dtgDanhSach.Columns[5].Name = "Status";
            dtgDanhSach.Columns[6].Name = "Quantity";
            dtgDanhSach.Columns[7].Name = "CreateDate";
            dtgDanhSach.Columns[8].Name = "Descretion";
            dtgDanhSach.Rows.Clear();
            int Count = 0;
            foreach (var item in _serSevice.GetAllServiceFromDb())
            {
                Count++;
                dtgDanhSach.Rows.Add(item.Id, Count, item.Name, item.Price, item.Type, item.Status, item.Quantity, item.CreatedDate.ToString("dd/MM/yyyy"), item.Descretion);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            DateTime createDate;
            string createDatest = dtDate.Value.ToString("dd/MM/yyyy");

            if (!DateTime.TryParseExact(createDatest, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out createDate))
            {
                MessageBox.Show("Ngày không hợp lệ.");
                return;
            }
            if (Convert.ToInt32(tb_Quantity.Text) < 0 && Convert.ToDecimal(tb_price.Text) < 0)
            {
                MessageBox.Show("Giá sản phẩm và số lượng không hợp lệ.");
                return;
            }
            if (Convert.ToDecimal(tb_price.Text) < 0)
            {

                MessageBox.Show("Giá sản phẩm khong hợp lệ.");
                return;
            }
            if (Convert.ToInt32(tb_Quantity.Text)<0)
            {
                MessageBox.Show("Số lượng khong hợp lệ.");
                return;
            }
           
            var addService = new Services()
            {
                CreatedDate = createDate,
                Name = tb_SerName.Text,
                Quantity = Convert.ToInt32(tb_Quantity.Text),
                Type = tbtype.Text,
                Descretion = tb_des.Text,
                Price = Convert.ToDecimal(tb_price.Text),
                Status = (Convert.ToInt32(tb_Quantity.Text) > 0) ? ServiceStatus.Available : ServiceStatus.OutOfStock
            };

            if (MessageBox.Show("Do you want to add this service?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _serSevice.AddService(addService);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            loadDtgv();
            Clear();
        }

        private void dtgDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selected = dtgDanhSach.SelectedRows[0];
                tb_SerName.Text = selected.Cells[2].Value?.ToString() ?? "";
                tb_price.Text = selected.Cells[3].Value?.ToString() ?? "";
                tbtype.Text = selected.Cells[4].Value?.ToString() ?? "";
                cbbStatus.Text = selected.Cells[5].Value?.ToString() ?? "";
                tb_Quantity.Text = selected.Cells[6].Value?.ToString() ?? "";
                dtDate.Value = DateTime.ParseExact(selected.Cells[7].Value?.ToString(), "dd/MM/yyyy", null);
                tb_des.Text = selected.Cells[8].Value?.ToString() ?? "";
                IdCell = Guid.Parse(selected.Cells[0].Value?.ToString() ?? "");
                cbbStatus.Enabled = false;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            var exists = _serSevice.GetAllServiceFromDb().Any(x => x.Id == IdCell);
            if (!exists)
            {
                MessageBox.Show("Service not exists", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime createDate;
            string createDatest = dtDate.Value.ToString("dd/MM/yyyy");

            if (!DateTime.TryParseExact(createDatest, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out createDate))
            {
                MessageBox.Show("Ngày không hợp lệ.");
                return;
            }
            if (Convert.ToInt32(tb_Quantity.Text) < 0 && Convert.ToDecimal(tb_price.Text) < 0)
            {
                MessageBox.Show("Giá sản phẩm và số lượng không hợp lệ.");
                return;
            }
            if (Convert.ToDecimal(tb_price.Text) < 0)
            {

                MessageBox.Show("Giá sản phẩm khong hợp lệ.");
                return;
            }
            if (Convert.ToInt32(tb_Quantity.Text) < 0)
            {
                MessageBox.Show("Số lượng khong hợp lệ.");
                return;
            }

            var addService = new Services()
            {
                Id = IdCell,
                CreatedDate = createDate,
                Name = tb_SerName.Text,
                Quantity = Convert.ToInt32(tb_Quantity.Text),
                Type = tbtype.Text,
                Descretion = tb_des.Text,
                Price = Convert.ToDecimal(tb_price.Text),
                Status = (Convert.ToInt32(tb_Quantity.Text) > 0) ? ServiceStatus.Available : ServiceStatus.OutOfStock
            };

            if (MessageBox.Show("Do you want to update this service?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string result = _serSevice.UpdateService(addService);
                    MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            loadDtgv();
            Clear();
        }


        private void btn_delete_Click(object sender, EventArgs e)
        {
            var exists = _serSevice.GetAllServiceFromDb().Any(x => x.Id == IdCell);
            if (!exists)
            {
                MessageBox.Show("Service not exists");
                return;
            }
            if (MessageBox.Show("Do you want to delete this service?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _serSevice.RemoveService(IdCell);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loadDtgv();
            Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}

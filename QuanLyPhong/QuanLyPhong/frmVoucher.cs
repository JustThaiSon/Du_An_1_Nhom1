using BUS.IService;
using BUS.Service;
using DAL.Entities;
using DAL.Enums;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace QuanLyPhong
{
    public partial class frmVoucher : Form
    {
        private IVoucherSevice _voucherSevice;
        private System.Timers.Timer _timer;
        Guid IdCell = Guid.Empty;
        public frmVoucher()
        {
            InitializeComponent();
            _voucherSevice = new VoucherSevice();
            SetupTimer();
            LoadCbbSatus();
            LoadDtg();
            ClearForm();
            Css();
        }
        void Css()
        {
            dtStartDate.Format = DateTimePickerFormat.Custom;
            dtStartDate.CustomFormat = "dd/MM/yyyy";

            DtEndDate.Format = DateTimePickerFormat.Custom;
            DtEndDate.CustomFormat = "dd/MM/yyyy";
        }
        private void SetupTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 6000;
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }
        private void ClearForm()
        {
            dtStartDate.Value = dtStartDate.MinDate;
            DtEndDate.Value = DtEndDate.MinDate;
            tb_voucherName.Clear();
            tb_minPrice.Clear();
            tbDiscount.Clear();
            cbbStatus.Enabled = false; // Vô hiệu hóa ComboBox
            cbbStatus.DropDownStyle = ComboBoxStyle.DropDownList; // Đặt ComboBox vào chế độ chỉ chọn, không nhập liệu
            cbbStatus.SelectedIndex = -1; // Đặt chọn mục về -1 để không có mục nào được chọn
            cbbStatus.TabStop = false; // Ngăn ComboBox nhận lấy focus
            tbVoucherCode.Clear();
            tbVoucherCode.Enabled = false;
            tbVoucherCode.TabStop = false;

        }
        void LoadCbbSatus()
        {
            foreach (VoucherStatus status in Enum.GetValues(typeof(VoucherStatus)))
            {
                cbbStatus.Items.Add(status);
            }
        }
        void LoadDtg()
        {
            dtgDanhSach.ColumnCount = 9;
            dtgDanhSach.Columns[0].Name = "Id";
            dtgDanhSach.Columns[0].Visible = false;
            dtgDanhSach.Columns[1].Name = "STT";
            dtgDanhSach.Columns[2].Name = "VoucherName";
            dtgDanhSach.Columns[3].Name = "VoucherCode";
            dtgDanhSach.Columns[4].Name = "DisscountRate";
            dtgDanhSach.Columns[5].Name = "MinPrice";
            dtgDanhSach.Columns[6].Name = "Status";
            dtgDanhSach.Columns[7].Name = "StartDate";
            dtgDanhSach.Columns[8].Name = "EndDate";
            dtgDanhSach.Rows.Clear();
            int Count = 0;
            foreach (var item in _voucherSevice.GetAllVoucherFromDb())
            {
                Count++;
                dtgDanhSach.Rows.Add(item.Id, Count, item.VoucherName, item.VoucherCode, item.DiscountRate, item.MinPrice, item.Status, item.StartDate.ToString("dd/MM/yyyy"), item.EndDate.ToString("dd/MM/yyyy"));
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _voucherSevice.UpdateVoucherStatusAuTo();
        }

        private void btn_addRoom_Click(object sender, EventArgs e)
        {
            DateTime startDate, endDate;
            string startDateStr = dtStartDate.Value.ToString("dd/MM/yyyy");
            string endDateStr = DtEndDate.Value.ToString("dd/MM/yyyy");

            if (!DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate) ||
                !DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                MessageBox.Show("Ngày không hợp lệ.");
                return;
            }
            if (endDate <= startDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var addVoucher = new Voucher()
            {
                StartDate = startDate,
                EndDate = endDate,
                VoucherName = tb_voucherName.Text,
                DiscountRate = Convert.ToDecimal(tbDiscount.Text),
                MinPrice = Convert.ToDecimal(tb_minPrice.Text),
            };
            if (MessageBox.Show("Do you want to add this voucher?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _voucherSevice.AddVoucher(addVoucher);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadDtg();
            ClearForm();
        }

        private void dtgDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selected = dtgDanhSach.SelectedRows[0];
                tb_voucherName.Text = selected.Cells[2].Value?.ToString() ?? "";
                tbVoucherCode.Text = selected.Cells[3].Value?.ToString() ?? "";
                tbDiscount.Text = selected.Cells[4].Value?.ToString() ?? "";
                tb_minPrice.Text = selected.Cells[5].Value?.ToString() ?? "";
                cbbStatus.Text = selected.Cells[6].Value?.ToString() ?? "";
                dtStartDate.Value = DateTime.ParseExact(selected.Cells[7].Value?.ToString(), "dd/MM/yyyy", null);
                DtEndDate.Value = DateTime.ParseExact(selected.Cells[8].Value?.ToString(), "dd/MM/yyyy", null);
                IdCell = Guid.Parse(selected.Cells[0].Value?.ToString() ?? "");
                cbbStatus.Enabled = true;
            }
            catch (Exception)
            {

                return;
            }
        }
        //update
        private void button1_Click(object sender, EventArgs e)
        {
            var exists = _voucherSevice.GetAllVoucherFromDb().Any(x => x.Id == IdCell);
            if (!exists)
            {
                MessageBox.Show("Voucher not exists", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime startDate, endDate;
            string startDateStr = dtStartDate.Value.ToString("dd/MM/yyyy");
            string endDateStr = DtEndDate.Value.ToString("dd/MM/yyyy");

            if (!DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate) ||
                !DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                MessageBox.Show("Ngày không hợp lệ.");
                return;
            }
            if ((VoucherStatus)cbbStatus.SelectedItem != VoucherStatus.Cancelled)
            {
                MessageBox.Show("Bạn chỉ có thể cập nhật trạng thái thành Cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (endDate <= startDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var addVoucher = new Voucher()
            {
                Id = IdCell,
                StartDate = startDate,
                EndDate = endDate,
                VoucherName = tb_voucherName.Text,
                DiscountRate = Convert.ToDecimal(tbDiscount.Text),
                MinPrice = Convert.ToDecimal(tb_minPrice.Text),
                Status = VoucherStatus.Cancelled,
            };
            if (MessageBox.Show("Do you want to update this voucher?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _voucherSevice.UpdateVoucher(addVoucher);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadDtg();
            ClearForm();
            cbbStatus.Enabled = false;
        }
        // Xóa
        private void button2_Click(object sender, EventArgs e)
        {
            var exists = _voucherSevice.GetAllVoucherFromDb().Any(x => x.Id == IdCell);
            if (!exists)
            {
                MessageBox.Show("Voucher not exists");
                return;
            }
            if (MessageBox.Show("Do you want to delete this voucher?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _voucherSevice.RemoveVoucher(IdCell);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadDtg();
            ClearForm();
        }

        
    }
}

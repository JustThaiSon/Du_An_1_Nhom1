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
using System.Timers;
using System.Windows.Forms;

namespace QuanLyPhong
{
    public partial class frmVoucher : Form
    {
        private IVoucherSevice _voucherSevice;
        private System.Timers.Timer _timer;
        public frmVoucher()
        {
            InitializeComponent();
            _voucherSevice = new VoucherSevice();
            SetupTimer();
            LoadCbbSatus();
            LoadDtg();
            ClearForm();
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
                dtgDanhSach.Rows.Add(item.Id, Count, item.VoucherName, item.VoucherCode, item.DiscountRate, item.MinPrice, item.Status,item.StartDate,item.EndDate);
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _voucherSevice.UpdateVoucherStatusAuTo();
        }

        private void btn_addRoom_Click(object sender, EventArgs e)
        {
            DateTime startDate, endDate;
            if (!DateTime.TryParse(dtStartDate.Text, out startDate) || !DateTime.TryParse(DtEndDate.Text, out endDate))
            {
                MessageBox.Show("Ngày không hợp lệ.");
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
    }
}

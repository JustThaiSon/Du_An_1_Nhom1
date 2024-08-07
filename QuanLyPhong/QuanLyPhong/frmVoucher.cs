using BUS.IService;
using BUS.Service;
using DAL.Entities;
using DAL.Enums;
using Guna.UI2.WinForms.Suite;
using Microsoft.VisualBasic.Devices;
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
			dtStartDate.CustomFormat = "dd/MM/yyyy HH:mm";

			DtEndDate.Format = DateTimePickerFormat.Custom;
			DtEndDate.CustomFormat = "dd/MM/yyyy HH:mm";
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
				cbbStatusfilter.Items.Add(status);
			}
			if (cbbStatus.Items.Count > 0)
			{
				cbbStatus.SelectedIndex = -1;
			}

			if (cbbStatusfilter.Items.Count > 0)
			{
				cbbStatusfilter.SelectedIndex = -1;
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
			//kjkk
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

			if (string.IsNullOrEmpty(tb_voucherName.Text))
			{
				MessageBox.Show("Voucher name cannot be empty.");
				return;
			}

			if (string.IsNullOrEmpty(tbDiscount.Text))
			{
				MessageBox.Show("Discount rate cannot be empty.");
				return;
			}

			if (string.IsNullOrEmpty(tb_minPrice.Text))
			{
				MessageBox.Show("Minimum price cannot be empty.");
				return;
			}

			if (!decimal.TryParse(tbDiscount.Text, out decimal discountRate))
			{
				MessageBox.Show("Invalid discount rate. Please enter a valid decimal number.");
				return;
			}

			if (decimal.Parse(tbDiscount.Text) > 1)
			{
				MessageBox.Show("The discount ratio must be less than or equal to 1");
				return;
			}

			if (!decimal.TryParse(tb_minPrice.Text, out decimal minPrice))
			{
				MessageBox.Show("Invalid minimum price. Please enter a valid decimal number.");
				return;
			}

			if (!DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate) ||
				!DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
			{
				MessageBox.Show("Invalid date.");
				return;
			}

			if (endDate < startDate)
			{
				MessageBox.Show("\r\nThe end date must be greater than the start date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (startDate < DateTime.Now)
			{
				MessageBox.Show("The start date must be greater than or equal to the current date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (endDate == startDate)
			{
				endDate = endDate.AddDays(1);
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
			if (string.IsNullOrEmpty(tb_voucherName.Text))
			{
				MessageBox.Show("Voucher name cannot be empty.");
				return;
			}

			if (string.IsNullOrEmpty(tbDiscount.Text))
			{
				MessageBox.Show("Discount rate cannot be empty.");
				return;
			}

			if (string.IsNullOrEmpty(tb_minPrice.Text))
			{
				MessageBox.Show("Minimum price cannot be empty.");
				return;
			}

			if (!decimal.TryParse(tbDiscount.Text, out decimal discountRate))
			{
				MessageBox.Show("Invalid discount rate. Please enter a valid decimal number.");
				return;
			}

			if (decimal.Parse(tbDiscount.Text) > 1)
			{
				MessageBox.Show("The discount ratio must be less than or equal to 1");
				return;
			}

			if (!decimal.TryParse(tb_minPrice.Text, out decimal minPrice))
			{
				MessageBox.Show("Invalid minimum price. Please enter a valid decimal number.");
				return;
			}

			DateTime startDate, endDate;
			string startDateStr = dtStartDate.Value.ToString("dd/MM/yyyy");
			string endDateStr = DtEndDate.Value.ToString("dd/MM/yyyy");

			if (!DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate) ||
				!DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
			{
				MessageBox.Show("Date is not valid .");
				return;
			}
			var status = _voucherSevice.GetAllVoucherFromDb()
						   .Where(x => x.Id == IdCell)
						   .Select(x => x.Status).FirstOrDefault();

			if ((VoucherStatus)cbbStatus.SelectedItem != VoucherStatus.Cancelled
				&& (VoucherStatus)cbbStatus.SelectedItem != status)
			{
				MessageBox.Show("Bạn chỉ có thể cập nhật trạng thái thành Cancelled.",
								"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (endDate < startDate)
			{
				MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (endDate < DateTime.Now)
			{
				MessageBox.Show("Ngày Kết thúc phải lớn hơn hoặc bằng ngày hiện tại.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (endDate == startDate)
			{
				endDate = endDate.AddDays(1);
			}


			var addVoucher = new Voucher()
			{
				Id = IdCell,
				StartDate = startDate,
				EndDate = endDate,
				VoucherName = tb_voucherName.Text,
				DiscountRate = Convert.ToDecimal(tbDiscount.Text),
				MinPrice = Convert.ToDecimal(tb_minPrice.Text),
				Status = (VoucherStatus)cbbStatus.SelectedItem,
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
		void LoadDtgSearch(string KeyWord)
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
			var Search = _voucherSevice.GetAllVoucherFromDb()
			.Where(x => x.VoucherCode.StartsWith(KeyWord) || x.VoucherName.StartsWith(KeyWord))
			.ToList();

			foreach (var item in Search)
			{
				Count++;
				dtgDanhSach.Rows.Add(item.Id, Count, item.VoucherName, item.VoucherCode, item.DiscountRate, item.MinPrice, item.Status, item.StartDate.ToString("dd/MM/yyyy"), item.EndDate.ToString("dd/MM/yyyy"));
			}
		}
		private void tb_search_TextChanged(object sender, EventArgs e)
		{
			LoadDtgSearch(tb_search.Text);
		}
		private void tb_minPrice_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsControl(e.KeyChar))
			{
				return;
			}

			if (!char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}
		private void tbDiscount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsControl(e.KeyChar))
			{
				return;
			}

			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
			{
				e.Handled = true;
				return;
			}
		}
		void LoadDtgFilterStatus(string KeyWord)
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
			var vouchers = _voucherSevice.GetAllVoucherFromDb();

			if (Enum.TryParse(cbbStatusfilter.SelectedItem.ToString(), out VoucherStatus selectedStatus))
			{
				var filteredVouchers = vouchers.Where(x => x.Status == selectedStatus).ToList();
				int count = 0;
				foreach (var item in filteredVouchers)
				{
					count++;
					dtgDanhSach.Rows.Add(item.Id, count, item.VoucherName, item.VoucherCode, item.DiscountRate, item.MinPrice, item.Status, item.StartDate.ToString("dd/MM/yyyy"), item.EndDate.ToString("dd/MM/yyyy"));
				}
			}
		}
		private void cbbStatusfilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadDtgFilterStatus(cbbStatusfilter.Text);
		}
	}
}

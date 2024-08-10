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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace QuanLyPhong
{
	public partial class frmCustomer : Form
	{
		private ICustomerService _customerService;
		private Guid selectedCustomerId = Guid.Empty;
		private IHistorypointService _historypointService;

		public frmCustomer()
		{
			_customerService = new CustomerService();
			_historypointService = new HistoryPointsService();
			InitializeComponent();
		}

		private void frmCustomer_Load(object sender, EventArgs e)
		{
			this.load_Gender();
			this.Load_dtGV_Customer();
		}

		public void Load_dtGV_Customer()
		{
			dtGV_Customer.Rows.Clear();
			dtGV_Customer.ColumnCount = 10;
			dtGV_Customer.Columns[0].Name = "STT";
			dtGV_Customer.Columns[1].Name = "Id";
			dtGV_Customer.Columns[1].Visible = false;
			dtGV_Customer.Columns[2].Name = "CustomerCode";
			dtGV_Customer.Columns[3].Name = "Name";
			dtGV_Customer.Columns[4].Name = "PhoneNumber";
			dtGV_Customer.Columns[5].Name = "Email";
			dtGV_Customer.Columns[6].Name = "Address";
			dtGV_Customer.Columns[7].Name = "Gender";
			dtGV_Customer.Columns[8].Name = "CCCD";
			dtGV_Customer.Columns[9].Name = "Point";

			int Count = 0;

			foreach (var item in _customerService.GetAllCustomerFromDb())
			{
				Count++;
				dtGV_Customer.Rows.Add(Count, item.Id, item.CustomerCode, item.Name, item.PhoneNumber, item.Email, item.Address, item.Gender, item.CCCD, item.Point.ToString("0"));
			}
		}

		public void load_Gender()
		{
			foreach (MenuGender value in Enum.GetValues(typeof(MenuGender)))
			{
				cbb_add_Gender.Items.Add(value);
				cbb_edit_Gender.Items.Add(value);
			}
		}
		public void add_Clear()
		{
			
			txt_add_CustomerCode.Clear();
			txt_add_Name.Clear();
			txt_add_Email.Clear();
			txt_add_Adress.Clear();
			txt_add_PhoneNumber.Clear();
			txt_add_CCCD.Clear();
			cbb_add_Gender.SelectedIndex = 0;
			
		}
		public void edit_Clear()
		{
			
			txt_edit_CustomerCode.Clear();
			txt_edit_Name.Clear();
			txt_edit_Email.Clear();
			txt_edit_Adress.Clear();
			txt_edit_PhoneNumber.Clear();
			txt_edit_CCCD.Clear();
			cbb_edit_Gender.SelectedIndex = 0;
			//NUD_edit_Point.Value = 0;
		}
		private bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
		private void dtGV_Customer_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.RowIndex < dtGV_Customer.Rows.Count - 1)
			{
				selectedCustomerId = (Guid)dtGV_Customer.Rows[e.RowIndex].Cells[1].Value;
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			dtGV_Customer.Rows.Clear();
			dtGV_Customer.ColumnCount = 10;
			dtGV_Customer.Columns[0].Name = "STT";
			dtGV_Customer.Columns[1].Name = "Id";
			dtGV_Customer.Columns[1].Visible = false;
			dtGV_Customer.Columns[2].Name = "CustomerCode";
			dtGV_Customer.Columns[3].Name = "Name";
			dtGV_Customer.Columns[4].Name = "PhoneNumber";
			dtGV_Customer.Columns[5].Name = "Email";
			dtGV_Customer.Columns[6].Name = "Address";
			dtGV_Customer.Columns[7].Name = "Gender";
			dtGV_Customer.Columns[8].Name = "CCCD";
			dtGV_Customer.Columns[9].Name = "Point";

			int Count = 0;
			var listCustomer = _customerService.GetAllCustomerFromDb()
			.Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower()) || x.CCCD.ToLower().StartsWith(txtSearch.Text.ToLower()) || x.CustomerCode.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

			foreach (var item in listCustomer)
			{
				Count++;
				dtGV_Customer.Rows.Add(Count, item.Id, item.CustomerCode, item.Name, item.PhoneNumber, item.Email, item.Address, item.Gender, item.CCCD, item.Point.ToString("0"));
			}
		}

		private void btn_list_Add_Click(object sender, EventArgs e)
		{
			tab_Customer.SelectedTab = tabPage_AddCustomer;
		}

		private bool IsValidPhoneNumber(string phoneNumber)
		{
			return Regex.IsMatch(phoneNumber, @"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b$");
		}
		private bool IsValidCCCD(string cccd)
		{
			string cccdPattern = @"^\d{12}$";
			return Regex.IsMatch(cccd, cccdPattern);
		}
		private void btn_add_Add_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txt_add_Name.Text) || txt_add_Name.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
			{
				MessageBox.Show("Name is not empty and no special characters .");
				return;
			}
			if (string.IsNullOrWhiteSpace(txt_add_Adress.Text))
			{
				MessageBox.Show("Address is not empty.");
				return;
			}
			if (string.IsNullOrWhiteSpace(txt_add_CCCD.Text))
			{
				MessageBox.Show("CCCD is not empty.");
				return;
			}
			if (!IsValidEmail(txt_add_Email.Text))
			{
				MessageBox.Show("Invalid email!!!.");
				return;

			}
			if (!IsValidPhoneNumber(txt_add_PhoneNumber.Text))
			{
				MessageBox.Show("Invalid phonenumber!!!.");
				return;
			}
			if (!IsValidPhoneNumber(txt_add_PhoneNumber.Text))
			{
				MessageBox.Show("Invalid phonenumber!!!.");
				return;
			}

			if (!IsValidCCCD(txt_add_CCCD.Text))
			{
				MessageBox.Show("Invalid CCCD");
				return;
			}

			
			MenuGender selectedGender = (MenuGender)Enum.Parse(typeof(MenuGender), cbb_add_Gender.SelectedItem.ToString());
			var customer = new Customer()
			{
				Name = txt_add_Name.Text,
				Email = txt_add_Email.Text,
				PhoneNumber = txt_add_PhoneNumber.Text,
				Address = txt_add_Adress.Text,
				CCCD = txt_add_CCCD.Text,
				Gender = selectedGender,
				Point = 0
			};

			MessageBox.Show(_customerService.AddCustomer(customer), "Notificaiton");
			this.add_Clear();
			Load_dtGV_Customer();
			tab_Customer.SelectedTab = tabPage_ListCustomer;
		}

		private void bnt_list_edit_Click(object sender, EventArgs e)
		{
			var customer = _customerService.GetAllCustomerFromDb().FirstOrDefault(x => x.Id == selectedCustomerId);
			if (customer == null)
			{
				MessageBox.Show("Please select 1 Customer to edit!!");
			}
			else
			{
				tab_Customer.SelectedTab = tabPage_EditCustomer;
				lb_edit_Id.Text = customer.Id.ToString();
				txt_edit_CustomerCode.Text = customer.CustomerCode.ToString();
				txt_edit_Name.Text = customer.Name;
				txt_edit_Email.Text = customer.Email;
				txt_edit_Adress.Text = customer.Address;
				txt_edit_PhoneNumber.Text = customer.PhoneNumber;
				txt_edit_CCCD.Text = customer.CCCD;
				cbb_edit_Gender.Text = customer.Gender.ToString();
				//NUD_edit_Point.Value = customer.Point;
			}
		}

		private void btn_add_Clear_Click(object sender, EventArgs e)
		{
			add_Clear();
		}

		private void btn_edit_Clear_Click(object sender, EventArgs e)
		{
			this.edit_Clear();
		}
		private void btn_Delete_Click(object sender, EventArgs	 e)
		{
			//if (selectedCustomerId == null)
			//{
			//	MessageBox.Show("Customer is not exist!!!");
			//	return;
			//}
			//DialogResult = MessageBox.Show("Are you Sure?", "Comfirm",
			// MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			//if (DialogResult == DialogResult.Yes)
			//{
			//	_customerService.RemoveCustomer(selectedCustomerId);
			//	MessageBox.Show("Delete Success!!!");
			//}
			//this.edit_Clear();
			//this.add_Clear();
			//this.Load_dtGV_Customer();
		}

		private void btn_edit_Edit_Click(object sender, EventArgs e)
		{
			var customer = _customerService.GetAllCustomerFromDb().FirstOrDefault(x => x.Id == selectedCustomerId);
			string cccd = txt_edit_CCCD.Text;
			string Sdt = txt_edit_PhoneNumber.Text;

			if (string.IsNullOrWhiteSpace(txt_edit_Name.Text) || txt_edit_Name.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
			{
				MessageBox.Show("Name is not empty and no special characters .");
				return;
			}

			if (string.IsNullOrWhiteSpace(txt_edit_Adress.Text))
			{
				MessageBox.Show("Address is not empty");
				return;
			}

			if (string.IsNullOrWhiteSpace(cccd))
			{
				MessageBox.Show("CCCD is not empty.");
				return;
			}
			if (!IsValidCCCD(cccd))
			{
				MessageBox.Show("Invalid CCCD.");
				return;
			}
			if (!IsValidEmail(txt_edit_Email.Text))
			{
				MessageBox.Show("Invalid Email !!!.");
				return;
			}

			if (string.IsNullOrWhiteSpace(Sdt))
			{
				MessageBox.Show("PhoneNumber is not empty.");
				return;
			}
			if (!IsValidPhoneNumber(Sdt))
			{
				MessageBox.Show("Invalid PhoneNumber.");
				return;
			}
			//if (NUD_edit_Point.Value < 0)
			//{
			//	MessageBox.Show("Point must be greater than or equal to 0.");
			//	return;
			//}
			MenuGender selectedGender = (MenuGender)Enum.Parse(typeof(MenuGender), cbb_edit_Gender.SelectedItem.ToString());

			customer.Name = txt_edit_Name.Text;
			customer.Address = txt_edit_Adress.Text;
			customer.CCCD = cccd;
			customer.PhoneNumber = Sdt;
			customer.Email = txt_edit_Email.Text;
			customer.Gender = selectedGender;
			//customer.Point = int.Parse(NUD_edit_Point.Value.ToString("0"));
			MessageBox.Show(_customerService.UpdateCustomer(customer), "Notificaiton");
			this.edit_Clear();
			Load_dtGV_Customer();
			tab_Customer.SelectedTab = tabPage_ListCustomer;
		}

		private void btnHistoryPoints_Click(object sender, EventArgs e)
		{
			tab_Customer.SelectedTab = tabPage_Historypoints;
			LoadHistoryPoint(selectedCustomerId);
		}

		void LoadHistoryPoint(Guid Id)
		{
			dtgHistoryPoints.ColumnCount = 5;
			dtgHistoryPoints.Columns[0].Name = "STT";
			dtgHistoryPoints.Columns[1].Name = "Id";
			dtgHistoryPoints.Columns[1].Visible = false;
			dtgHistoryPoints.Columns[2].Name = "Point Used";
			dtgHistoryPoints.Columns[3].Name = "CreatedDate";
			dtgHistoryPoints.Columns[4].Name = "Customer";
			dtgHistoryPoints.Rows.Clear();

			int count = 1;
			foreach (var item in _historypointService.GetAllhtrPointFromDb().Where(x => x.Point > 0 && x.CustomerId == Id).OrderBy(x=>x.CreatedDate))
			{
				dtgHistoryPoints.Rows.Add(count++, item.Id, item.Point?.ToString("0"), item.CreatedDate, _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.Name).FirstOrDefault());
			}
		}
		 void LoadHistoryPoint(Guid Id, DateTime? startDate = null, DateTime? endDate = null)
		{
			//dtgHistoryPoints.ColumnCount = 5;
			//dtgHistoryPoints.Columns[0].Name = "STT";
			//dtgHistoryPoints.Columns[1].Name = "Id";
			//dtgHistoryPoints.Columns[1].Visible = false;
			//dtgHistoryPoints.Columns[2].Name = "Point Used";
			//dtgHistoryPoints.Columns[3].Name = "CreatedDate";
			//dtgHistoryPoints.Columns[4].Name = "Customer";
			dtgHistoryPoints.Rows.Clear();

            //var historyPoints = _historypointService.GetAllhtrPointFromDb()
            //					.Where(x => x.CustomerId == Id && x.Point > 0
            //							&& (startDate.HasValue || x.CreatedDate >= startDate.Value)
            //							&& (endDate.HasValue || x.CreatedDate <= endDate.Value))
            //					.ToList();
            var historyPoints = _historypointService.GetAllhtrPointFromDb()
				.Where(x => x.CustomerId == Id && x.Point > 0
				&& (startDate.HasValue && x.CreatedDate >= startDate.Value)
				&& (endDate.HasValue && x.CreatedDate <= endDate.Value))
				.ToList();

            int count = 1;
			foreach (var item in historyPoints)
			{
				dtgHistoryPoints.Rows.Add(count++, item.Id, item.Point?.ToString("0"), item.CreatedDate, _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.Name).FirstOrDefault());
			}
		}
		private void dtStartdate_ValueChanged(object sender, EventArgs e)
		{
			if (dtStartdate.Value.Date > dtEndDate.Value.Date)
			{
				MessageBox.Show("Start date cannot be later than end date.");
				return;
			}

			LoadHistoryPoint(selectedCustomerId, dtStartdate.Value, dtEndDate.Value);
		}

		private void dtEndDate_ValueChanged(object sender, EventArgs e)
		{

			if (dtStartdate.Value.Date > dtEndDate.Value.Date)
			{
				MessageBox.Show("End date cannot be earlier than start date.");
				return;
			}

			LoadHistoryPoint(selectedCustomerId, dtStartdate.Value, dtEndDate.Value);
		}
	}
}


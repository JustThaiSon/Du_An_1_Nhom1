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
    public partial class frmCustomer : Form
    {
        private ICustomerService _customerService;
        private Guid selectedCustomerId = Guid.Empty;

        public frmCustomer()
        {
            _customerService = new CustomerService();
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
                dtGV_Customer.Rows.Add(Count, item.Id, item.CustomerCode, item.Name, item.PhoneNumber, item.Email, item.Address, item.Gender, item.CCCD, item.Point);
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
            lb_add_Id.Text = "---";
            txt_add_CustomerCode.Clear();
            txt_add_Name.Clear();
            txt_add_Email.Clear();
            txt_add_Adress.Clear();
            txt_add_PhoneNumber.Clear();
            txt_add_CCCD.Clear();
            cbb_add_Gender.SelectedIndex = 0;
            NUD_add_Point.Value = 0;
        }
        public void edit_Clear()
        {
            lb_edit_Id.Text = "---";
            txt_edit_CustomerCode.Clear();
            txt_edit_Name.Clear();
            txt_edit_Email.Clear();
            txt_edit_Adress.Clear();
            txt_edit_PhoneNumber.Clear();
            txt_edit_CCCD.Clear();
            cbb_edit_Gender.SelectedIndex = 0;
            NUD_edit_Point.Value = 0;
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
            .Where(x => x.Name.Contains(txtSearch.Text) || x.CCCD.StartsWith(txtSearch.Text) || x.CustomerCode.Contains(txtSearch.Text)).ToList();

            foreach (var item in listCustomer)
            {
                Count++;
                dtGV_Customer.Rows.Add(Count, item.Id, item.CustomerCode, item.Name, item.PhoneNumber, item.Email, item.Address, item.Gender, item.CCCD, item.Point);
            }
        }

        private void btn_list_Add_Click(object sender, EventArgs e)
        {
            tab_Customer.SelectedTab = tabPage_AddCustomer;
        }

        private void btn_add_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_add_Name.Text) || txt_add_Name.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Tên không được Để Trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_add_Adress.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_add_CCCD.Text) || !txt_add_CCCD.Text.All(char.IsDigit) || txt_add_CCCD.Text.Length != 12)
            {
                MessageBox.Show("CCCD phải là số có 12 chữ số.");
                return;
            }
            if (!IsValidEmail(txt_add_Email.Text))
            {
                MessageBox.Show("Email Không Hợp Lệ!!!.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_add_PhoneNumber.Text) || !txt_add_PhoneNumber.Text.All(char.IsDigit) || txt_add_PhoneNumber.Text.Length != 10 || !txt_add_PhoneNumber.Text.StartsWith("0"))
            {
                MessageBox.Show("PhoneNumber phải có 10 chữ số và bắt đầu từ số 0.");
                return;
            }
            if (NUD_add_Point.Value < 0)
            {
                MessageBox.Show("Point phải có lớn hơn hoặc bằng 0.");
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
                Point = int.Parse(NUD_add_Point.Value.ToString())
            };

            MessageBox.Show(_customerService.AddCustomer(customer), "Thông Báo");
            this.add_Clear();
            Load_dtGV_Customer();
            tab_Customer.SelectedTab = tabPage_ListCustomer;
        }

        private void bnt_list_edit_Click(object sender, EventArgs e)
        {
            var customer = _customerService.GetAllCustomerFromDb().FirstOrDefault(x => x.Id == selectedCustomerId);
            if (customer == null)
            {
                MessageBox.Show("Vui lòng chọn 1 Customer để chỉnh sửa!!");
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
                NUD_edit_Point.Value = customer.Point;
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
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == null)
            {
                MessageBox.Show("Customer không tồn tại!!!");
                return;
            }
            DialogResult = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                _customerService.RemoveCustomer(selectedCustomerId);
                MessageBox.Show("Xóa Customer Thành Công!!!");
            }
            this.edit_Clear();
            this.add_Clear();
            this.Load_dtGV_Customer();
        }

        private void btn_edit_Edit_Click(object sender, EventArgs e)
        {
            var customer = _customerService.GetAllCustomerFromDb().FirstOrDefault(x => x.Id == selectedCustomerId);
            string cccd = txt_edit_CCCD.Text;
            string Sdt = txt_edit_PhoneNumber.Text;

            if (string.IsNullOrWhiteSpace(txt_edit_Name.Text) || txt_edit_Name.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Tên không được chứa ký tự đặc biệt.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_edit_Adress.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cccd) || !cccd.All(char.IsDigit) || cccd.Length != 12)
            {
                MessageBox.Show("CCCD phải là số có 12 chữ số.");
                return;
            }

            if (!IsValidEmail(txt_edit_Email.Text))
            {
                MessageBox.Show("Email Không Hợp Lệ!!!.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Sdt) || !Sdt.All(char.IsDigit) || Sdt.Length != 10 || !Sdt.StartsWith("0"))
            {
                MessageBox.Show("PhoneNumber phải có 10 chữ số và bắt đầu từ số 0.");
                return;
            }
            
            MenuGender selectedGender = (MenuGender)Enum.Parse(typeof(MenuGender), cbb_edit_Gender.SelectedItem.ToString());

            customer.Name = txt_edit_Name.Text;
            customer.Address = txt_edit_Adress.Text;
            customer.CCCD = cccd;
            customer.PhoneNumber = Sdt;
            customer.Email = txt_edit_Email.Text;
            customer.Gender = selectedGender;
            customer.Point=int.Parse(NUD_edit_Point.Value.ToString());
            MessageBox.Show(_customerService.UpdateCustomer(customer), "Thông Báo");
            this.edit_Clear();
            Load_dtGV_Customer();
            tab_Customer.SelectedTab = tabPage_ListCustomer;
        }

    }
}


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
        ICustomerService sv;

        public frmCustomer()
        {
            sv = new CustomerService();
            InitializeComponent();
        }
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            cbb_CustomerGender.Items.AddRange(Enum.GetNames(typeof(MenuGender)));
            loaddata();

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

        private void btn_add_Click(object sender, EventArgs e)
        {
            // Xác thực giới tính
            if (cbb_CustomerGender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

            // Xác thực tên
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                txtCustomerName.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Name should not contain special characters.");
                return;
            }

            // Xác thực số điện thoại
            string phoneNumber = txt_CustomerphoneNum.Text;
            if (string.IsNullOrWhiteSpace(phoneNumber) ||
                !phoneNumber.All(char.IsDigit) ||
                phoneNumber.Length < 10 ||
                phoneNumber.Length > 13)
            {
                MessageBox.Show("Phone number must be a number with length between 10 and 13 digits.");
                return;
            }

            // Xác thực email
            if (!IsValidEmail(txtCustomerEmail.Text))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            // Xác thực CCCD
            string cccd = txtCustomerCCCD.Text;
            if (string.IsNullOrWhiteSpace(cccd) ||
                !cccd.All(char.IsDigit) ||
                cccd.Length != 12)
            {
                MessageBox.Show("CCCD must be a 12-digit number.");
                return;
            }

            // Nếu tất cả các kiểm tra đều hợp lệ
            MenuGender selectedGender = (MenuGender)Enum.Parse(typeof(MenuGender), cbb_CustomerGender.SelectedItem.ToString());
            Customer customer = new Customer()
            {
                Name = txtCustomerName.Text,
                PhoneNumber = phoneNumber,
                Address = txt_CustomerAddress.Text,
                Email = txtCustomerEmail.Text,
                Gender = selectedGender,
                CCCD = cccd,
                Point = 0
            };
            MessageBox.Show(sv.AddCustomer(customer), "Notification");
            loaddata();
            ClearCustomerDetails();
        }

        private void SetupDataGridViewColumns()
        {
            guna2DataGridViewCustomer.AutoGenerateColumns = false;
            guna2DataGridViewCustomer.ColumnCount = 9;

            guna2DataGridViewCustomer.Columns[0].Name = "Id";
            guna2DataGridViewCustomer.Columns[0].HeaderText = "Customer ID";
            guna2DataGridViewCustomer.Columns[0].DataPropertyName = "Id";

            guna2DataGridViewCustomer.Columns[1].Name = "CustomerCode";
            guna2DataGridViewCustomer.Columns[1].HeaderText = "Customer Code";
            guna2DataGridViewCustomer.Columns[1].DataPropertyName = "CustomerCode";

            guna2DataGridViewCustomer.Columns[2].Name = "Name";
            guna2DataGridViewCustomer.Columns[2].HeaderText = "Name";
            guna2DataGridViewCustomer.Columns[2].DataPropertyName = "Name";

            guna2DataGridViewCustomer.Columns[3].Name = "PhoneNumber";
            guna2DataGridViewCustomer.Columns[3].HeaderText = "Phone Number";
            guna2DataGridViewCustomer.Columns[3].DataPropertyName = "PhoneNumber";

            guna2DataGridViewCustomer.Columns[4].Name = "Email";
            guna2DataGridViewCustomer.Columns[4].HeaderText = "Email";
            guna2DataGridViewCustomer.Columns[4].DataPropertyName = "Email";

            guna2DataGridViewCustomer.Columns[5].Name = "Address";
            guna2DataGridViewCustomer.Columns[5].HeaderText = "Address";
            guna2DataGridViewCustomer.Columns[5].DataPropertyName = "Address";

            guna2DataGridViewCustomer.Columns[6].Name = "Gender";
            guna2DataGridViewCustomer.Columns[6].HeaderText = "Gender";
            guna2DataGridViewCustomer.Columns[6].DataPropertyName = "Gender";

            guna2DataGridViewCustomer.Columns[7].Name = "CCCD";
            guna2DataGridViewCustomer.Columns[7].HeaderText = "CCCD";
            guna2DataGridViewCustomer.Columns[7].DataPropertyName = "CCCD";

            guna2DataGridViewCustomer.Columns[8].Name = "Point";
            guna2DataGridViewCustomer.Columns[8].HeaderText = "Point";
            guna2DataGridViewCustomer.Columns[8].DataPropertyName = "Point";
        }

        void loaddata()
        {
            List<Customer> customers = sv.GetAllCustomerFromDb();
            guna2DataGridViewCustomer.DataSource = customers;
        }


        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Remove_Customer_Click(object sender, EventArgs e)
        {
            if (guna2DataGridViewCustomer.SelectedRows.Count > 0)
            {
                int selectedIndex = guna2DataGridViewCustomer.SelectedRows[0].Index;
                Guid customerId = (Guid)guna2DataGridViewCustomer.SelectedRows[0].Cells["Id"].Value;

                var confirmResult = MessageBox.Show("Are you sure to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    string result = sv.RemoveCustomer(customerId);
                    MessageBox.Show(result, "Notification");

                    ClearCustomerDetails();

                    loaddata();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Notification");
            }
        }
        private void ClearCustomerDetails()
        {
            txtCustomerName.Text = "";
            txt_CustomerphoneNum.Text = "";
            txt_CustomerAddress.Text = "";
            txtCustomerEmail.Text = "";
            txtCustomerCCCD.Text = "";
            cbb_CustomerGender.SelectedIndex = -1;
        }

        private void guna2DataGridViewCustomer_SelectionChanged(object sender, EventArgs e)
        {

        }

        private string selectedCustomerCode;
        private Guid selectedCustomerId = Guid.Empty;
        private void btn_Edit_Customer_Click(object sender, EventArgs e)
        {
            if (guna2DataGridViewCustomer.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = guna2DataGridViewCustomer.SelectedRows[0];
                Guid customerId = (Guid)selectedRow.Cells["Id"].Value;

                // Xác thực giới tính
                if (cbb_CustomerGender.SelectedItem == null)
                {
                    MessageBox.Show("Please select a gender.");
                    return;
                }

                // Xác thực tên
                if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                    txtCustomerName.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
                {
                    MessageBox.Show("Name should not contain special characters.");
                    return;
                }

                // Xác thực số điện thoại
                string phoneNumber = txt_CustomerphoneNum.Text;
                if (string.IsNullOrWhiteSpace(phoneNumber) ||
                    !phoneNumber.All(char.IsDigit) ||
                    phoneNumber.Length < 10 ||
                    phoneNumber.Length > 13)
                {
                    MessageBox.Show("Phone number must be a number with length between 10 and 13 digits.");
                    return;
                }

                // Xác thực email
                if (!IsValidEmail(txtCustomerEmail.Text))
                {
                    MessageBox.Show("Invalid email format.");
                    return;
                }

                // Xác thực CCCD
                string cccd = txtCustomerCCCD.Text;
                if (string.IsNullOrWhiteSpace(cccd) ||
                    !cccd.All(char.IsDigit) ||
                    cccd.Length != 12)
                {
                    MessageBox.Show("CCCD must be a 12-digit number.");
                    return;
                }

                // Tạo đối tượng Customer với dữ liệu đã chỉnh sửa
                Customer updatedCustomer = new Customer()
                {
                    Id = customerId,
                    CustomerCode = selectedRow.Cells["CustomerCode"].Value.ToString(), // Giữ nguyên mã khách hàng
                    Name = txtCustomerName.Text,
                    PhoneNumber = phoneNumber,
                    Address = txt_CustomerAddress.Text,
                    Email = txtCustomerEmail.Text,
                    Gender = (MenuGender)Enum.Parse(typeof(MenuGender), cbb_CustomerGender.SelectedItem.ToString()),
                    CCCD = cccd,
                    Point = int.Parse(selectedRow.Cells["Point"].Value.ToString()) // Giữ nguyên số điểm hiện tại
                };

                // Cập nhật khách hàng
                string result = sv.UpdateCustomer(updatedCustomer);
                MessageBox.Show(result, "Notification");

                ClearCustomerDetails();
                loaddata();
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.", "Notification");
            }
        }

        private void guna2DataGridViewCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridViewCustomer.Rows[e.RowIndex];
                selectedCustomerId = (Guid)row.Cells["Id"].Value;
                selectedCustomerCode = row.Cells["CustomerCode"].Value.ToString();
                txtCustomerName.Text = row.Cells["Name"].Value.ToString();
                txt_CustomerphoneNum.Text = row.Cells["PhoneNumber"].Value.ToString();
                txt_CustomerAddress.Text = row.Cells["Address"].Value.ToString();
                txtCustomerEmail.Text = row.Cells["Email"].Value.ToString();
                txtCustomerCCCD.Text = row.Cells["CCCD"].Value.ToString();
                cbb_CustomerGender.SelectedItem = row.Cells["Gender"].Value.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtShare_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            string searchText = txtShare.Text.Trim().ToLower();

            // Lấy tất cả khách hàng từ cơ sở dữ liệu
            List<Customer> allCustomers = sv.GetAllCustomerFromDb();

            // Lọc danh sách khách hàng theo tên và mã khách hàng
            var filteredCustomers = allCustomers
                .Where(c => c.Name != null && c.Name.ToLower().Contains(searchText) ||
                            c.CustomerCode != null && c.CustomerCode.ToLower().Contains(searchText))
                .ToList();

            // Cập nhật DataGridView với dữ liệu đã lọc
            guna2DataGridViewCustomer.DataSource = filteredCustomers;
        }
    }
}


using BUS.IService;
using BUS.Service;
using DAL.Entities;
using DAL.Enums;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyPhong
{
    public partial class frmNhanVien : Form
    {
        private IEmployeeService _employeeService;
        private IRoleService _roleService;
        public RoleRepository Role = new RoleRepository();
        Guid IdCell = Guid.Empty;
        private string selectedEmployeeCode;
        private Guid selectedEmployeeId = Guid.Empty;
        private Guid selectedRoleId = Guid.Empty;
        private byte[] img;
        private string path_img= "C:\\Users\\UBC\\Desktop\\Picture Tạm Thời";
        public frmNhanVien()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _roleService = new RoleService();
            this.load_Role();
            this.load_Status();
            this.load_Gender();
            this.Load_dtGV_Employee();
        }
        public void load_Gender()
        {
            foreach (MenuGender value in Enum.GetValues(typeof(MenuGender)))
            {
                cb_Gender.Items.Add(value);
                cbb_add_Gender.Items.Add(value);
            }
        }
        public void load_Status()
        {
            cbb_Status.Items.Add("false");
            cbb_Status.Items.Add("true");
            cbb_add_Status.Items.Add("false");
            cbb_add_Status.Items.Add("true");
        }
        public void load_Role()
        {
            foreach (Role role in _roleService.GetAllRoleFromDb())
            {
                cbb_Role.Items.Add(role.RoleName);
                cbb_add_Role.Items.Add(role.RoleName);
            }
        }
        private void AddNhanVien_Load(object sender, EventArgs e)
        {

        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var employee = _employeeService.GetAllEmployeeFromDb().FirstOrDefault(x => x.Id == selectedEmployeeId);
            var role_Name = _roleService.GetAllRoleFromDb().FirstOrDefault(x => x.Id == employee.RoleId).RoleName;
            if (employee == null)
            {
                MessageBox.Show("Vui lòng chọn 1 Employee để chỉnh sửa!!");
            }
            else
            {
                tabControl1.SelectedTab = tabPage3;
                cbb_Status.Text = employee.Status.ToString();
                lb_Id.Text = employee.Id.ToString();
                cbb_Role.Text = role_Name;
                txt_Name.Text = employee.Name;
                txt_Email.Text = employee.Email;
                txt_Adress.Text = employee.Address;
                txt_PhoneNumber.Text = employee.PhoneNumber;
                txt_UserName.Text = employee.UserName;
                txt_PassWord.Text = employee.PassWord;
                dtP_DateOfBirth.Text = employee.DateOfBirth.ToString();
                txt_CCCD.Text = employee.CCCD;
                txt_EmployeeCode.Text = employee.EmployeeCode.ToString();
                cb_Gender.Text = employee.Gender.ToString();
                string tempFilePath = Path.GetTempFileName();

                File.WriteAllBytes(tempFilePath, employee.Img);

                ptb_Employee.ImageLocation = tempFilePath;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string cccd = txt_add_CCCD.Text;
            string Sdt = txt_add_PhoneNumber.Text;
            bool status = false;
            DateTime dateOfBirth = dtP_add_DateOfBirth.Value;
            Guid role = Guid.Empty;
            var listCCCD = _employeeService.GetAllEmployeeFromDb().Select(x => x.CCCD);

            if (string.IsNullOrWhiteSpace(txt_add_Name.Text) || txt_Name.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Tên không được Để Trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_add_Adress.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_add_UserName.Text))
            {
                MessageBox.Show("Tài Khoản Không Được Để Trống!!!.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_add_PassWord.Text))
            {
                MessageBox.Show("Mật Khẩu Không Được Để Trống!!!.");
                return;
            }
            if (string.IsNullOrWhiteSpace(cccd) || !cccd.All(char.IsDigit) || cccd.Length != 12 || listCCCD.Any(x => x == cccd))
            {
                MessageBox.Show("CCCD phải là số có 12 chữ số Hoặc CCCD đã tồn tại!!!");
                return;
            }
            if (!IsValidEmail(txt_add_Email.Text))
            {
                MessageBox.Show("Email Không Hợp Lệ!!!.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Sdt) || !Sdt.All(char.IsDigit) || Sdt.Length != 10 || !Sdt.StartsWith("0"))
            {
                MessageBox.Show("PhoneNumber phải có 10 chữ số và bắt đầu từ số 0.");
                return;
            }
            if (!DateTime.TryParse(dtP_add_DateOfBirth.Value.ToString(), out dateOfBirth) ||
            dateOfBirth > DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.");
                return;
            }
            foreach (var item in _roleService.GetAllRoleFromDb())
            {
                if (item.RoleName == cbb_add_Role.Text)
                {
                    role = item.Id;
                }
            }
            MenuGender selectedGender = (MenuGender)Enum.Parse(typeof(MenuGender), cbb_add_Gender.SelectedItem.ToString());
            if (cbb_add_Status.SelectedIndex == 1)
            {
                status = true;
            }
            if (img == null)
            {
                MessageBox.Show("Không được để trống Img!!!");
                return;
            }
            var employee = new Employee()
            {
                Name = txt_add_Name.Text,
                RoleId = role,
                Email = txt_add_Email.Text,
                Address = txt_add_Adress.Text,
                PhoneNumber = txt_add_PhoneNumber.Text,
                Status = status,
                UserName = txt_add_UserName.Text,
                PassWord = txt_add_PassWord.Text,
                Img = img,
                DateOfBirth = dateOfBirth,
                CCCD = txt_add_CCCD.Text,
                Gender = selectedGender,
            };

            MessageBox.Show(_employeeService.AddEmployee(employee), "Thông Báo");
            this.add_Clear();
            Load_dtGV_Employee();
            tabControl1.SelectedTab = tabPage2;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            var employee = _employeeService.GetAllEmployeeFromDb().FirstOrDefault(x => x.Id == Guid.Parse(lb_Id.Text));
            var role_Name = _roleService.GetAllRoleFromDb().FirstOrDefault(x => x.Id == employee.RoleId).RoleName;
            var listCCCD = _employeeService.GetAllEmployeeFromDb().Select(x => x.CCCD);

            if (string.IsNullOrWhiteSpace(txt_Name.Text) || txt_Name.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Tên không được chứa ký tự đặc biệt.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Adress.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_UserName.Text))
            {
                MessageBox.Show("Tài Khoản Không Được Để Trống!!!.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_PassWord.Text))
            {
                MessageBox.Show("Mật Khẩu Không Được Để Trống!!!.");
                return;
            }

            string cccd = txt_CCCD.Text;
            if (string.IsNullOrWhiteSpace(cccd) || !cccd.All(char.IsDigit) || cccd.Length != 12 || listCCCD.Any(x=>x == cccd))
            {
                MessageBox.Show("CCCD phải là số có 12 chữ số.");
                return;
            }

            if (!IsValidEmail(txt_Email.Text))
            {
                MessageBox.Show("Email Không Hợp Lệ!!!.");
                return;
            }
            string Sdt = txt_PhoneNumber.Text;
            if (string.IsNullOrWhiteSpace(Sdt) || !Sdt.All(char.IsDigit) || Sdt.Length != 10 || !Sdt.StartsWith("0"))
            {
                MessageBox.Show("PhoneNumber phải có 10 chữ số và bắt đầu từ số 0.");
                return;
            }

            DateTime dateOfBirth = dtP_DateOfBirth.Value;
            if (!DateTime.TryParse(dtP_DateOfBirth.Value.ToString(), out dateOfBirth) ||
            dateOfBirth > DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.");

                return;
            }

            foreach (var item in _roleService.GetAllRoleFromDb())
            {
                if (item.RoleName == cbb_Role.Text)
                {
                    employee.RoleId = item.Id;
                }
            }
            MenuGender selectedGender = (MenuGender)Enum.Parse(typeof(MenuGender), cb_Gender.SelectedItem.ToString());
            employee.Status = false;
            if (cbb_Status.SelectedIndex == 1)
            {
                employee.Status = true;
            }
            var img = File.ReadAllBytes(ptb_Employee.ImageLocation);
            employee.Name = txt_Name.Text;
            employee.Address = txt_Adress.Text;
            employee.UserName = txt_UserName.Text;
            employee.PassWord = txt_PassWord.Text;
            employee.CCCD = txt_CCCD.Text;
            employee.PhoneNumber = txt_PhoneNumber.Text;
            employee.Email = txt_Email.Text;
            employee.DateOfBirth = dateOfBirth;
            employee.Gender = selectedGender;
            employee.Img = img;
            MessageBox.Show(_employeeService.UpdateEmployee(employee), "Thông Báo");
            this.edit_Clear();
            Load_dtGV_Employee();
            tabControl1.SelectedTab = tabPage2;
        }

        private void Load_dtGV_Employee()
        {
            dtGV_Employee.Rows.Clear();
            dtGV_Employee.ColumnCount = 15;
            dtGV_Employee.Columns[0].Name = "STT";
            dtGV_Employee.Columns[1].Name = "Id";
            dtGV_Employee.Columns[1].Visible = false;
            dtGV_Employee.Columns[2].Name = "EmployeeCode";
            dtGV_Employee.Columns[3].Name = "Name";
            dtGV_Employee.Columns[4].Name = "Role";
            dtGV_Employee.Columns[5].Name = "Email";
            dtGV_Employee.Columns[6].Name = "Address";
            dtGV_Employee.Columns[7].Name = "PhoneNumber";
            dtGV_Employee.Columns[8].Name = "Status";
            dtGV_Employee.Columns[9].Name = "UserName";
            dtGV_Employee.Columns[10].Name = "PassWord";
            dtGV_Employee.Columns[11].Name = "DateOfBirth";
            dtGV_Employee.Columns[12].Name = "Img";
            dtGV_Employee.Columns[12].Visible = false;
            dtGV_Employee.Columns[13].Name = "CCCD";
            dtGV_Employee.Columns[14].Name = "Gender";

            int Count = 0;
            string role = "";

            foreach (var item in _employeeService.GetAllEmployeeFromDb())
            {
                foreach (Role role1 in _roleService.GetAllRoleFromDb())
                {
                    if (item.RoleId == role1.Id)
                    {
                        role = role1.RoleName;
                    }
                }
                Count++;
                dtGV_Employee.Rows.Add(Count, item.Id, item.EmployeeCode, item.Name, role, item.Email, item.Address, item.PhoneNumber, item.Status, item.UserName, item.PassWord, item.DateOfBirth, item.Img, item.CCCD, item.Gender);
            }
        }

        private void dtGV_Employee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtGV_Employee.Rows.Count - 1)
            {
                selectedEmployeeId = (Guid)dtGV_Employee.Rows[e.RowIndex].Cells[1].Value;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dtGV_Employee.Rows.Clear();
            dtGV_Employee.ColumnCount = 15;
            dtGV_Employee.Columns[0].Name = "STT";
            dtGV_Employee.Columns[1].Name = "Id";
            dtGV_Employee.Columns[1].Visible = false;
            dtGV_Employee.Columns[2].Name = "EmployeeCode";
            dtGV_Employee.Columns[3].Name = "Name";
            dtGV_Employee.Columns[4].Name = "Role";
            dtGV_Employee.Columns[5].Name = "Email";
            dtGV_Employee.Columns[6].Name = "Address";
            dtGV_Employee.Columns[7].Name = "PhoneNumber";
            dtGV_Employee.Columns[8].Name = "Status";
            dtGV_Employee.Columns[9].Name = "UserName";
            dtGV_Employee.Columns[10].Name = "PassWord";
            dtGV_Employee.Columns[11].Name = "DateOfBirth";
            dtGV_Employee.Columns[12].Name = "Img";
            dtGV_Employee.Columns[12].Visible = false;
            dtGV_Employee.Columns[13].Name = "CCCD";
            dtGV_Employee.Columns[14].Name = "Gender";

            int Count = 0;
            string role = "";
            var listEployeeSearch = _employeeService.GetAllEmployeeFromDb()
             .Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower()) || x.CCCD.StartsWith(txtSearch.Text) || x.EmployeeCode.Contains(txtSearch.Text)).ToList();

            foreach (var item in listEployeeSearch)
            {
                foreach (Role itemrole in _roleService.GetAllRoleFromDb())
                {
                    if (item.RoleId == itemrole.Id)
                    {
                        role = itemrole.RoleName;
                    }
                }
                Count++;
                dtGV_Employee.Rows.Add(Count, item.Id, item.EmployeeCode, item.Name, role, item.Email, item.Address, item.PhoneNumber, item.Status, item.UserName, item.PassWord, item.DateOfBirth, item.Img, item.CCCD, item.Gender);
            }
        }

        private void btn_DeleteEmployee_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == null)
            {
                MessageBox.Show("Employee không tồn tại!!!");
                return;
            }
            DialogResult = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                _employeeService.RemoveEmployee(selectedEmployeeId);
                MessageBox.Show("Xóa Employee Thành Công!!!");
            }
            this.edit_Clear();
            this.add_Clear();
            this.Load_dtGV_Employee();
        }
        private void edit_Clear()
        {
            txt_EmployeeCode.Clear();
            txt_Name.Clear();
            txt_Email.Clear();
            txt_Adress.Clear();
            cbb_Role.SelectedIndex = -1;
            txt_PhoneNumber.Clear();
            txt_UserName.Clear();
            txt_PassWord.Clear();
            txt_CCCD.Clear();
            cb_Gender.SelectedIndex = 2;
            dtP_DateOfBirth.Value = DateTime.Now;
            ptb_Employee.ImageLocation = "C:\\Users\\UBC\\Desktop\\Picture Tạm Thời\\441584290_1147593473252448_4764847968139588065_n.jpg";
        }
        private void add_Clear()
        {
            txt_add_EmployeeCode.Clear();
            txt_add_Name.Clear();
            txt_add_Email.Clear();
            txt_add_Adress.Clear();
            cbb_add_Role.SelectedIndex = -1;
            txt_add_PhoneNumber.Clear();
            txt_add_UserName.Clear();
            txt_add_PassWord.Clear();
            txt_add_CCCD.Clear();
            cbb_add_Gender.SelectedIndex = 2;
            dtP_add_DateOfBirth.Value = DateTime.Now;
            ptB_add_Img.ImageLocation = "C:\\Users\\UBC\\Desktop\\Picture Tạm Thời\\441584290_1147593473252448_4764847968139588065_n.jpg";
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.edit_Clear();
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
        public void Img_edit_click()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())

            {

                openFileDialog.InitialDirectory = path_img;

                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                openFileDialog.FilterIndex = 1;

                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)

                {

                    // Get the path of specified file

                    string filePath = openFileDialog.FileName;

                    // Read the image into a byte array, if needed

                    byte[] imgData = File.ReadAllBytes(filePath);
                    ptb_Employee.ImageLocation = filePath;


                    img = imgData;

                }

            }
        }
        public void Img_add_click()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())

            {
                //File chứa Ảnh
                openFileDialog.InitialDirectory = path_img;

                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                openFileDialog.FilterIndex = 1;

                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)

                {

                    // Get the path of specified file

                    string filePath = openFileDialog.FileName;

                    // Read the image into a byte array, if needed

                    byte[] imgData = File.ReadAllBytes(filePath);
                    ptB_add_Img.ImageLocation = filePath;

                    img = imgData;

                }

            }
        }
        private void ptB_add_Click(object sender, EventArgs e)
        {
            this.Img_add_click();
        }
        private void ptb_Employee_Click(object sender, EventArgs e)
        {
            this.Img_edit_click();
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            add_Clear();
        }
    }
}

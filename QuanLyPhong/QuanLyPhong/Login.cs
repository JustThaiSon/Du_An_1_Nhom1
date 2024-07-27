using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhong
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var employee = context.Employees
                    .Include(e => e.Role) // Include Role navigation property
                    .FirstOrDefault(e => e.UserName == tb_username.Text && e.PassWord == tb_password.Text);

                if (employee != null)
                {
                    lb_error.Visible = false;

                    // Lưu thông tin phiên vào lớp Session
                    Session.UserName = employee.UserName;
                    Session.UserId = employee.Id;
                    Session.EmployeeCode = employee.EmployeeCode;
                    Session.Name = employee.Name;
                    Session.RoleCode = employee.Role?.RoleCode; // RoleCode của nhân viên
                    Session.PassWord = employee.PassWord;

                    TrangChu tc = new TrangChu();
                    this.Hide();
                    tc.Show();
                }
                else
                {
                    lb_error.Visible = true;
                    tb_password.Clear();
                }
            }
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

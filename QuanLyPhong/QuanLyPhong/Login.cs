using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhong
{
    public partial class Login : Form
    {
        private readonly MyDbContext _context;

        public Login()
        {
            InitializeComponent();
            _context = new MyDbContext(); 
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var employee = context.Employees
                    .Include(e => e.Role) 
                    .FirstOrDefault(e => e.UserName == tb_username.Text && e.PassWord == tb_password.Text);

                if (employee != null)
                {
                    lb_error.Visible = false;

                    Session.UserName = employee.UserName;
                    Session.UserId = employee.Id;
                    Session.EmployeeCode = employee.EmployeeCode;
                    Session.Name = employee.Name;
                    Session.RoleCode = employee.Role?.RoleCode; 
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

        private void btn_forgotPassword_Click(object sender, EventArgs e)
        {
            frmForgotPassword forgotPasswordForm = new frmForgotPassword(_context);
            forgotPasswordForm.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(Login_KeyDown);
            this.KeyPreview = true;
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(sender, e);
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lb_error_Click(object sender, EventArgs e)
        {

        }
    }
}

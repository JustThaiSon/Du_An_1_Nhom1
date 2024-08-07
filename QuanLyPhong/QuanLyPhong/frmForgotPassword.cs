using DAL.Data;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace QuanLyPhong
{
    public partial class frmForgotPassword : Form
    {
        private string generatedCode; 
        private readonly MyDbContext _context; 

        public frmForgotPassword(MyDbContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private void btn_forgotPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_EmailTofogotpass.Text.Trim()))
            {
                MessageBox.Show("Please enter your email.");
                return;
            }
            var user = _context.Employees.FirstOrDefault(u => u.Email == txt_EmailTofogotpass.Text);

            if (user == null)
            {
                MessageBox.Show("Email does not exist.");
                return;
            }

            Random random = new Random();
            generatedCode = random.Next(100000, 999999).ToString();




            MailMessage mail = new MailMessage();
            mail.To.Add(txt_EmailTofogotpass.Text.Trim());
            mail.From = new MailAddress("thaothaobatbai123@gmail.com");
            mail.Subject = "Mã xác nhận của bạn";
            mail.Body = $"Your verification code is {generatedCode}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("thaothaobatbai123@gmail.com", "kaefdapftqcriiwj");

            try
            {
                smtp.Send(mail);
                MessageBox.Show("Email đã được gửi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void submit_Change_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_otp.Text) || string.IsNullOrEmpty(txt_NewPass.Text) || string.IsNullOrEmpty(Re_newpass.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (txt_otp.Text != generatedCode)
            {
                MessageBox.Show("Mã OTP không hợp lệ. Vui lòng kiểm tra lại.");
                return;
            }

            if (txt_NewPass.Text != Re_newpass.Text)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu xác nhận không trùng khớp.");
                return;
            }

            if (!ValidatePassword(txt_NewPass.Text))
            {
                MessageBox.Show("Mật khẩu quá yếu. Mật khẩu phải chứa ít nhất 8 ký tự, bao gồm cả chữ cái in hoa, chữ thường và số.");
                return;
            }

            try
            {
                var user = _context.Employees.FirstOrDefault(u => u.Email == txt_EmailTofogotpass.Text);

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txt_NewPass.Text);
                user.PassWord = hashedPassword;
                _context.SaveChanges();

                MessageBox.Show("Mật khẩu đã được thay đổi thành công.");

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật mật khẩu: " + ex.Message);
            }
        }
        private bool ValidatePassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            return regex.IsMatch(password);
        }
    }
}

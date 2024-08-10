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
            mail.Subject = "Your confirmation code";
            mail.Body = $"Your verification code is {generatedCode}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("thaothaobatbai123@gmail.com", "kaefdapftqcriiwj");

            try
            {
                smtp.Send(mail);
                MessageBox.Show("Email sent successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Please fill in all information.");
                return;
            }

            if (txt_otp.Text != generatedCode)
            {
                MessageBox.Show("Invalid OTP code. Please check again.");
                return;
            }

            if (txt_NewPass.Text != Re_newpass.Text)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            if (!ValidatePassword(txt_NewPass.Text))
            {
                MessageBox.Show("Password is too weak. Password must contain at least 8 characters, including uppercase letters, lowercase letters, and numbers..");
                return;
            }

            try
            {
                var user = _context.Employees.FirstOrDefault(u => u.Email == txt_EmailTofogotpass.Text);

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txt_NewPass.Text);
                user.PassWord = hashedPassword;
                _context.SaveChanges();

                MessageBox.Show("Password has been changed successfully.");

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating password: " + ex.Message);
            }
        }
        private bool ValidatePassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            return regex.IsMatch(password);
        }
    }
}

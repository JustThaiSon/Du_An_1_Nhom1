using DAL.Data;
using System;
using System.Windows.Forms;

namespace QuanLyPhong
{
    public partial class TrangChu : Form
    {
        private int picListOriginalLeft;
        private int picOrderOriginalLeft;
        private int picCustOriginalLeft;
        private int picEmployeeOriginalLeft;
        private int picVoucherOriginalLeft;
        private int picServiceOriginalLeft;
        private int picStatisticalOriginalLeft;
        private int picBookingOriginalLeft;


        //ádfasdffsdfasdfa
        public TrangChu()
        {
            InitializeComponent();
            picListOriginalLeft = picList.Left;
            picOrderOriginalLeft = picOrder.Left;
            picCustOriginalLeft = picCust.Left;
            picEmployeeOriginalLeft = picEmployee.Left;
            picVoucherOriginalLeft = picVoucher.Left;
            picServiceOriginalLeft = picService.Left;
            picStatisticalOriginalLeft = picStatistical.Left;
            picBookingOriginalLeft = picBooking.Left;
        }

        private Form currentFormChild;
        private void OpenChillFrom(Form childForm)
        {
            if (currentFormChild != null) { currentFormChild.Close(); }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PanHienThi.Controls.Add(childForm);
            PanHienThi.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void ResetAllPictures()
        {
            picList.Left = picListOriginalLeft;
            picOrder.Left = picOrderOriginalLeft;
            picCust.Left = picCustOriginalLeft;
            picEmployee.Left = picEmployeeOriginalLeft;
            picVoucher.Left = picVoucherOriginalLeft;
            picService.Left = picServiceOriginalLeft;
            picStatistical.Left = picStatisticalOriginalLeft;
            picBooking.Left = picBookingOriginalLeft;
        }
        private void btn_Exit_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ListRoom_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picList.Left = btn_ListRoom.Left + 30;
            OpenChillFrom(new frmRoom());
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picBooking.Left = guna2Button1.Left + 30;
            OpenChillFrom(new frmBookingRoom());
        }

        private void btn_Order_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picOrder.Left = btn_Order.Left + 30;
            OpenChillFrom(new frmQuanLyOrder());
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picCust.Left = btn_Customer.Left + 30;
            OpenChillFrom(new frmCustomer());
        }

        private void btn_Empolyee_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picEmployee.Left = btn_Empolyee.Left + 30;
            OpenChillFrom(new frmNhanVien());
        }

        private void btn_Vocher_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picVoucher.Left = btn_Vocher.Left + 30;
            OpenChillFrom(new frmVoucher());
        }

        private void btn_service_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picService.Left = btn_service.Left + 30;
            OpenChillFrom(new frmService());
        }

        private void btn_Statistical_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picStatistical.Left = btn_Statistical.Left + 30;
            OpenChillFrom(new frmThongKe());

        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

            labelManhanvien.Text = Session.EmployeeCode;
            labelTenNhanVien.Text = Session.Name;


            if (Session.RoleCode == "emp")
            {
                btn_Empolyee.Visible = false;
                picEmployee.Visible = false;
            }
        }

        private void picStatistical_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Session.EmployeeCode = null;
            Session.Name = null;
            Session.RoleCode = null;

            Login frmLogin = new Login();
            frmLogin.Show();

            this.Close();
        }

       
    }
}

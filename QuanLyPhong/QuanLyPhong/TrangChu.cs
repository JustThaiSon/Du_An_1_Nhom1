using BUS.IService;
using BUS.Service;
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
    public partial class TrangChu : Form
    {
        private int picListOriginalLeft;
        private int picOrderOriginalLeft;
        private int picCustOriginalLeft;
        private int picEmployeeOriginalLeft;
        private int picVoucherOriginalLeft;
        private int picServiceOriginalLeft;
        private int picStatisticalOriginalLeft;
       
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
        }
        private void btn_Exit_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ListRoom_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picList.Left = btn_ListRoom.Left + 30;
            OpenChillFrom(new ListRoom());
        }

        private void btn_Order_Click(object sender, EventArgs e)
        {
            ResetAllPictures();
            picOrder.Left = btn_Order.Left + 30;
            OpenChillFrom(new frmBookingRoom());

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
            OpenChillFrom(new AddNhanVien());

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
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }
    }
}

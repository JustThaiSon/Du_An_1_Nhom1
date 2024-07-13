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
        public TrangChu()
        {
            InitializeComponent();
            picListOriginalLeft = picList.Left;
            picOrderOriginalLeft = picOrder.Left;
            picCustOriginalLeft = picCust.Left;
            picEmployeeOriginalLeft = picEmployee.Left;
            picVoucherOriginalLeft = picVoucher.Left;
            picServiceOriginalLeft = picService.Left;

        }
        private void ResetAllPictures()
        {
            picList.Left = picListOriginalLeft;
            picOrder.Left = picOrderOriginalLeft;
            picCust.Left = picCustOriginalLeft;
            picEmployee.Left = picEmployeeOriginalLeft;
            picVoucher.Left = picVoucherOriginalLeft;
            picService.Left = picServiceOriginalLeft;
        }
        private void btn_Exit_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ListRoom_Click(object sender, EventArgs e)
        {
            ResetAllPictures(); // Đặt lại vị trí tất cả các PictureBox
            picList.Left = btn_ListRoom.Left + 30; // Di chuyển picList
        }

        private void btn_Order_Click(object sender, EventArgs e)
        {
            ResetAllPictures(); // Đặt lại vị trí tất cả các PictureBox
            picOrder.Left = btn_Order.Left + 30; // Di chuyển picOrder
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            ResetAllPictures(); // Đặt lại vị trí tất cả các PictureBox
            picCust.Left = btn_Customer.Left + 30; // Di chuyển picCust
        }

        private void btn_Empolyee_Click(object sender, EventArgs e)
        {
            ResetAllPictures(); // Đặt lại vị trí tất cả các PictureBox
            picEmployee.Left = btn_Empolyee.Left + 30; // Di chuyển picEmployee
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ResetAllPictures(); // Đặt lại vị trí tất cả các PictureBox
            picVoucher.Left = btn_Vocher.Left + 30; // Di chuyển picVoucher
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ResetAllPictures(); // Đặt lại vị trí tất cả các PictureBox
            picService.Left = btn_service.Left + 30; // Di chuyển picService
        }
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

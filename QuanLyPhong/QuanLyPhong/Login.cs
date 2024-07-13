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
			if (tb_username.Text == "nhom1" && tb_password.Text == "123")
			{
				lb_error.Visible = false;
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
}
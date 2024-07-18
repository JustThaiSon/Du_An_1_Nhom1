namespace QuanLyPhong
{
	partial class Login
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            lb_error = new Label();
            btn_login = new Guna.UI2.WinForms.Guna2Button();
            tb_password = new Guna.UI2.WinForms.Guna2TextBox();
            tb_username = new Guna.UI2.WinForms.Guna2TextBox();
            label1 = new Label();
            guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            btn_Exit = new Guna.UI2.WinForms.Guna2CircleButton();
            guna2GradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).BeginInit();
            SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.BackColor = Color.FromArgb(192, 255, 255);
            guna2GradientPanel1.Controls.Add(lb_error);
            guna2GradientPanel1.Controls.Add(btn_login);
            guna2GradientPanel1.Controls.Add(tb_password);
            guna2GradientPanel1.Controls.Add(tb_username);
            guna2GradientPanel1.Controls.Add(label1);
            guna2GradientPanel1.Controls.Add(guna2CirclePictureBox1);
            guna2GradientPanel1.Controls.Add(btn_Exit);
            guna2GradientPanel1.CustomizableEdges = customizableEdges9;
            guna2GradientPanel1.Location = new Point(283, 104);

            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2GradientPanel1.Size = new Size(1112, 550);
            guna2GradientPanel1.TabIndex = 0;
            // 
            // lb_error
            // 
            lb_error.AutoSize = true;
            lb_error.ForeColor = Color.Red;
            lb_error.Location = new Point(717, 437);
            lb_error.Name = "lb_error";
            lb_error.Size = new Size(212, 20);
            lb_error.TabIndex = 7;
            lb_error.Text = "Wrong UserName Or Password";
            lb_error.Visible = false;
            // 
            // btn_login
            // 
            btn_login.BorderRadius = 18;
            btn_login.CustomizableEdges = customizableEdges1;
            btn_login.DisabledState.BorderColor = Color.DarkGray;
            btn_login.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_login.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_login.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_login.FillColor = Color.FromArgb(0, 192, 192);
            btn_login.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_login.ForeColor = Color.Black;
            btn_login.Location = new Point(630, 372);
            btn_login.Name = "btn_login";
            btn_login.PressedColor = Color.Blue;
            btn_login.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_login.Size = new Size(361, 47);
            btn_login.TabIndex = 6;
            btn_login.Text = "LOGIN";
            btn_login.Click += btn_login_Click;
            // 
            // tb_password
            // 
            tb_password.BorderRadius = 18;
            tb_password.CustomizableEdges = customizableEdges3;
            tb_password.DefaultText = "";
            tb_password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_password.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_password.Font = new Font("Segoe UI", 9F);
            tb_password.ForeColor = Color.Black;
            tb_password.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_password.IconLeft = (Image)resources.GetObject("tb_password.IconLeft");
            tb_password.IconLeftSize = new Size(35, 35);
            tb_password.Location = new Point(600, 273);
            tb_password.Margin = new Padding(3, 4, 3, 4);
            tb_password.Name = "tb_password";
            tb_password.PasswordChar = '*';
            tb_password.PlaceholderText = "Enter Password";
            tb_password.SelectedText = "";
            tb_password.ShadowDecoration.CustomizableEdges = customizableEdges4;
            tb_password.Size = new Size(417, 67);
            tb_password.TabIndex = 5;
            tb_password.TextOffset = new Point(20, 0);
            tb_password.TextChanged += guna2TextBox2_TextChanged;
            // 
            // tb_username
            // 
            tb_username.BorderRadius = 18;
            tb_username.CustomizableEdges = customizableEdges5;
            tb_username.DefaultText = "";
            tb_username.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_username.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_username.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_username.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_username.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_username.Font = new Font("Segoe UI", 9F);
            tb_username.ForeColor = Color.Black;
            tb_username.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_username.IconLeft = (Image)resources.GetObject("tb_username.IconLeft");
            tb_username.IconLeftSize = new Size(35, 35);
            tb_username.Location = new Point(600, 172);
            tb_username.Margin = new Padding(3, 4, 3, 4);
            tb_username.Name = "tb_username";
            tb_username.PasswordChar = '\0';
            tb_username.PlaceholderText = "Enter UserName";
            tb_username.SelectedText = "";
            tb_username.ShadowDecoration.CustomizableEdges = customizableEdges6;
            tb_username.Size = new Size(417, 67);
            tb_username.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            tb_username.TabIndex = 4;
            tb_username.TextOffset = new Point(20, 0);
            tb_username.TextChanged += guna2TextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Algerian", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(728, 94);
            label1.Name = "label1";
            label1.Size = new Size(157, 53);
            label1.TabIndex = 3;
            label1.Text = "LOGIN";
            // 
            // guna2CirclePictureBox1
            // 
            guna2CirclePictureBox1.Image = (Image)resources.GetObject("guna2CirclePictureBox1.Image");
            guna2CirclePictureBox1.ImageRotate = 0F;
            guna2CirclePictureBox1.Location = new Point(74, 77);
            guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            guna2CirclePictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges7;
            guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CirclePictureBox1.Size = new Size(418, 371);
            guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2CirclePictureBox1.TabIndex = 2;
            guna2CirclePictureBox1.TabStop = false;
            // 
            // btn_Exit
            // 
            btn_Exit.DisabledState.BorderColor = Color.DarkGray;
            btn_Exit.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Exit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Exit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Exit.FillColor = Color.White;
            btn_Exit.Font = new Font("Segoe UI", 9F);
            btn_Exit.ForeColor = Color.White;
            btn_Exit.Image = (Image)resources.GetObject("btn_Exit.Image");
            btn_Exit.ImageSize = new Size(35, 35);
            btn_Exit.Location = new Point(1078, 0);
            btn_Exit.Name = "btn_Exit";
            btn_Exit.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btn_Exit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btn_Exit.Size = new Size(34, 35);
            btn_Exit.TabIndex = 1;
            btn_Exit.Click += guna2CircleButton1_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1866, 663);
            Controls.Add(guna2GradientPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            Text = "Form1";
            guna2GradientPanel1.ResumeLayout(false);
            guna2GradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
		private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
		private Guna.UI2.WinForms.Guna2CircleButton btn_Exit;
		private Guna.UI2.WinForms.Guna2TextBox tb_username;
		private Label label1;
		private Guna.UI2.WinForms.Guna2TextBox tb_password;
		private Label lb_error;
		private Guna.UI2.WinForms.Guna2Button btn_login;
	}
}

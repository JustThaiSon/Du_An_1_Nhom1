namespace QuanLyPhong
{
    partial class frmKindOfRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKindOfRoom));
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			panel1 = new Panel();
			txtName = new Guna.UI2.WinForms.Guna2TextBox();
			label2 = new Label();
			button3 = new Button();
			button2 = new Button();
			button1 = new Button();
			txtHour = new Guna.UI2.WinForms.Guna2TextBox();
			label1 = new Label();
			txtDay = new Guna.UI2.WinForms.Guna2TextBox();
			label3 = new Label();
			DTgrvkindofroom = new DataGridView();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DTgrvkindofroom).BeginInit();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(txtName);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(button3);
			panel1.Controls.Add(button2);
			panel1.Controls.Add(button1);
			panel1.Controls.Add(txtHour);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(txtDay);
			panel1.Controls.Add(label3);
			panel1.Location = new Point(402, 19);
			panel1.Margin = new Padding(3, 2, 3, 2);
			panel1.Name = "panel1";
			panel1.Size = new Size(288, 301);
			panel1.TabIndex = 3;
			// 
			// txtName
			// 
			txtName.CustomizableEdges = customizableEdges1;
			txtName.DefaultText = "";
			txtName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			txtName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			txtName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			txtName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			txtName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			txtName.Font = new Font("Segoe UI", 9F);
			txtName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			txtName.Location = new Point(34, 58);
			txtName.Name = "txtName";
			txtName.PasswordChar = '\0';
			txtName.PlaceholderText = "";
			txtName.SelectedText = "";
			txtName.ShadowDecoration.CustomizableEdges = customizableEdges2;
			txtName.Size = new Size(204, 27);
			txtName.TabIndex = 30;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label2.ForeColor = SystemColors.HotTrack;
			label2.Location = new Point(34, 27);
			label2.Name = "label2";
			label2.Size = new Size(150, 21);
			label2.TabIndex = 27;
			label2.Text = "Mane KindOfRoom";
			// 
			// button3
			// 
			button3.Image = (Image)resources.GetObject("button3.Image");
			button3.Location = new Point(203, 254);
			button3.Margin = new Padding(3, 2, 3, 2);
			button3.Name = "button3";
			button3.Size = new Size(49, 38);
			button3.TabIndex = 26;
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button2
			// 
			button2.Image = (Image)resources.GetObject("button2.Image");
			button2.Location = new Point(116, 254);
			button2.Margin = new Padding(3, 2, 3, 2);
			button2.Name = "button2";
			button2.Size = new Size(49, 38);
			button2.TabIndex = 25;
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button1
			// 
			button1.Image = (Image)resources.GetObject("button1.Image");
			button1.Location = new Point(33, 254);
			button1.Margin = new Padding(3, 2, 3, 2);
			button1.Name = "button1";
			button1.Size = new Size(49, 38);
			button1.TabIndex = 24;
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// txtHour
			// 
			txtHour.CustomizableEdges = customizableEdges3;
			txtHour.DefaultText = "";
			txtHour.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			txtHour.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			txtHour.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			txtHour.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			txtHour.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			txtHour.Font = new Font("Segoe UI", 9F);
			txtHour.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			txtHour.Location = new Point(34, 186);
			txtHour.Name = "txtHour";
			txtHour.PasswordChar = '\0';
			txtHour.PlaceholderText = "";
			txtHour.SelectedText = "";
			txtHour.ShadowDecoration.CustomizableEdges = customizableEdges4;
			txtHour.Size = new Size(204, 27);
			txtHour.TabIndex = 12;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.HotTrack;
			label1.Location = new Point(34, 162);
			label1.Name = "label1";
			label1.Size = new Size(102, 21);
			label1.TabIndex = 11;
			label1.Text = "PriceByHour";
			// 
			// txtDay
			// 
			txtDay.CustomizableEdges = customizableEdges5;
			txtDay.DefaultText = "";
			txtDay.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			txtDay.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			txtDay.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			txtDay.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			txtDay.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			txtDay.Font = new Font("Segoe UI", 9F);
			txtDay.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			txtDay.Location = new Point(34, 132);
			txtDay.Name = "txtDay";
			txtDay.PasswordChar = '\0';
			txtDay.PlaceholderText = "";
			txtDay.SelectedText = "";
			txtDay.ShadowDecoration.CustomizableEdges = customizableEdges6;
			txtDay.Size = new Size(204, 27);
			txtDay.TabIndex = 10;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label3.ForeColor = SystemColors.HotTrack;
			label3.Location = new Point(34, 97);
			label3.Name = "label3";
			label3.Size = new Size(100, 21);
			label3.TabIndex = 9;
			label3.Text = "PricePerDay";
			// 
			// DTgrvkindofroom
			// 
			DTgrvkindofroom.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DTgrvkindofroom.Location = new Point(22, 19);
			DTgrvkindofroom.Margin = new Padding(3, 2, 3, 2);
			DTgrvkindofroom.Name = "DTgrvkindofroom";
			DTgrvkindofroom.RowHeadersWidth = 51;
			DTgrvkindofroom.Size = new Size(359, 292);
			DTgrvkindofroom.TabIndex = 2;
			DTgrvkindofroom.CellClick += DTgrvkindofroom_CellClick;
			// 
			// frmKindOfRoom
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(700, 338);
			Controls.Add(panel1);
			Controls.Add(DTgrvkindofroom);
			Margin = new Padding(3, 2, 3, 2);
			Name = "frmKindOfRoom";
			Text = "KindOfRoom";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)DTgrvkindofroom).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtName;
        private Guna.UI2.WinForms.Guna2TextBox txtId;
        private Label label4;
        private Label label2;
        private Button button3;
        private Button button2;
        private Button button1;
        private Guna.UI2.WinForms.Guna2TextBox txtHour;
        private Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtDay;
        private Label label3;
        private DataGridView DTgrvkindofroom;
    }
}
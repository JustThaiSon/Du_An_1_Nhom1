﻿namespace QuanLyPhong
{
    partial class frmRoom
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
			components = new System.ComponentModel.Container();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoom));
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			panel1 = new Panel();
			label2 = new Label();
			tb_search = new Guna.UI2.WinForms.Guna2TextBox();
			panel2 = new Panel();
			btn_addTypeRoom = new Button();
			btn_addFloor = new Button();
			btnUpdate = new Button();
			cbbStatus = new Guna.UI2.WinForms.Guna2ComboBox();
			btn_addRoom = new Button();
			cbb_typeroom = new Guna.UI2.WinForms.Guna2ComboBox();
			cbb_floor = new Guna.UI2.WinForms.Guna2ComboBox();
			label1 = new Label();
			guna2TextBox5 = new Guna.UI2.WinForms.Guna2TextBox();
			label6 = new Label();
			label5 = new Label();
			tb_nameroom = new Guna.UI2.WinForms.Guna2TextBox();
			label4 = new Label();
			label3 = new Label();
			dtgPhong = new DataGridView();
			guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
			cbbStatusfilter = new Guna.UI2.WinForms.Guna2ComboBox();
			cbbFloorFilter = new Guna.UI2.WinForms.Guna2ComboBox();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dtgPhong).BeginInit();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.ControlLightLight;
			panel1.Controls.Add(label2);
			panel1.Controls.Add(tb_search);
			panel1.Location = new Point(41, 39);
			panel1.Name = "panel1";
			panel1.Size = new Size(846, 133);
			panel1.TabIndex = 0;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.ForeColor = SystemColors.HotTrack;
			label2.Location = new Point(46, 15);
			label2.Name = "label2";
			label2.Size = new Size(63, 23);
			label2.TabIndex = 2;
			label2.Text = "Search";
			// 
			// tb_search
			// 
			tb_search.CustomizableEdges = customizableEdges1;
			tb_search.DefaultText = "Search in here";
			tb_search.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			tb_search.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			tb_search.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			tb_search.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			tb_search.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			tb_search.Font = new Font("Segoe UI", 9F);
			tb_search.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			tb_search.IconLeft = (Image)resources.GetObject("tb_search.IconLeft");
			tb_search.IconLeftSize = new Size(35, 35);
			tb_search.Location = new Point(46, 45);
			tb_search.Margin = new Padding(3, 5, 3, 5);
			tb_search.Name = "tb_search";
			tb_search.PasswordChar = '\0';
			tb_search.PlaceholderText = "";
			tb_search.SelectedText = "";
			tb_search.ShadowDecoration.CustomizableEdges = customizableEdges2;
			tb_search.Size = new Size(744, 40);
			tb_search.TabIndex = 0;
			tb_search.TextOffset = new Point(15, 0);
			tb_search.TextChanged += guna2TextBox1_TextChanged;
			// 
			// panel2
			// 
			panel2.BackColor = SystemColors.ControlLightLight;
			panel2.Controls.Add(btn_addTypeRoom);
			panel2.Controls.Add(btn_addFloor);
			panel2.Controls.Add(btnUpdate);
			panel2.Controls.Add(cbbStatus);
			panel2.Controls.Add(btn_addRoom);
			panel2.Controls.Add(cbb_typeroom);
			panel2.Controls.Add(cbb_floor);
			panel2.Controls.Add(label1);
			panel2.Controls.Add(guna2TextBox5);
			panel2.Controls.Add(label6);
			panel2.Controls.Add(label5);
			panel2.Controls.Add(tb_nameroom);
			panel2.Controls.Add(label4);
			panel2.Controls.Add(label3);
			panel2.Location = new Point(937, 39);
			panel2.Name = "panel2";
			panel2.Size = new Size(517, 899);
			panel2.TabIndex = 1;
			panel2.Paint += panel2_Paint;
			// 
			// btn_addTypeRoom
			// 
			btn_addTypeRoom.Image = (Image)resources.GetObject("btn_addTypeRoom.Image");
			btn_addTypeRoom.Location = new Point(448, 417);
			btn_addTypeRoom.Name = "btn_addTypeRoom";
			btn_addTypeRoom.Size = new Size(48, 36);
			btn_addTypeRoom.TabIndex = 27;
			btn_addTypeRoom.UseVisualStyleBackColor = true;
			btn_addTypeRoom.Click += btn_addTypeRoom_Click;
			// 
			// btn_addFloor
			// 
			btn_addFloor.Image = (Image)resources.GetObject("btn_addFloor.Image");
			btn_addFloor.Location = new Point(448, 307);
			btn_addFloor.Name = "btn_addFloor";
			btn_addFloor.Size = new Size(48, 36);
			btn_addFloor.TabIndex = 26;
			btn_addFloor.UseVisualStyleBackColor = true;
			btn_addFloor.Click += btn_addFloor_Click;
			// 
			// btnUpdate
			// 
			btnUpdate.Location = new Point(283, 669);
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Size = new Size(118, 61);
			btnUpdate.TabIndex = 25;
			btnUpdate.Text = "Update Room";
			btnUpdate.UseVisualStyleBackColor = true;
			btnUpdate.Click += btnUpdate_Click;
			// 
			// cbbStatus
			// 
			cbbStatus.BackColor = Color.Transparent;
			cbbStatus.CustomizableEdges = customizableEdges3;
			cbbStatus.DrawMode = DrawMode.OwnerDrawFixed;
			cbbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			cbbStatus.FocusedColor = Color.FromArgb(94, 148, 255);
			cbbStatus.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			cbbStatus.Font = new Font("Segoe UI", 10F);
			cbbStatus.ForeColor = Color.FromArgb(68, 88, 112);
			cbbStatus.ItemHeight = 30;
			cbbStatus.Location = new Point(47, 512);
			cbbStatus.Name = "cbbStatus";
			cbbStatus.ShadowDecoration.CustomizableEdges = customizableEdges4;
			cbbStatus.Size = new Size(374, 36);
			cbbStatus.TabIndex = 23;
			// 
			// btn_addRoom
			// 
			btn_addRoom.Location = new Point(82, 669);
			btn_addRoom.Name = "btn_addRoom";
			btn_addRoom.Size = new Size(118, 61);
			btn_addRoom.TabIndex = 22;
			btn_addRoom.Text = "Add Room";
			btn_addRoom.UseVisualStyleBackColor = true;
			btn_addRoom.Click += btn_addRoom_Click;
			// 
			// cbb_typeroom
			// 
			cbb_typeroom.BackColor = Color.Transparent;
			cbb_typeroom.CustomizableEdges = customizableEdges5;
			cbb_typeroom.DrawMode = DrawMode.OwnerDrawFixed;
			cbb_typeroom.DropDownStyle = ComboBoxStyle.DropDownList;
			cbb_typeroom.FocusedColor = Color.FromArgb(94, 148, 255);
			cbb_typeroom.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			cbb_typeroom.Font = new Font("Segoe UI", 10F);
			cbb_typeroom.ForeColor = Color.FromArgb(68, 88, 112);
			cbb_typeroom.ItemHeight = 30;
			cbb_typeroom.Location = new Point(45, 405);
			cbb_typeroom.Name = "cbb_typeroom";
			cbb_typeroom.ShadowDecoration.CustomizableEdges = customizableEdges6;
			cbb_typeroom.Size = new Size(377, 36);
			cbb_typeroom.TabIndex = 20;
			cbb_typeroom.Click += cbb_typeroom_Click;
			// 
			// cbb_floor
			// 
			cbb_floor.BackColor = Color.Transparent;
			cbb_floor.CustomizableEdges = customizableEdges7;
			cbb_floor.DrawMode = DrawMode.OwnerDrawFixed;
			cbb_floor.DropDownStyle = ComboBoxStyle.DropDownList;
			cbb_floor.FocusedColor = Color.FromArgb(94, 148, 255);
			cbb_floor.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			cbb_floor.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			cbb_floor.ForeColor = Color.FromArgb(68, 88, 112);
			cbb_floor.ItemHeight = 30;
			cbb_floor.Location = new Point(45, 307);
			cbb_floor.Name = "cbb_floor";
			cbb_floor.ShadowDecoration.CustomizableEdges = customizableEdges8;
			cbb_floor.Size = new Size(377, 36);
			cbb_floor.TabIndex = 19;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.HotTrack;
			label1.Location = new Point(45, 467);
			label1.Name = "label1";
			label1.Size = new Size(72, 28);
			label1.TabIndex = 15;
			label1.Text = "Status";
			// 
			// guna2TextBox5
			// 
			guna2TextBox5.CustomizableEdges = customizableEdges9;
			guna2TextBox5.DefaultText = "";
			guna2TextBox5.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			guna2TextBox5.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			guna2TextBox5.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			guna2TextBox5.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			guna2TextBox5.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			guna2TextBox5.Font = new Font("Segoe UI", 9F);
			guna2TextBox5.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			guna2TextBox5.Location = new Point(187, 701);
			guna2TextBox5.Margin = new Padding(3, 5, 3, 5);
			guna2TextBox5.Name = "guna2TextBox5";
			guna2TextBox5.PasswordChar = '\0';
			guna2TextBox5.PlaceholderText = "";
			guna2TextBox5.SelectedText = "";
			guna2TextBox5.ShadowDecoration.CustomizableEdges = customizableEdges10;
			guna2TextBox5.Size = new Size(0, 0);
			guna2TextBox5.TabIndex = 14;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label6.ForeColor = SystemColors.HotTrack;
			label6.Location = new Point(45, 375);
			label6.Name = "label6";
			label6.Size = new Size(118, 28);
			label6.TabIndex = 11;
			label6.Text = "Type Room";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label5.ForeColor = SystemColors.HotTrack;
			label5.Location = new Point(45, 275);
			label5.Name = "label5";
			label5.Size = new Size(175, 28);
			label5.TabIndex = 9;
			label5.Text = "Number of floors";
			// 
			// tb_nameroom
			// 
			tb_nameroom.CustomizableEdges = customizableEdges11;
			tb_nameroom.DefaultText = "";
			tb_nameroom.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			tb_nameroom.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			tb_nameroom.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			tb_nameroom.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			tb_nameroom.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			tb_nameroom.Font = new Font("Segoe UI", 9F);
			tb_nameroom.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			tb_nameroom.Location = new Point(47, 207);
			tb_nameroom.Margin = new Padding(3, 5, 3, 5);
			tb_nameroom.Name = "tb_nameroom";
			tb_nameroom.PasswordChar = '\0';
			tb_nameroom.PlaceholderText = "";
			tb_nameroom.SelectedText = "";
			tb_nameroom.ShadowDecoration.CustomizableEdges = customizableEdges12;
			tb_nameroom.Size = new Size(374, 36);
			tb_nameroom.TabIndex = 8;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label4.ForeColor = Color.FromArgb(0, 0, 192);
			label4.Location = new Point(122, 45);
			label4.Name = "label4";
			label4.Size = new Size(261, 38);
			label4.TabIndex = 7;
			label4.Text = "Information Room";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label3.ForeColor = SystemColors.HotTrack;
			label3.Location = new Point(45, 175);
			label3.Name = "label3";
			label3.Size = new Size(129, 28);
			label3.TabIndex = 6;
			label3.Text = "Name Room";
			// 
			// dtgPhong
			// 
			dtgPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dtgPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dtgPhong.Location = new Point(41, 265);
			dtgPhong.Name = "dtgPhong";
			dtgPhong.RowHeadersWidth = 51;
			dtgPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dtgPhong.Size = new Size(846, 672);
			dtgPhong.TabIndex = 22;
			dtgPhong.CellClick += dtgPhong_CellClick;
			dtgPhong.CellContentClick += dtgPhong_CellContentClick;
			// 
			// guna2Elipse1
			// 
			guna2Elipse1.BorderRadius = 30;
			guna2Elipse1.TargetControl = this;
			// 
			// cbbStatusfilter
			// 
			cbbStatusfilter.BackColor = Color.Transparent;
			cbbStatusfilter.CustomizableEdges = customizableEdges15;
			cbbStatusfilter.DrawMode = DrawMode.OwnerDrawFixed;
			cbbStatusfilter.DropDownStyle = ComboBoxStyle.DropDownList;
			cbbStatusfilter.FocusedColor = Color.FromArgb(94, 148, 255);
			cbbStatusfilter.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			cbbStatusfilter.Font = new Font("Segoe UI", 10F);
			cbbStatusfilter.ForeColor = Color.FromArgb(68, 88, 112);
			cbbStatusfilter.ItemHeight = 30;
			cbbStatusfilter.Location = new Point(41, 191);
			cbbStatusfilter.Margin = new Padding(3, 4, 3, 4);
			cbbStatusfilter.Name = "cbbStatusfilter";
			cbbStatusfilter.ShadowDecoration.CustomizableEdges = customizableEdges16;
			cbbStatusfilter.Size = new Size(257, 36);
			cbbStatusfilter.TabIndex = 27;
			cbbStatusfilter.SelectedIndexChanged += cbbStatusfilter_SelectedIndexChanged;
			// 
			// cbbFloorFilter
			// 
			cbbFloorFilter.BackColor = Color.Transparent;
			cbbFloorFilter.CustomizableEdges = customizableEdges13;
			cbbFloorFilter.DrawMode = DrawMode.OwnerDrawFixed;
			cbbFloorFilter.DropDownStyle = ComboBoxStyle.DropDownList;
			cbbFloorFilter.FocusedColor = Color.FromArgb(94, 148, 255);
			cbbFloorFilter.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			cbbFloorFilter.Font = new Font("Segoe UI", 10F);
			cbbFloorFilter.ForeColor = Color.FromArgb(68, 88, 112);
			cbbFloorFilter.ItemHeight = 30;
			cbbFloorFilter.Location = new Point(630, 191);
			cbbFloorFilter.Margin = new Padding(3, 4, 3, 4);
			cbbFloorFilter.Name = "cbbFloorFilter";
			cbbFloorFilter.ShadowDecoration.CustomizableEdges = customizableEdges14;
			cbbFloorFilter.Size = new Size(257, 36);
			cbbFloorFilter.TabIndex = 28;
			cbbFloorFilter.SelectedIndexChanged += cbbFloorFilter_SelectedIndexChanged;
			// 
			// frmRoom
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(128, 255, 255);
			ClientSize = new Size(1530, 992);
			Controls.Add(cbbFloorFilter);
			Controls.Add(cbbStatusfilter);
			Controls.Add(dtgPhong);
			Controls.Add(panel2);
			Controls.Add(panel1);
			FormBorderStyle = FormBorderStyle.None;
			Name = "frmRoom";
			Text = "Form3";
			Load += frmRoom_Load;
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dtgPhong).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox tb_search;
        private Label label2;
        private Panel panel2;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox5;
        private Label label6;
        private Label label5;
        private Guna.UI2.WinForms.Guna2TextBox tb_nameroom;
        private Label label4;
        private Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox cbb_floor;
        private Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cbb_typeroom;
        private DataGridView dtgPhong;
        private Button btn_addRoom;
        public Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ComboBox cbbStatus;
        private Button btnUpdate;
        private Button btn_addTypeRoom;
        private Button btn_addFloor;
		private Guna.UI2.WinForms.Guna2ComboBox cbbStatusfilter;
		private Guna.UI2.WinForms.Guna2ComboBox cbbFloorFilter;
	}
}
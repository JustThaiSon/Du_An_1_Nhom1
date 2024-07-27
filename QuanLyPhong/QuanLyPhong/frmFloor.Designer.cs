namespace QuanLyPhong
{
    partial class frmFloor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFloor));
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			DTgrvFloor = new DataGridView();
			panel1 = new Panel();
			button3 = new Button();
			button2 = new Button();
			button1 = new Button();
			txtFloorName = new Guna.UI2.WinForms.Guna2TextBox();
			label1 = new Label();
			((System.ComponentModel.ISupportInitialize)DTgrvFloor).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// DTgrvFloor
			// 
			DTgrvFloor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			DTgrvFloor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DTgrvFloor.Location = new Point(32, 31);
			DTgrvFloor.Margin = new Padding(3, 2, 3, 2);
			DTgrvFloor.Name = "DTgrvFloor";
			DTgrvFloor.RowHeadersWidth = 51;
			DTgrvFloor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			DTgrvFloor.Size = new Size(343, 283);
			DTgrvFloor.TabIndex = 0;
			DTgrvFloor.CellClick += DTgrvFloor_CellClick_1;
			// 
			// panel1
			// 
			panel1.Controls.Add(button3);
			panel1.Controls.Add(button2);
			panel1.Controls.Add(button1);
			panel1.Controls.Add(txtFloorName);
			panel1.Controls.Add(label1);
			panel1.Location = new Point(412, 31);
			panel1.Margin = new Padding(3, 2, 3, 2);
			panel1.Name = "panel1";
			panel1.Size = new Size(246, 283);
			panel1.TabIndex = 1;
			// 
			// button3
			// 
			button3.Image = (Image)resources.GetObject("button3.Image");
			button3.Location = new Point(174, 199);
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
			button2.Location = new Point(101, 199);
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
			button1.Location = new Point(19, 199);
			button1.Margin = new Padding(3, 2, 3, 2);
			button1.Name = "button1";
			button1.Size = new Size(49, 38);
			button1.TabIndex = 24;
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// txtFloorName
			// 
			txtFloorName.CustomizableEdges = customizableEdges1;
			txtFloorName.DefaultText = "";
			txtFloorName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			txtFloorName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			txtFloorName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			txtFloorName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			txtFloorName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			txtFloorName.Font = new Font("Segoe UI", 9F);
			txtFloorName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			txtFloorName.Location = new Point(19, 106);
			txtFloorName.Name = "txtFloorName";
			txtFloorName.PasswordChar = '\0';
			txtFloorName.PlaceholderText = "";
			txtFloorName.SelectedText = "";
			txtFloorName.ShadowDecoration.CustomizableEdges = customizableEdges2;
			txtFloorName.Size = new Size(204, 27);
			txtFloorName.TabIndex = 12;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.HotTrack;
			label1.Location = new Point(19, 50);
			label1.Name = "label1";
			label1.Size = new Size(96, 21);
			label1.TabIndex = 11;
			label1.Text = "Name Floor";
			// 
			// frmFloor
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(700, 338);
			Controls.Add(panel1);
			Controls.Add(DTgrvFloor);
			Margin = new Padding(3, 2, 3, 2);
			Name = "frmFloor";
			Text = "AddFloor";
			((System.ComponentModel.ISupportInitialize)DTgrvFloor).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView DTgrvFloor;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtFloorName;
        private Label label1;
        private Button button3;
        private Button button2;
        private Button button1;
    }
}
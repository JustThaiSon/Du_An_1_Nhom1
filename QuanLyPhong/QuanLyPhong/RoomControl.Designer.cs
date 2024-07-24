namespace QuanLyPhong
{
	partial class RoomControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ptRoom = new PictureBox();
            lbName = new Label();
            lbGia = new Label();
            ((System.ComponentModel.ISupportInitialize)ptRoom).BeginInit();
            SuspendLayout();
            // 
            // ptRoom
            // 
            ptRoom.BackColor = Color.White;
            ptRoom.Location = new Point(0, 16);
            ptRoom.Margin = new Padding(3, 4, 3, 4);
            ptRoom.Name = "ptRoom";
            ptRoom.Size = new Size(186, 137);
            ptRoom.SizeMode = PictureBoxSizeMode.Zoom;
            ptRoom.TabIndex = 0;
            ptRoom.TabStop = false;
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Location = new Point(54, 175);
            lbName.Name = "lbName";
            lbName.Size = new Size(77, 20);
            lbName.TabIndex = 1;
            lbName.Text = "Room 101";
            lbName.Click += lbName_Click;
            // 
            // lbGia
            // 
            lbGia.AutoSize = true;
            lbGia.Location = new Point(21, 212);
            lbGia.Name = "lbGia";
            lbGia.Size = new Size(135, 20);
            lbGia.TabIndex = 2;
            lbGia.Text = "Price : 250000 VNĐ";
            // 
            // RoomControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lbGia);
            Controls.Add(lbName);
            Controls.Add(ptRoom);
            Margin = new Padding(3, 4, 3, 4);
            Name = "RoomControl";
            Size = new Size(186, 267);
            ((System.ComponentModel.ISupportInitialize)ptRoom).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox ptRoom;
		private Label lbName;
		private Label lbGia;
	}
}

﻿namespace QuanLyPhong
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
            ptRoom.Location = new Point(0, 12);
            ptRoom.Name = "ptRoom";
            ptRoom.Size = new Size(163, 103);
            ptRoom.SizeMode = PictureBoxSizeMode.Zoom;
            ptRoom.TabIndex = 0;
            ptRoom.TabStop = false;
            ptRoom.Click += ptRoom_Click;
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Location = new Point(31, 128);
            lbName.Name = "lbName";
            lbName.Size = new Size(60, 15);
            lbName.TabIndex = 1;
            lbName.Text = "Room 101";
            lbName.Click += lbName_Click;
            // 
            // lbGia
            // 
            lbGia.AutoSize = true;
            lbGia.Location = new Point(11, 159);
            lbGia.Name = "lbGia";
            lbGia.Size = new Size(105, 15);
            lbGia.TabIndex = 2;
            lbGia.Text = "Price : 250000 VNĐ";
            // 
            // RoomControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lbGia);
            Controls.Add(lbName);
            Controls.Add(ptRoom);
            Name = "RoomControl";
            Size = new Size(163, 200);
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

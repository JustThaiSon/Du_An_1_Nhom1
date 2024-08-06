namespace QuanLyPhong
{
	partial class frmBookingRoom
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
			tableLayoutPanelRoom = new TableLayoutPanel();
			panel1 = new Panel();
			label1 = new Label();
			label2 = new Label();
			panel2 = new Panel();
			label3 = new Label();
			panel3 = new Panel();
			SuspendLayout();
			// 
			// tableLayoutPanelRoom
			// 
			tableLayoutPanelRoom.ColumnCount = 2;
			tableLayoutPanelRoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.Location = new Point(-2, 94);
			tableLayoutPanelRoom.Name = "tableLayoutPanelRoom";
			tableLayoutPanelRoom.RowCount = 2;
			tableLayoutPanelRoom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.Size = new Size(1326, 650);
			tableLayoutPanelRoom.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.BackColor = Color.Red;
			panel1.Location = new Point(402, 18);
			panel1.Margin = new Padding(3, 2, 3, 2);
			panel1.Name = "panel1";
			panel1.Size = new Size(34, 17);
			panel1.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.Location = new Point(441, 18);
			label1.Name = "label1";
			label1.Size = new Size(90, 19);
			label1.TabIndex = 2;
			label1.Text = "Have guests";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.Location = new Point(620, 18);
			label2.Name = "label2";
			label2.Size = new Size(81, 19);
			label2.TabIndex = 4;
			label2.Text = "Still empty";
			// 
			// panel2
			// 
			panel2.BackColor = Color.Cyan;
			panel2.Location = new Point(581, 18);
			panel2.Margin = new Padding(3, 2, 3, 2);
			panel2.Name = "panel2";
			panel2.Size = new Size(34, 17);
			panel2.TabIndex = 3;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.Location = new Point(799, 18);
			label3.Name = "label3";
			label3.Size = new Size(67, 19);
			label3.TabIndex = 6;
			label3.Text = "Clean up";
			// 
			// panel3
			// 
			panel3.BackColor = Color.Yellow;
			panel3.Location = new Point(760, 18);
			panel3.Margin = new Padding(3, 2, 3, 2);
			panel3.Name = "panel3";
			panel3.Size = new Size(34, 17);
			panel3.TabIndex = 5;
			// 
			// frmBookingRoom
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1323, 709);
			Controls.Add(label3);
			Controls.Add(panel3);
			Controls.Add(label2);
			Controls.Add(panel2);
			Controls.Add(label1);
			Controls.Add(panel1);
			Controls.Add(tableLayoutPanelRoom);
			Name = "frmBookingRoom";
			Text = "frmBookingRoom";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TableLayoutPanel tableLayoutPanelRoom;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Panel panel2;
        private Label label3;
        private Panel panel3;
    }
}
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
			SuspendLayout();
			// 
			// tableLayoutPanelRoom
			// 
			tableLayoutPanelRoom.ColumnCount = 2;
			tableLayoutPanelRoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.Location = new Point(60, 40);
			tableLayoutPanelRoom.Name = "tableLayoutPanelRoom";
			tableLayoutPanelRoom.RowCount = 2;
			tableLayoutPanelRoom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanelRoom.Size = new Size(733, 226);
			tableLayoutPanelRoom.TabIndex = 0;
			// 
			// frmBookingRoom
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(820, 487);
			Controls.Add(tableLayoutPanelRoom);
			Name = "frmBookingRoom";
			Text = "frmBookingRoom";
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanelRoom;
	}
}
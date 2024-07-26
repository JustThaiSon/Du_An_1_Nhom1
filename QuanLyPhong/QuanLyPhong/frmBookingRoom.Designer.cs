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
            tableLayoutPanelRoom.Location = new Point(-2, 126);
            tableLayoutPanelRoom.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelRoom.Name = "tableLayoutPanelRoom";
            tableLayoutPanelRoom.RowCount = 2;
            tableLayoutPanelRoom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelRoom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelRoom.Size = new Size(1516, 866);
            tableLayoutPanelRoom.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Location = new Point(459, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(39, 23);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(504, 24);
            label1.Name = "label1";
            label1.Size = new Size(106, 23);
            label1.TabIndex = 2;
            label1.Text = "Have guests";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(709, 24);
            label2.Name = "label2";
            label2.Size = new Size(99, 23);
            label2.TabIndex = 4;
            label2.Text = "Still empty";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Cyan;
            panel2.Location = new Point(664, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(39, 23);
            panel2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(913, 24);
            label3.Name = "label3";
            label3.Size = new Size(62, 23);
            label3.TabIndex = 6;
            label3.Text = "Repair";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Yellow;
            panel3.Location = new Point(868, 24);
            panel3.Name = "panel3";
            panel3.Size = new Size(39, 23);
            panel3.TabIndex = 5;
            // 
            // frmBookingRoom
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1512, 945);
            Controls.Add(label3);
            Controls.Add(panel3);
            Controls.Add(label2);
            Controls.Add(panel2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanelRoom);
            Margin = new Padding(3, 4, 3, 4);
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
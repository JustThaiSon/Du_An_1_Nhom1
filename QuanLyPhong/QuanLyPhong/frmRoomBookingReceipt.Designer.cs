namespace QuanLyPhong
{
	partial class frmRoomBookingReceipt
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
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
			groupBox1 = new GroupBox();
			groupBox2 = new GroupBox();
			guna2DataGridView2 = new Guna.UI2.WinForms.Guna2DataGridView();
			groupBox3 = new GroupBox();
			groupBox4 = new GroupBox();
			((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)guna2DataGridView2).BeginInit();
			SuspendLayout();
			// 
			// guna2DataGridView1
			// 
			dataGridViewCellStyle1.BackColor = Color.White;
			guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
			dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			guna2DataGridView1.ColumnHeadersHeight = 4;
			guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = Color.White;
			dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
			dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
			dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
			guna2DataGridView1.GridColor = Color.FromArgb(231, 229, 255);
			guna2DataGridView1.Location = new Point(6, 26);
			guna2DataGridView1.Name = "guna2DataGridView1";
			guna2DataGridView1.RowHeadersVisible = false;
			guna2DataGridView1.RowHeadersWidth = 51;
			guna2DataGridView1.Size = new Size(637, 149);
			guna2DataGridView1.TabIndex = 0;
			guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
			guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
			guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
			guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
			guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
			guna2DataGridView1.ThemeStyle.BackColor = Color.White;
			guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
			guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
			guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
			guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
			guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White;
			guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 4;
			guna2DataGridView1.ThemeStyle.ReadOnly = false;
			guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.White;
			guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
			guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
			guna2DataGridView1.ThemeStyle.RowsStyle.Height = 29;
			guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
			guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(guna2DataGridView1);
			groupBox1.Location = new Point(24, 525);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(652, 184);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			groupBox1.Text = "List Service";
			groupBox1.Enter += groupBox1_Enter;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(guna2DataGridView2);
			groupBox2.Location = new Point(702, 525);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(740, 184);
			groupBox2.TabIndex = 2;
			groupBox2.TabStop = false;
			groupBox2.Text = "Orders";
			// 
			// guna2DataGridView2
			// 
			dataGridViewCellStyle4.BackColor = Color.White;
			guna2DataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
			dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle5.ForeColor = Color.White;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
			guna2DataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			guna2DataGridView2.ColumnHeadersHeight = 4;
			guna2DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = Color.White;
			dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
			dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
			dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
			guna2DataGridView2.DefaultCellStyle = dataGridViewCellStyle6;
			guna2DataGridView2.GridColor = Color.FromArgb(231, 229, 255);
			guna2DataGridView2.Location = new Point(6, 26);
			guna2DataGridView2.Name = "guna2DataGridView2";
			guna2DataGridView2.RowHeadersVisible = false;
			guna2DataGridView2.RowHeadersWidth = 51;
			guna2DataGridView2.Size = new Size(728, 149);
			guna2DataGridView2.TabIndex = 0;
			guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
			guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.Font = null;
			guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
			guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
			guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
			guna2DataGridView2.ThemeStyle.BackColor = Color.White;
			guna2DataGridView2.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
			guna2DataGridView2.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
			guna2DataGridView2.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
			guna2DataGridView2.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
			guna2DataGridView2.ThemeStyle.HeaderStyle.ForeColor = Color.White;
			guna2DataGridView2.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			guna2DataGridView2.ThemeStyle.HeaderStyle.Height = 4;
			guna2DataGridView2.ThemeStyle.ReadOnly = false;
			guna2DataGridView2.ThemeStyle.RowsStyle.BackColor = Color.White;
			guna2DataGridView2.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			guna2DataGridView2.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
			guna2DataGridView2.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
			guna2DataGridView2.ThemeStyle.RowsStyle.Height = 29;
			guna2DataGridView2.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
			guna2DataGridView2.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
			// 
			// groupBox3
			// 
			groupBox3.Location = new Point(39, 75);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(440, 407);
			groupBox3.TabIndex = 3;
			groupBox3.TabStop = false;
			groupBox3.Text = "Invoice information";
			// 
			// groupBox4
			// 
			groupBox4.Location = new Point(539, 75);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new Size(418, 407);
			groupBox4.TabIndex = 4;
			groupBox4.TabStop = false;
			groupBox4.Text = "Invoice information";
			// 
			// frmRoomBookingReceipt
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Silver;
			ClientSize = new Size(1454, 758);
			Controls.Add(groupBox4);
			Controls.Add(groupBox3);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			Name = "frmRoomBookingReceipt";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "frmRoomBookingReceipt";
			Load += frmRoomBookingReceipt_Load;
			((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)guna2DataGridView2).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView2;
		private GroupBox groupBox3;
		private GroupBox groupBox4;
	}
}
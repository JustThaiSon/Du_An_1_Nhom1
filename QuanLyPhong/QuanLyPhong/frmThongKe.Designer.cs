namespace QuanLyPhong
{
    partial class frmThongKe
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            dtGV_ThongKe = new DataGridView();
            label1 = new Label();
            groupBox1 = new GroupBox();
            btn_KhachThue = new Button();
            btn_DoanhThu = new Button();
            btn_LuotThue = new Button();
            cbb_Phong = new ComboBox();
            cbb_Tang = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            dtP_TuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            dtP_DenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            btn_XuatFileExcel = new Button();
            btn_XemThongKe = new Button();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dtGV_ThongKe).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dtGV_ThongKe
            // 
            dtGV_ThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtGV_ThongKe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtGV_ThongKe.Location = new Point(12, 414);
            dtGV_ThongKe.Name = "dtGV_ThongKe";
            dtGV_ThongKe.RowHeadersWidth = 51;
            dtGV_ThongKe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtGV_ThongKe.ShowCellErrors = false;
            dtGV_ThongKe.Size = new Size(1488, 519);
            dtGV_ThongKe.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(47, 9);
            label1.Name = "label1";
            label1.Size = new Size(528, 46);
            label1.TabIndex = 5;
            label1.Text = "Thống Kê Doanh Thu Khách Sạn";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_KhachThue);
            groupBox1.Controls.Add(btn_DoanhThu);
            groupBox1.Controls.Add(btn_LuotThue);
            groupBox1.Controls.Add(cbb_Phong);
            groupBox1.Controls.Add(cbb_Tang);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(dtP_TuNgay);
            groupBox1.Controls.Add(dtP_DenNgay);
            groupBox1.Controls.Add(btn_XuatFileExcel);
            groupBox1.Controls.Add(btn_XemThongKe);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(12, 75);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1488, 333);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Lựa Chọn Thống Kê";
            // 
            // btn_KhachThue
            // 
            btn_KhachThue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_KhachThue.Location = new Point(705, 212);
            btn_KhachThue.Name = "btn_KhachThue";
            btn_KhachThue.Size = new Size(153, 67);
            btn_KhachThue.TabIndex = 114;
            btn_KhachThue.Text = "Khách Có Lượt Thuê Nhiều Nhất";
            btn_KhachThue.UseVisualStyleBackColor = true;
            btn_KhachThue.Click += btn_KhachThue_Click;
            // 
            // btn_DoanhThu
            // 
            btn_DoanhThu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_DoanhThu.Location = new Point(966, 212);
            btn_DoanhThu.Name = "btn_DoanhThu";
            btn_DoanhThu.Size = new Size(153, 67);
            btn_DoanhThu.TabIndex = 113;
            btn_DoanhThu.Text = "Phòng Có Doanh Thu Cao Nhất";
            btn_DoanhThu.UseVisualStyleBackColor = true;
            btn_DoanhThu.Click += btn_DoanhThu_Click;
            // 
            // btn_LuotThue
            // 
            btn_LuotThue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_LuotThue.Location = new Point(966, 70);
            btn_LuotThue.Name = "btn_LuotThue";
            btn_LuotThue.Size = new Size(153, 67);
            btn_LuotThue.TabIndex = 112;
            btn_LuotThue.Text = "Phòng Có Lượt Thuê Nhiều Nhất";
            btn_LuotThue.UseVisualStyleBackColor = true;
            btn_LuotThue.Click += btn_LuotThue_Click;
            // 
            // cbb_Phong
            // 
            cbb_Phong.FormattingEnabled = true;
            cbb_Phong.Location = new Point(387, 109);
            cbb_Phong.Name = "cbb_Phong";
            cbb_Phong.Size = new Size(204, 28);
            cbb_Phong.TabIndex = 111;
            cbb_Phong.SelectedIndexChanged += cbb_Phong_SelectedIndexChanged;
            // 
            // cbb_Tang
            // 
            cbb_Tang.FormattingEnabled = true;
            cbb_Tang.Location = new Point(35, 109);
            cbb_Tang.Name = "cbb_Tang";
            cbb_Tang.Size = new Size(204, 28);
            cbb_Tang.TabIndex = 110;
            cbb_Tang.SelectedIndexChanged += cbb_Tang_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(387, 68);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 107;
            label5.Text = "Phòng";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(35, 68);
            label4.Name = "label4";
            label4.Size = new Size(44, 20);
            label4.TabIndex = 106;
            label4.Text = "Tầng";
            // 
            // dtP_TuNgay
            // 
            dtP_TuNgay.BorderRadius = 5;
            dtP_TuNgay.Checked = true;
            dtP_TuNgay.CustomizableEdges = customizableEdges1;
            dtP_TuNgay.Font = new Font("Segoe UI", 9F);
            dtP_TuNgay.Format = DateTimePickerFormat.Short;
            dtP_TuNgay.Location = new Point(35, 250);
            dtP_TuNgay.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtP_TuNgay.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtP_TuNgay.Name = "dtP_TuNgay";
            dtP_TuNgay.ShadowDecoration.CustomizableEdges = customizableEdges2;
            dtP_TuNgay.Size = new Size(204, 29);
            dtP_TuNgay.TabIndex = 104;
            dtP_TuNgay.Value = new DateTime(2024, 7, 17, 0, 0, 0, 0);
            // 
            // dtP_DenNgay
            // 
            dtP_DenNgay.BorderRadius = 5;
            dtP_DenNgay.Checked = true;
            dtP_DenNgay.CustomizableEdges = customizableEdges3;
            dtP_DenNgay.Font = new Font("Segoe UI", 9F);
            dtP_DenNgay.Format = DateTimePickerFormat.Short;
            dtP_DenNgay.Location = new Point(387, 250);
            dtP_DenNgay.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtP_DenNgay.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtP_DenNgay.Name = "dtP_DenNgay";
            dtP_DenNgay.ShadowDecoration.CustomizableEdges = customizableEdges4;
            dtP_DenNgay.Size = new Size(204, 29);
            dtP_DenNgay.TabIndex = 103;
            dtP_DenNgay.Value = new DateTime(2024, 7, 17, 12, 1, 0, 0);
            // 
            // btn_XuatFileExcel
            // 
            btn_XuatFileExcel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_XuatFileExcel.Location = new Point(1213, 109);
            btn_XuatFileExcel.Name = "btn_XuatFileExcel";
            btn_XuatFileExcel.Size = new Size(182, 129);
            btn_XuatFileExcel.TabIndex = 5;
            btn_XuatFileExcel.Text = "Xuất File Excel";
            btn_XuatFileExcel.UseVisualStyleBackColor = true;
            btn_XuatFileExcel.Click += btn_XuatFileExcel_Click;
            // 
            // btn_XemThongKe
            // 
            btn_XemThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_XemThongKe.Location = new Point(705, 70);
            btn_XemThongKe.Name = "btn_XemThongKe";
            btn_XemThongKe.Size = new Size(153, 67);
            btn_XemThongKe.TabIndex = 4;
            btn_XemThongKe.Text = "Xem Thống Kê";
            btn_XemThongKe.UseVisualStyleBackColor = true;
            btn_XemThongKe.Click += btn_XemThongKe_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(35, 202);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 2;
            label2.Text = "Từ Ngày";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(387, 202);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 3;
            label3.Text = "Đến Ngày";
            // 
            // frmThongKe
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1512, 945);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(dtGV_ThongKe);
            Name = "frmThongKe";
            Text = "frmThongKe";
            Load += frmThongKe_Load;
            ((System.ComponentModel.ISupportInitialize)dtGV_ThongKe).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dtGV_ThongKe;
        private Label label1;
        private GroupBox groupBox1;
        private Button btn_KhachThue;
        private Button btn_DoanhThu;
        private Button btn_LuotThue;
        private ComboBox cbb_Phong;
        private ComboBox cbb_Tang;
        private Label label5;
        private Label label4;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtP_TuNgay;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtP_DenNgay;
        private Button btn_XuatFileExcel;
        private Button btn_XemThongKe;
        private Label label2;
        private Label label3;
    }
}
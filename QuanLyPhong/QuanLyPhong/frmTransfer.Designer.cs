namespace QuanLyPhong
{
    partial class frmTransfer
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
            tb_roomnow = new Guna.UI2.WinForms.Guna2TextBox();
            lb_roomnow = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btn_change = new Button();
            btn_cancel = new Button();
            cbb_roomchange = new Guna.UI2.WinForms.Guna2ComboBox();
            SuspendLayout();
            // 
            // tb_roomnow
            // 
            tb_roomnow.BorderRadius = 5;
            tb_roomnow.CustomizableEdges = customizableEdges1;
            tb_roomnow.DefaultText = "";
            tb_roomnow.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_roomnow.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_roomnow.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_roomnow.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_roomnow.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_roomnow.Font = new Font("Segoe UI", 9F);
            tb_roomnow.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_roomnow.Location = new Point(162, 38);
            tb_roomnow.Margin = new Padding(3, 4, 3, 4);
            tb_roomnow.Name = "tb_roomnow";
            tb_roomnow.PasswordChar = '\0';
            tb_roomnow.PlaceholderText = "";
            tb_roomnow.SelectedText = "";
            tb_roomnow.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tb_roomnow.Size = new Size(380, 42);
            tb_roomnow.TabIndex = 12;
            // 
            // lb_roomnow
            // 
            lb_roomnow.BackColor = Color.Transparent;
            lb_roomnow.Location = new Point(51, 58);
            lb_roomnow.Name = "lb_roomnow";
            lb_roomnow.Size = new Size(81, 22);
            lb_roomnow.TabIndex = 11;
            lb_roomnow.Text = "Room Now:";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(51, 118);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(99, 22);
            guna2HtmlLabel1.TabIndex = 13;
            guna2HtmlLabel1.Text = "Rome Change:";
            // 
            // btn_change
            // 
            btn_change.BackColor = Color.DeepSkyBlue;
            btn_change.Location = new Point(392, 174);
            btn_change.Name = "btn_change";
            btn_change.Size = new Size(142, 44);
            btn_change.TabIndex = 35;
            btn_change.Text = "Change ";
            btn_change.UseVisualStyleBackColor = false;
            btn_change.Click += btn_change_Click;
            // 
            // btn_cancel
            // 
            btn_cancel.BackColor = Color.Red;
            btn_cancel.Location = new Point(191, 174);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.Size = new Size(142, 44);
            btn_cancel.TabIndex = 34;
            btn_cancel.Text = "Cancel";
            btn_cancel.UseVisualStyleBackColor = false;
            btn_cancel.Click += btn_cancel_Click;
            // 
            // cbb_roomchange
            // 
            cbb_roomchange.BackColor = Color.Transparent;
            cbb_roomchange.BorderRadius = 5;
            cbb_roomchange.CustomizableEdges = customizableEdges3;
            cbb_roomchange.DrawMode = DrawMode.OwnerDrawFixed;
            cbb_roomchange.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_roomchange.FocusedColor = Color.FromArgb(94, 148, 255);
            cbb_roomchange.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbb_roomchange.Font = new Font("Segoe UI", 10F);
            cbb_roomchange.ForeColor = Color.FromArgb(68, 88, 112);
            cbb_roomchange.ItemHeight = 30;
            cbb_roomchange.Location = new Point(162, 115);
            cbb_roomchange.Name = "cbb_roomchange";
            cbb_roomchange.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbb_roomchange.Size = new Size(380, 36);
            cbb_roomchange.TabIndex = 36;
            // 
            // frmTransfer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(589, 242);
            Controls.Add(cbb_roomchange);
            Controls.Add(btn_change);
            Controls.Add(btn_cancel);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(tb_roomnow);
            Controls.Add(lb_roomnow);
            Name = "frmTransfer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Change Room";
            Load += frmTransfer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox tb_roomnow;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_roomnow;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Button btn_change;
        private Button btn_cancel;
        private Guna.UI2.WinForms.Guna2ComboBox cbb_roomchange;
    }
}
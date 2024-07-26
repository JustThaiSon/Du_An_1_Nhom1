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
            tb_orderCode = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btn_change = new Button();
            btn_cancel = new Button();
            SuspendLayout();
            // 
            // tb_orderCode
            // 
            tb_orderCode.CustomizableEdges = customizableEdges1;
            tb_orderCode.DefaultText = "";
            tb_orderCode.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_orderCode.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_orderCode.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_orderCode.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_orderCode.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_orderCode.Font = new Font("Segoe UI", 9F);
            tb_orderCode.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_orderCode.Location = new Point(154, 38);
            tb_orderCode.Margin = new Padding(3, 4, 3, 4);
            tb_orderCode.Name = "tb_orderCode";
            tb_orderCode.PasswordChar = '\0';
            tb_orderCode.PlaceholderText = "";
            tb_orderCode.SelectedText = "";
            tb_orderCode.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tb_orderCode.Size = new Size(380, 52);
            tb_orderCode.TabIndex = 12;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(51, 58);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(81, 22);
            guna2HtmlLabel2.TabIndex = 11;
            guna2HtmlLabel2.Text = "Room Now:";
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.CustomizableEdges = customizableEdges3;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Font = new Font("Segoe UI", 9F);
            guna2TextBox1.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Location = new Point(154, 107);
            guna2TextBox1.Margin = new Padding(3, 4, 3, 4);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PasswordChar = '\0';
            guna2TextBox1.PlaceholderText = "";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2TextBox1.Size = new Size(380, 45);
            guna2TextBox1.TabIndex = 14;
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
            // 
            // frmTransfer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(589, 242);
            Controls.Add(btn_change);
            Controls.Add(btn_cancel);
            Controls.Add(guna2TextBox1);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(tb_orderCode);
            Controls.Add(guna2HtmlLabel2);
            Name = "frmTransfer";
            Text = "Change Room";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox tb_orderCode;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Button btn_change;
        private Button btn_cancel;
    }
}
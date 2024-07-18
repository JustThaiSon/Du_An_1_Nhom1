namespace QuanLyPhong
{
    partial class AddFloor
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFloor));
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            tb_nameroom = new Guna.UI2.WinForms.Guna2TextBox();
            label3 = new Label();
            guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(37, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(392, 377);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(guna2TextBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(tb_nameroom);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(471, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(281, 377);
            panel1.TabIndex = 1;
            // 
            // tb_nameroom
            // 
            tb_nameroom.CustomizableEdges = customizableEdges3;
            tb_nameroom.DefaultText = "";
            tb_nameroom.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_nameroom.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_nameroom.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_nameroom.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_nameroom.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_nameroom.Font = new Font("Segoe UI", 9F);
            tb_nameroom.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_nameroom.Location = new Point(22, 79);
            tb_nameroom.Margin = new Padding(3, 4, 3, 4);
            tb_nameroom.Name = "tb_nameroom";
            tb_nameroom.PasswordChar = '\0';
            tb_nameroom.PlaceholderText = "";
            tb_nameroom.SelectedText = "";
            tb_nameroom.ShadowDecoration.CustomizableEdges = customizableEdges4;
            tb_nameroom.Size = new Size(233, 36);
            tb_nameroom.TabIndex = 10;
            tb_nameroom.TextChanged += tb_nameroom_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.HotTrack;
            label3.Location = new Point(22, 34);
            label3.Name = "label3";
            label3.Size = new Size(87, 28);
            label3.TabIndex = 9;
            label3.Text = "ID Floor";
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.CustomizableEdges = customizableEdges1;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Font = new Font("Segoe UI", 9F);
            guna2TextBox1.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Location = new Point(22, 173);
            guna2TextBox1.Margin = new Padding(3, 4, 3, 4);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PasswordChar = '\0';
            guna2TextBox1.PlaceholderText = "";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2TextBox1.Size = new Size(233, 36);
            guna2TextBox1.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(22, 128);
            label1.Name = "label1";
            label1.Size = new Size(122, 28);
            label1.TabIndex = 11;
            label1.Text = "Name Floor";
            // 
            // button1
            // 
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(22, 265);
            button1.Name = "button1";
            button1.Size = new Size(56, 50);
            button1.TabIndex = 24;
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(115, 265);
            button2.Name = "button2";
            button2.Size = new Size(56, 50);
            button2.TabIndex = 25;
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(199, 265);
            button3.Name = "button3";
            button3.Size = new Size(56, 50);
            button3.TabIndex = 26;
            button3.UseVisualStyleBackColor = true;
            // 
            // AddFloor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "AddFloor";
            Text = "AddFloor";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox tb_nameroom;
        private Label label3;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Label label1;
        private Button button3;
        private Button button2;
        private Button button1;
    }
}
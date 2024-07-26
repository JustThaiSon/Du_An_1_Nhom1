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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFloor));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DTgrvFloor = new DataGridView();
            panel1 = new Panel();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            txtFloorName = new Guna.UI2.WinForms.Guna2TextBox();
            label1 = new Label();
            txtFloorId = new Guna.UI2.WinForms.Guna2TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)DTgrvFloor).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // DTgrvFloor
            // 
            DTgrvFloor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DTgrvFloor.Location = new Point(37, 41);
            DTgrvFloor.Name = "DTgrvFloor";
            DTgrvFloor.RowHeadersWidth = 51;
            DTgrvFloor.Size = new Size(392, 377);
            DTgrvFloor.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(txtFloorName);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtFloorId);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(471, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(281, 377);
            panel1.TabIndex = 1;
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
            // button2
            // 
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(115, 265);
            button2.Name = "button2";
            button2.Size = new Size(56, 50);
            button2.TabIndex = 25;
            button2.UseVisualStyleBackColor = true;
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
            // txtFloorName
            // 
            txtFloorName.CustomizableEdges = customizableEdges5;
            txtFloorName.DefaultText = "";
            txtFloorName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtFloorName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtFloorName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtFloorName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtFloorName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFloorName.Font = new Font("Segoe UI", 9F);
            txtFloorName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFloorName.Location = new Point(22, 173);
            txtFloorName.Margin = new Padding(3, 4, 3, 4);
            txtFloorName.Name = "txtFloorName";
            txtFloorName.PasswordChar = '\0';
            txtFloorName.PlaceholderText = "";
            txtFloorName.SelectedText = "";
            txtFloorName.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtFloorName.Size = new Size(233, 36);
            txtFloorName.TabIndex = 12;
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
            // txtFloorId
            // 
            txtFloorId.CustomizableEdges = customizableEdges7;
            txtFloorId.DefaultText = "";
            txtFloorId.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtFloorId.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtFloorId.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtFloorId.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtFloorId.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFloorId.Font = new Font("Segoe UI", 9F);
            txtFloorId.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFloorId.Location = new Point(22, 79);
            txtFloorId.Margin = new Padding(3, 4, 3, 4);
            txtFloorId.Name = "txtFloorId";
            txtFloorId.PasswordChar = '\0';
            txtFloorId.PlaceholderText = "";
            txtFloorId.SelectedText = "";
            txtFloorId.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtFloorId.Size = new Size(233, 36);
            txtFloorId.TabIndex = 10;
            txtFloorId.TextChanged += tb_nameroom_TextChanged;
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
            // AddFloor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(DTgrvFloor);
            Name = "AddFloor";
            Text = "AddFloor";
            ((System.ComponentModel.ISupportInitialize)DTgrvFloor).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DTgrvFloor;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtFloorId;
        private Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtFloorName;
        private Label label1;
        private Button button3;
        private Button button2;
        private Button button1;
    }
}
namespace BCPA_OTS_Prototype
{
    partial class LoginForm
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
            this.tb_UsernameInput = new System.Windows.Forms.TextBox();
            this.tb_PasswordInput = new System.Windows.Forms.TextBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.linkLabel_RegisterNewAccount = new System.Windows.Forms.LinkLabel();
            this.Label_BCPA_OTS = new System.Windows.Forms.Label();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_UsernameInput
            // 
            this.tb_UsernameInput.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_UsernameInput.Location = new System.Drawing.Point(12, 188);
            this.tb_UsernameInput.MaxLength = 64;
            this.tb_UsernameInput.Name = "tb_UsernameInput";
            this.tb_UsernameInput.Size = new System.Drawing.Size(478, 31);
            this.tb_UsernameInput.TabIndex = 0;
            this.tb_UsernameInput.Text = "FirstNameMiddleNameLastName@MailServerName.co.uk";
            // 
            // tb_PasswordInput
            // 
            this.tb_PasswordInput.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_PasswordInput.Location = new System.Drawing.Point(12, 225);
            this.tb_PasswordInput.Name = "tb_PasswordInput";
            this.tb_PasswordInput.Size = new System.Drawing.Size(478, 31);
            this.tb_PasswordInput.TabIndex = 1;
            this.tb_PasswordInput.Text = "Password";
            this.tb_PasswordInput.Enter += new System.EventHandler(this.tb_PasswordInput_Enter);
            // 
            // MainPanel
            // 
            this.MainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainPanel.Controls.Add(this.linkLabel_RegisterNewAccount);
            this.MainPanel.Controls.Add(this.Label_BCPA_OTS);
            this.MainPanel.Controls.Add(this.LogoPictureBox);
            this.MainPanel.Controls.Add(this.btn_Login);
            this.MainPanel.Controls.Add(this.tb_UsernameInput);
            this.MainPanel.Controls.Add(this.tb_PasswordInput);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(502, 299);
            this.MainPanel.TabIndex = 2;
            // 
            // linkLabel_RegisterNewAccount
            // 
            this.linkLabel_RegisterNewAccount.AutoSize = true;
            this.linkLabel_RegisterNewAccount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_RegisterNewAccount.Location = new System.Drawing.Point(12, 266);
            this.linkLabel_RegisterNewAccount.Name = "linkLabel_RegisterNewAccount";
            this.linkLabel_RegisterNewAccount.Size = new System.Drawing.Size(178, 23);
            this.linkLabel_RegisterNewAccount.TabIndex = 5;
            this.linkLabel_RegisterNewAccount.TabStop = true;
            this.linkLabel_RegisterNewAccount.Text = "Register New Account";
            this.linkLabel_RegisterNewAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_RegisterNewAccount_LinkClicked);
            // 
            // Label_BCPA_OTS
            // 
            this.Label_BCPA_OTS.AutoSize = true;
            this.Label_BCPA_OTS.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_BCPA_OTS.Location = new System.Drawing.Point(12, 159);
            this.Label_BCPA_OTS.Name = "Label_BCPA_OTS";
            this.Label_BCPA_OTS.Size = new System.Drawing.Size(479, 26);
            this.Label_BCPA_OTS.TabIndex = 4;
            this.Label_BCPA_OTS.Text = "Bucks Centre for Performing Arts: Online Ticket System";
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Image = global::BCPA_OTS_Prototype.Properties.Resources.BucksNewUniLogo;
            this.LogoPictureBox.Location = new System.Drawing.Point(12, 12);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(479, 144);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPictureBox.TabIndex = 3;
            this.LogoPictureBox.TabStop = false;
            // 
            // btn_Login
            // 
            this.btn_Login.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Login.Location = new System.Drawing.Point(408, 262);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(82, 31);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btn_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(502, 299);
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BCPA OTS: Login";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label Label_BCPA_OTS;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.LinkLabel linkLabel_RegisterNewAccount;
        public System.Windows.Forms.Button btn_Login;
        public System.Windows.Forms.TextBox tb_UsernameInput;
        public System.Windows.Forms.TextBox tb_PasswordInput;
    }
}
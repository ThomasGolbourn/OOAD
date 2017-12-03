namespace BCPA_OTS_Server
{
    partial class MainWindow
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
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.ConsoleTab = new System.Windows.Forms.TabPage();
            this.tb_Output = new System.Windows.Forms.RichTextBox();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.tb_Username = new System.Windows.Forms.TextBox();
            this.tb_DatabaseName = new System.Windows.Forms.TextBox();
            this.tb_Address = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SaveSettings = new System.Windows.Forms.Button();
            this.btn_ReloadSettings = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.ConsoleTab.SuspendLayout();
            this.SettingsTab.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.ConsoleTab);
            this.MainTabControl.Controls.Add(this.SettingsTab);
            this.MainTabControl.Location = new System.Drawing.Point(0, 12);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(833, 186);
            this.MainTabControl.TabIndex = 1;
            // 
            // ConsoleTab
            // 
            this.ConsoleTab.Controls.Add(this.tb_Output);
            this.ConsoleTab.Location = new System.Drawing.Point(4, 22);
            this.ConsoleTab.Name = "ConsoleTab";
            this.ConsoleTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConsoleTab.Size = new System.Drawing.Size(825, 160);
            this.ConsoleTab.TabIndex = 0;
            this.ConsoleTab.Text = "Console";
            this.ConsoleTab.UseVisualStyleBackColor = true;
            // 
            // tb_Output
            // 
            this.tb_Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tb_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Output.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Output.ForeColor = System.Drawing.SystemColors.Window;
            this.tb_Output.Location = new System.Drawing.Point(3, 3);
            this.tb_Output.Name = "tb_Output";
            this.tb_Output.ReadOnly = true;
            this.tb_Output.Size = new System.Drawing.Size(819, 154);
            this.tb_Output.TabIndex = 1;
            this.tb_Output.Text = "";
            // 
            // SettingsTab
            // 
            this.SettingsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SettingsTab.Controls.Add(this.btn_SaveSettings);
            this.SettingsTab.Controls.Add(this.btn_ReloadSettings);
            this.SettingsTab.Controls.Add(this.panel2);
            this.SettingsTab.Controls.Add(this.panel1);
            this.SettingsTab.Location = new System.Drawing.Point(4, 22);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTab.Size = new System.Drawing.Size(825, 160);
            this.SettingsTab.TabIndex = 1;
            this.SettingsTab.Text = "Settings";
            this.SettingsTab.Enter += new System.EventHandler(this.SettingsTab_Enter);
            this.SettingsTab.Leave += new System.EventHandler(this.SettingsTab_Leave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.tb_Port);
            this.panel2.Controls.Add(this.tb_Password);
            this.panel2.Controls.Add(this.tb_Username);
            this.panel2.Controls.Add(this.tb_DatabaseName);
            this.panel2.Controls.Add(this.tb_Address);
            this.panel2.Location = new System.Drawing.Point(538, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(279, 111);
            this.panel2.TabIndex = 2;
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(189, 3);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(87, 20);
            this.tb_Port.TabIndex = 1;
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(3, 81);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.Size = new System.Drawing.Size(273, 20);
            this.tb_Password.TabIndex = 4;
            // 
            // tb_Username
            // 
            this.tb_Username.Location = new System.Drawing.Point(3, 55);
            this.tb_Username.Name = "tb_Username";
            this.tb_Username.Size = new System.Drawing.Size(273, 20);
            this.tb_Username.TabIndex = 3;
            // 
            // tb_DatabaseName
            // 
            this.tb_DatabaseName.Location = new System.Drawing.Point(3, 29);
            this.tb_DatabaseName.Name = "tb_DatabaseName";
            this.tb_DatabaseName.Size = new System.Drawing.Size(273, 20);
            this.tb_DatabaseName.TabIndex = 2;
            // 
            // tb_Address
            // 
            this.tb_Address.Location = new System.Drawing.Point(3, 3);
            this.tb_Address.Name = "tb_Address";
            this.tb_Address.Size = new System.Drawing.Size(180, 20);
            this.tb_Address.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 111);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "MySQL Database Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(372, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 96);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address : Port:\r\nDatabase Name:\r\nUsername:\r\nPassword:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_SaveSettings
            // 
            this.btn_SaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveSettings.Location = new System.Drawing.Point(729, 124);
            this.btn_SaveSettings.Name = "btn_SaveSettings";
            this.btn_SaveSettings.Size = new System.Drawing.Size(88, 23);
            this.btn_SaveSettings.TabIndex = 5;
            this.btn_SaveSettings.Text = "Save Settings";
            this.btn_SaveSettings.UseVisualStyleBackColor = true;
            this.btn_SaveSettings.Click += new System.EventHandler(this.btn_SaveSettings_Click);
            // 
            // btn_ReloadSettings
            // 
            this.btn_ReloadSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ReloadSettings.Location = new System.Drawing.Point(629, 124);
            this.btn_ReloadSettings.Name = "btn_ReloadSettings";
            this.btn_ReloadSettings.Size = new System.Drawing.Size(94, 23);
            this.btn_ReloadSettings.TabIndex = 6;
            this.btn_ReloadSettings.Text = "Reload Settings";
            this.btn_ReloadSettings.UseVisualStyleBackColor = true;
            this.btn_ReloadSettings.Click += new System.EventHandler(this.btn_ReloadSettings_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 198);
            this.Controls.Add(this.MainTabControl);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "BCPA OTS Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MainTabControl.ResumeLayout(false);
            this.ConsoleTab.ResumeLayout(false);
            this.SettingsTab.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ConsoleTab;
        private System.Windows.Forms.TabPage SettingsTab;
        public System.Windows.Forms.RichTextBox tb_Output;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tb_Port;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.TextBox tb_Username;
        private System.Windows.Forms.TextBox tb_DatabaseName;
        private System.Windows.Forms.TextBox tb_Address;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SaveSettings;
        private System.Windows.Forms.Button btn_ReloadSettings;
    }
}


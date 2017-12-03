using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPA_OTS_Server
{
    public partial class MainWindow : Form
    {
        public Size FormSize;

        public MainWindow()
        {
            InitializeComponent();
            tb_Output.AppendText("BCPA OTS: Server Version 1.0 \n");
            CheckForIllegalCrossThreadCalls = false;
            FormSize = this.Size;
        }

        public void Log(string logText)
        {
            //Add new line
            tb_Output.AppendText(Environment.NewLine);

            //Add date and time
            tb_Output.AppendText(
                DateTime.Now.ToString("dd/M/yy HH:mm:ss") + ":\t"
            );

            //Add input text
            tb_Output.AppendText(logText);

            //Scroll window to bottom
            tb_Output.SelectionStart = tb_Output.Text.Length;
            tb_Output.ScrollToCaret();
        }

        //Save settings
        private void btn_SaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.db_Address = tb_Address.Text;
            Properties.Settings.Default.db_Port = Convert.ToUInt32(tb_Port.Text);
            Properties.Settings.Default.db_Database = tb_DatabaseName.Text;
            Properties.Settings.Default.db_Username = tb_Username.Text;
            Properties.Settings.Default.db_Password = tb_Password.Text;

            Properties.Settings.Default.Save();
        }

        //Reload settings
        private void btn_ReloadSettings_Click(object sender, EventArgs e)
        {
            InsertSettings();
        }

        private void InsertSettings() {
            tb_Address.Text = Properties.Settings.Default.db_Address;
            tb_Port.Text = Properties.Settings.Default.db_Port.ToString();
            tb_DatabaseName.Text = Properties.Settings.Default.db_Database;
            tb_Username.Text = Properties.Settings.Default.db_Username;
            tb_Password.Text = Properties.Settings.Default.db_Password;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SettingsTab_Enter(object sender, EventArgs e)
        {
            FormSize = this.Size;
            this.Size = new Size(849, 237);
            InsertSettings();
        }

        private void SettingsTab_Leave(object sender, EventArgs e)
        {
            this.Size = FormSize;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}

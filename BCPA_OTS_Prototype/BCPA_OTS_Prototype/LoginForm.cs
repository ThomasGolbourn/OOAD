using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace BCPA_OTS_Prototype
{
    public partial class LoginForm : Form
    {
        public NetConnection netCon;
        public Task ClientTask;

        public LoginForm()
        {
            InitializeComponent();

            //Check connection on open
            netCon = new NetConnection(this, "CONN_OPEN:HELLO");
        }

        private void tb_PasswordInput_Enter(object sender, EventArgs e)
        {
            tb_PasswordInput.Text = "";
            tb_PasswordInput.UseSystemPasswordChar = true;
        }

        private void linkLabel_RegisterNewAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm RegForm = new RegistrationForm();
            ActiveForm.SendToBack();
            RegForm.Show();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            // netCon.SendLogin(tb_UsernameInput.Text,tb_PasswordInput.Text);

            netCon = new NetConnection(this, "LOGIN:["+tb_UsernameInput.Text+":"+ tb_PasswordInput.Text+"]");

        }

        public void errorMessage(string message)
        {
            //Show warning box
            MessageBox.Show(
                message,
                "Login Error!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1
            );
        }

    }
}

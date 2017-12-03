using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPA_OTS_Prototype
{
    public partial class RegistrationForm : Form
    {

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btn_RegisterAccount_Click(object sender, EventArgs e)
        {
            string btnText = btn_RegisterAccount.Text;
            btn_RegisterAccount.Enabled = false;
            btn_RegisterAccount.Text = "Please Wait...";

            /*bool AddUserSuccess = netCon.AddNewUser(
               cmb_TitleInput.Text.ToString(),
               tb_FirstNameInput.Text.ToString(),
               tb_MiddleNameInput.Text.ToString(),
               tb_LastNameInput.Text.ToString(),
               tb_EmailAddressInput.Text.ToString(),
               tb_PasswordInput.Text.ToString(),
               tb_CountyInput.Text.ToString(),
               cmb_CountryInput.Text.ToString(),
               tb_HouseNumberInput.Text.ToString(),
               tb_AddressLine1Input.Text.ToString(),
               tb_AddressLine2Input.Text.ToString(),
               tb_AddressLine3Input.Text.ToString(),
               tb_TownCityInput.Text.ToString(),
               tb_PostCodeInput.Text.ToString(),
               dtp_DateOfBirthInput.Value
           );

            //Show warning and exit if error with adding user
            if (!AddUserSuccess) {
                
                //Show warning box
                MessageBox.Show(
                    "Failed to register account, please try again",
                    "Registration Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                );

                //Reset button
                btn_RegisterAccount.Enabled = true;
                btn_RegisterAccount.Text = btnText;

                //Exit
                return;
            };

            //Reset button
            btn_RegisterAccount.Enabled = true;
            btn_RegisterAccount.Text = btnText;

            //Show success message
            MessageBox.Show("Account Registered Successfully. You may now login.");

            //Close form
            this.Close();
            */
        }

        private void linkLabel_TermsAndConditions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer non lacus rutrum, mollis nisi quis, mattis magna. Proin vel vestibulum dolor. Cras tempus nisi non felis placerat aliquam. In ullamcorper, neque id bibendum vehicula, nunc quam auctor mauris, congue luctus risus turpis sed justo. Sed nec diam ut orci pharetra pharetra ut sed felis. Vivamus gravida id magna et vulputate. Phasellus viverra diam vitae purus maximus gravida. Donec eu felis fringilla, varius tortor vitae, convallis erat. In in massa eu est placerat iaculis. Pellentesque vitae lorem posuere, sollicitudin nulla a, facilisis ipsum. Donec placerat tristique massa, vitae sollicitudin erat tincidunt rhoncus. Fusce dapibus ante magna, lacinia feugiat massa sollicitudin sodales. Vestibulum ac sem nisl. Duis condimentum a urna id pharetra. Maecenas a felis at odio placerat tincidunt sit amet sed nibh. Vestibulum eros nisi, auctor eu turpis sit amet, hendrerit pellentesque elit.\n\nDonec sollicitudin convallis nibh eget gravida. Sed commodo augue vel erat hendrerit mollis. Fusce consequat mi elit, quis semper urna condimentum ut. Suspendisse potenti. Vivamus at gravida dui. In sit amet purus vel odio suscipit placerat. Nunc ac dolor eu nibh auctor accumsan. Cras ullamcorper accumsan libero vitae tincidunt. Ut cursus, massa ut interdum bibendum, nisl lorem sodales nunc, luctus venenatis dui nunc in diam. Suspendisse pulvinar viverra ante, in rutrum sapien egestas sed. Proin ac rhoncus sapien. Phasellus accumsan augue ut neque viverra, non auctor turpis tempor. Morbi ullamcorper enim nec ante placerat consectetur. Phasellus egestas leo a lorem scelerisque, et volutpat ante sodales. Integer ultricies, risus at congue facilisis, massa felis mollis nulla, nec lacinia augue ligula quis nisi. Nunc scelerisque ante orci, eget hendrerit lacus hendrerit vel.\n\nInterdum et malesuada fames ac ante ipsum primis in faucibus. Nunc non lorem molestie, accumsan nisl cursus, cursus metus. Fusce mattis, elit sed accumsan egestas, nibh mauris laoreet turpis, ut interdum neque urna nec ex. Cras at enim mi. Pellentesque id eros eget nunc elementum varius vitae vel erat. Vivamus porta ultrices dolor, vel condimentum orci hendrerit tristique. Etiam non sagittis nibh. Vivamus ac ipsum porta, tristique metus quis, ullamcorper est. Aenean interdum, mauris sed tempor sagittis, felis risus placerat odio, ac faucibus tortor turpis in sem. Vivamus nibh ante, aliquam id enim id, hendrerit sollicitudin nisl. Nunc bibendum ipsum a tristique tristique. Integer bibendum risus id dictum malesuada. Cras orci enim, facilisis eget interdum eu, consectetur tincidunt est.\n\nDuis id libero nec lacus venenatis malesuada id eget urna. Maecenas laoreet justo massa, at finibus erat pretium eu. Aliquam lobortis, sem at mattis porttitor, risus felis dapibus dui, sit amet lobortis mi lacus lobortis eros. Aliquam erat volutpat. Sed orci dui, porttitor sit amet malesuada in, condimentum in justo. Nullam suscipit, sapien vel pretium commodo, massa dui ornare lectus, non consectetur sem odio vel mauris. Nullam rhoncus vitae quam ut tempor. Interdum et malesuada fames ac ante ipsum primis in faucibus. Quisque non velit cursus, interdum tortor sit amet, tempus lectus. Cras ac lorem convallis, commodo ligula sit amet, tempus elit. Pellentesque ornare vel orci ac eleifend. Praesent dignissim posuere odio nec malesuada.\n\nCras scelerisque at odio non scelerisque. Phasellus feugiat condimentum dolor vel facilisis. Sed aliquam vel ante quis hendrerit. Donec ullamcorper aliquet erat, a egestas odio pharetra sit amet. Curabitur vestibulum urna leo, vel consectetur leo dictum quis. Donec interdum blandit velit, quis suscipit tellus tempus in. Morbi hendrerit metus ut leo sodales, at semper mauris eleifend. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer non lacus rutrum, mollis nisi quis, mattis magna. Proin vel vestibulum dolor. Cras tempus nisi non felis placerat aliquam. In ullamcorper, neque id bibendum vehicula, nunc quam auctor mauris, congue luctus risus turpis sed justo. Sed nec diam ut orci pharetra pharetra ut sed felis. Vivamus gravida id magna et vulputate. Phasellus viverra diam vitae purus maximus gravida. Donec eu felis fringilla, varius tortor vitae, convallis erat. In in massa eu est placerat iaculis. Pellentesque vitae lorem posuere, sollicitudin nulla a, facilisis ipsum. Donec placerat tristique massa, vitae sollicitudin erat tincidunt rhoncus. Fusce dapibus ante magna, lacinia feugiat massa sollicitudin sodales. Vestibulum ac sem nisl. Duis condimentum a urna id pharetra. Maecenas a felis at odio placerat tincidunt sit amet sed nibh. Vestibulum eros nisi, auctor eu turpis sit amet, hendrerit pellentesque elit.\n\nDonec sollicitudin convallis nibh eget gravida. Sed commodo augue vel erat hendrerit mollis. Fusce consequat mi elit, quis semper urna condimentum ut. Suspendisse potenti. Vivamus at gravida dui. In sit amet purus vel odio suscipit placerat. Nunc ac dolor eu nibh auctor accumsan. Cras ullamcorper accumsan libero vitae tincidunt. Ut cursus, massa ut interdum bibendum, nisl lorem sodales nunc, luctus venenatis dui nunc in diam. Suspendisse pulvinar viverra ante, in rutrum sapien egestas sed. Proin ac rhoncus sapien. Phasellus accumsan augue ut neque viverra, non auctor turpis tempor. Morbi ullamcorper enim nec ante placerat consectetur. Phasellus egestas leo a lorem scelerisque, et volutpat ante sodales. Integer ultricies, risus at congue facilisis, massa felis mollis nulla, nec lacinia augue ligula quis nisi. Nunc scelerisque ante orci, eget hendrerit lacus hendrerit vel.\n\nInterdum et malesuada fames ac ante ipsum primis in faucibus. Nunc non lorem molestie, accumsan nisl cursus, cursus metus. Fusce mattis, elit sed accumsan egestas, nibh mauris laoreet turpis, ut interdum neque urna nec ex. Cras at enim mi. Pellentesque id eros eget nunc elementum varius vitae vel erat. Vivamus porta ultrices dolor, vel condimentum orci hendrerit tristique. Etiam non sagittis nibh. Vivamus ac ipsum porta, tristique metus quis, ullamcorper est. Aenean interdum, mauris sed tempor sagittis, felis risus placerat odio, ac faucibus tortor turpis in sem. Vivamus nibh ante, aliquam id enim id, hendrerit sollicitudin nisl. Nunc bibendum ipsum a tristique tristique. Integer bibendum risus id dictum malesuada. Cras orci enim, facilisis eget interdum eu, consectetur tincidunt est.\n\nDuis id libero nec lacus venenatis malesuada id eget urna. Maecenas laoreet justo massa, at finibus erat pretium eu. Aliquam lobortis, sem at mattis porttitor, risus felis dapibus dui, sit amet lobortis mi lacus lobortis eros. Aliquam erat volutpat. Sed orci dui, porttitor sit amet malesuada in, condimentum in justo. Nullam suscipit, sapien vel pretium commodo, massa dui ornare lectus, non consectetur sem odio vel mauris. Nullam rhoncus vitae quam ut tempor. Interdum et malesuada fames ac ante ipsum primis in faucibus. Quisque non velit cursus, interdum tortor sit amet, tempus lectus. Cras ac lorem convallis, commodo ligula sit amet, tempus elit. Pellentesque ornare vel orci ac eleifend. Praesent dignissim posuere odio nec malesuada.\n\nCras scelerisque at odio non scelerisque. Phasellus feugiat condimentum dolor vel facilisis. Sed aliquam vel ante quis hendrerit. Donec ullamcorper aliquet erat, a egestas odio pharetra sit amet. Curabitur vestibulum urna leo, vel consectetur leo dictum quis. Donec interdum blandit velit, quis suscipit tellus tempus in. Morbi hendrerit metus ut leo sodales, at semper mauris eleifend.",
                "Terms & Conditions"
            );
        }
    }
}

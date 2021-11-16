using Assignment_2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Assignment_2.View
{
    public partial class Register : Form
    {
        Users users;//Load users object
        public Register()
        {
            InitializeComponent();
            datePicker.MaxDate = DateTime.Now;
        }
        private void Register_Load(object sender, EventArgs e)
        {
            try
            {
                users = new Users(); //If there is no Login.txt
            }
            catch (Exception)
            {
                ErrorMessage.Text = "No connection to login detail detected. Please re-open the program";
                //Disable All Input
                txtFname.Enabled = false;
                txtLname.Enabled = false;
                txtPassword.Enabled = false;
                txtUsername.Enabled = false;
                datePicker.Enabled = false;
                AccessType.Enabled = false;
                //Disable all button except Exit
                btnSubmit.Enabled = false;
                btnBack.Enabled = false;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();//Exit
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!noEmptyInput())
            {
                //Error Message regarding no input found
                ErrorMessage.Text = "Some input(s) are empty";
                return;
            }
            else if(!txtConfirmPassword.Text.Trim().Equals(txtPassword.Text.Trim()))
            {
                //Error Message regarding unmatching password
                ErrorMessage.Text = "Password are not same";
                return;
            }
            else
            {
                //Validate Username
                if (users.usernameExist(txtUsername.Text.Trim()))
                    ErrorMessage.Text = "This username is already taken";
                else
                {
                    //Code to Register
                    users.Register(new User(
                        txtUsername.Text.Trim(),
                        txtPassword.Text.Trim(),
                        AccessType.AccessibilityObject.Value.Trim(),
                        txtFname.Text.Trim(),
                        txtLname.Text.Trim(),
                        datePicker.Value.ToShortDateString().Replace("/","-")));

                    string msg = "Successfully create an account : " +
                        "\n\nUsername: " + txtUsername.Text.Trim()  +
                        "\nPassword:" + txtPassword.Text.Trim();
                    DialogResult result = MessageBox.Show(msg, "Register Confirmation",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Redirect to Login
                    Login LoginForm = new Login();
                    this.Visible = false;
                    LoginForm.ShowDialog();
                    this.Close();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Redirect to Login
            Login LoginForm = new Login();
            this.Visible = false;
            LoginForm.ShowDialog();
            this.Close();
        }
        private void RegisterKeyDown(object sender, KeyEventArgs e)
        {
            //Keydown Enter to Submit
            if (e.KeyCode == Keys.Enter)
                btnSubmit.PerformClick();
        }
        //Keydown No Empty Input Function
        public bool noEmptyInput()
        {
            if (AccessType.AccessibilityObject.Value != null &&
                txtLname.TextLength != 0 &&
                txtFname.TextLength != 0 &&
                txtPassword.TextLength != 0 &&
                txtUsername.TextLength != 0)
                return true;
            return false;
        }
    }
}

using Assignment_2.Model;
using Assignment_2.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{

    public partial class Login : Form
    {
        Users users; //Users object
        public Login()
        {
            InitializeComponent();
        }
        //OnLoad Function
        private void Form1_Load(object sender, EventArgs e)
        {
            //If there is no Login.txt
            try
            {
                users = new Users();
            }
            catch (Exception)
            {
                ErrorMessage.Text = "No connection to login detail detected.\nPlease fix the issue then re-open the program";
                //Disable everything except Exit Button
                txtPassword.Enabled = false;
                txtUsername.Enabled = false;
                btnLogin.Enabled = false;
                btnRegister.Enabled = false;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit(); //Exit
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Redirect to Register Form
            Register RegisterForm = new Register();
            this.Visible = false;
            RegisterForm.ShowDialog();
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //If some input are empty
            if (!noEmptyText())
            {
                //Error Message regarding no input found
                ErrorMessage.Text = "Username and Password are required";
                return;
            }
            else
            {
                //If User exist in login txt
                if (users.UserExist(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
                {
                    string msg = "Successfully Login " +
                        "\n\nWelcome to UTSTextEditor";
                    DialogResult result = MessageBox.Show(msg, "Login Confirmation",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TextEditor TextEditorForm = new TextEditor(users.getLoginUser());
                    this.Visible = false;
                    TextEditorForm.ShowDialog();
                    this.Close();
                }
                else 
                ErrorMessage.Text = "Username and Password are incorrect";//wrong username or password
            }
        }
        //KeyDown Enter to Press Login Button
        private void LoginKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }
        //No Empty Text Function
        public bool noEmptyText()
        {
            if (txtPassword.TextLength != 0 && txtUsername.TextLength != 0)
                return true;
            return false;
        }
    }
}

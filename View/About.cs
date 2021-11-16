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
    public partial class About : Form
    {
        //Current Login User
        private User currentUser;
        public About(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;//To Pass data back to TextEditor Form
        }

        //Button OK Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            //Redirect back to TextEditor Form
            TextEditor TextEditorForm = new TextEditor(this.currentUser);
            this.Visible = false;
            TextEditorForm.ShowDialog();
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            //Load details from README.MD
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"..\..\..\README.md");
                foreach (string line in lines)
                {
                    label1.Text = label1.Text + line + "\n";
                }
            }
            catch (Exception)
            {
                //Load details from local code if READMDE is not there
                label1.Text = "This software created by Michael Ryo Kandiawan with Student Id:13407123 \n" +
                    "From 32998 31927.NET Application Development -Spring 2021\n\n"+
                    "Subject Coordinator : Nabin Sharma\n"+
                    "LAB Activity: 04 with Mr Ayman Elgharabawy(Monday 16:00 - 17:30)\n\n"+
                    "This software created to show learning objectives of :\n"+
                    "1.Understand the mechanisms and techniques for the deployment and configuration of.NET applications.\n"+
                    "2.Be able to utilize the.NET libraries.\n"+
                    "3.Have practical experience of how to write C# programs in the .NET environment.";
            }

        }
    }
}

using Assignment_2.Model;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Assignment_2.View
{
    public partial class TextEditor : Form
    {
        private User currentUser;
        private string currentFile;
        public TextEditor(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            toolStripUsername.Text = "Username: " + currentUser.getUsername() +" - " + currentUser.getAccess();
            decimal fontSizeOnLoad = Math.Round((decimal)richTextBox.Font.Size, 0);
            txtFontSize.ComboBox.SelectedIndex = (Convert.ToInt32(fontSizeOnLoad) - 8);
        }
        private void TextEditor_Load(object sender, EventArgs e)
        {
            if(currentUser.getAccess().Trim().Equals("View"))
            {
                this.richTextBox.ReadOnly = true;

                //First ToolStrip
                newToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;

                //Second Tool Strip
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;

                //Second Row ToolStrip
                toolStripNew.Enabled = false;
                toolStripSave.Enabled = false;
                toolStripSaveAs.Enabled = false;

                toolStripBold.Enabled = false;
                toolStripItalic.Enabled = false;
                toolStripUnderline.Enabled = false;

                txtFontSize.Enabled = false;

                //Left ToolStrip
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
                toolStripButton4.Enabled = false;

            }
        }

        //Key down function to have shortcut key
        private new void KeyDown (object sender, KeyEventArgs e)
        {
            if (e.Control && currentUser.getAccess().Trim().Equals("Edit"))
            {
                switch (e.KeyCode)
                {
                    case Keys.N: //Control + N = New
                        {
                            e.Handled = true;
                            toolStripNew.PerformClick();
                            break;
                        }
                    case Keys.S: //Control + S = Save
                        {
                            e.Handled = true;
                            toolStripSave.PerformClick();
                            break;
                        }
                    case Keys.O: //Control + O = Open
                        {
                            e.Handled = true;
                            toolStripOpen.PerformClick();
                            break;
                        }
                    case Keys.X: //Control + X = Cut
                        {
                            e.Handled = true;
                            cutToolStripMenuItem.PerformClick();
                            break;
                        }
                    case Keys.C: //Control + C = Copy
                        {
                            e.Handled = true;
                            copyToolStripMenuItem.PerformClick();
                            break;
                        }
                    case Keys.V: //Control + V = Paste
                        {
                            e.Handled = true;
                            pasteToolStripMenuItem.PerformClick();
                            break;
                        }
                }
            }
        }
        //New file Function
        private void newFile(object sender, EventArgs e)
        {
            //Dialog to make sure
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to clear this text?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                richTextBox.Clear(); //Clear textbox
                this.currentFile = null; //Clear CurrentFileUsed
            }
        }
        //Open file Function
        private void openFile(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog.ShowDialog(); //File Dialog to Open File
                if (result == DialogResult.OK) 
                {
                    currentFile = openFileDialog.FileName; //Save CurrentFile 
                    richTextBox.LoadFile(this.currentFile); //Load RTF File with Format
                }
            }
            catch (Exception) //If file has no format
            {
                try
                {
                    using (var streamReader = new StreamReader(this.currentFile, Encoding.UTF8))//Read by Line (Normal Text)
                        richTextBox.Text = streamReader.ReadToEnd();//Put all text in
                }
                catch (Exception)//If file opened is not valid
                {
                    ErrorMessage.Text = "File are not accepted \nPlease select correct file format"; //add error message
                }
            }
        }
        //Save file Function
        private void saveFile(object sender, EventArgs e)
        {
            if (this.currentFile == null) //If its no file then use Save to be Save As
            {
                saveAsToolStripMenuItem.PerformClick();
            }
            else
            {
                try
                {
                    richTextBox.SaveFile(this.currentFile);//Save File on the current file
                }
                catch (Exception)
                {
                    ErrorMessage.Text = "Something went wrong while trying to save file\n Please try Again";//add error message
                }
            }
        }
        private void saveAsFile(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = saveFileDialog.ShowDialog();//File Dialog to Save File
                if (result == DialogResult.OK)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to save this as a new file?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        richTextBox.SaveFile(saveFileDialog.FileName);//Save File on the current file path from File Dialog
                        this.currentFile = saveFileDialog.FileName;//Save new Current File
                    }
                }
            }
            catch
            {
                ErrorMessage.Text = "Something went wrong while trying to save file\n Please try Again";//add error message
            }
        }
        private void cutText(object sender, EventArgs e)
        {
            try
            {
                richTextBox.Cut();//Cut Text
            }
            catch (Exception)
            {
                ErrorMessage.Text = "Cannot Cut the current Text";//add error message
            }
        }
        private void copyText(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetData(DataFormats.Rtf, richTextBox.SelectedRtf);//Copy Text
            }
            catch (Exception)
            {
                ErrorMessage.Text = "Cannot Copy the current Text";//add error message
            }
        }
        private void pasteText(object sender, EventArgs e)
        {
            //Paste Text
            try
            {
                if (Clipboard.ContainsText(TextDataFormat.Rtf))
                {
                    richTextBox.SelectedRtf = Clipboard.GetData(DataFormats.Rtf).ToString();
                }
            }
            catch (Exception)
            {
                ErrorMessage.Text = "Cannot Paste the current Text";//add error message
            }
        }
        private void logout(object sender, EventArgs e)
        {
            string msg = "Thank you for using this Software ";
            MessageBox.Show(msg, "Logout Confirmation",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Open LoginForm
            Login LoginForm = new Login();
            this.Visible = false;
            LoginForm.ShowDialog();
            this.Close();
        }

        private void aboutUs(object sender, EventArgs e)
        {
            //Open AboutForm
            About AboutForm = new About(this.currentUser);
            this.Visible = false;
            AboutForm.ShowDialog();
            this.Close();
        }

        private void toolStripBold_Click(object sender, EventArgs e)
        {
            //get previous richtextbox font, size, format
            string fontName = richTextBox.SelectionFont.Name;
            float fontSize = richTextBox.SelectionFont.Size;
            FontStyle fontStyleOld = richTextBox.SelectionFont.Style;
            FontStyle fontStyleNew;
            //if it has Bold format already
            if (fontStyleOld.ToString().Contains(FontStyle.Bold.ToString()))
                //remove Bold format
                fontStyleNew = fontStyleOld & ~FontStyle.Bold;
            else
                //add Bold format
                fontStyleNew = richTextBox.SelectionFont.Style | FontStyle.Bold;
            //update text with previous text,size + newFontFormat
            richTextBox.SelectionFont = new Font(fontName, fontSize, fontStyleNew);
        }

        private void toolStripItalic_Click(object sender, EventArgs e)
        {
            //get previous richtextbox font, size, format
            string fontName = richTextBox.SelectionFont.Name;
            float fontSize = richTextBox.SelectionFont.Size;
            FontStyle fontStyleOld = richTextBox.SelectionFont.Style;
            FontStyle fontStyleNew;
            //if it has Italic format already
            if (fontStyleOld.ToString().Contains(FontStyle.Italic.ToString()))
                //remove Italic format
                fontStyleNew = fontStyleOld & ~FontStyle.Italic;
            else
                //add Italic format
                fontStyleNew = richTextBox.SelectionFont.Style | FontStyle.Italic;
            //update text with previous text,size + newFontFormat
            richTextBox.SelectionFont = new Font(fontName, fontSize, fontStyleNew);
        }

        private void toolStripUnderline_Click(object sender, EventArgs e)
        {
            //get previous richtextbox font, size, format
            string fontName = richTextBox.SelectionFont.Name;
            float fontSize = richTextBox.SelectionFont.Size;
            FontStyle fontStyleOld = richTextBox.SelectionFont.Style;
            FontStyle fontStyleNew;
            //if it has Underline format already
            if (fontStyleOld.ToString().Contains(FontStyle.Underline.ToString()))
                //remove Underline format
                fontStyleNew = fontStyleOld & ~FontStyle.Underline;
            else
                //add Underline format
                fontStyleNew = richTextBox.SelectionFont.Style | FontStyle.Underline;
            //update text with previous text,size + newFontFormat
            richTextBox.SelectionFont = new Font(fontName, fontSize, fontStyleNew);
        }

        private void txtFontSize_onChange(object sender, EventArgs e)
        {
            //get previous richtextbox font, format
            FontFamily fontName = richTextBox.SelectionFont.FontFamily;
            FontStyle fontStyle = richTextBox.SelectionFont.Style;
            //update text with previous font, format + selected fontSize
            richTextBox.SelectionFont = new Font(fontName, Int32.Parse(txtFontSize.ComboBox.SelectedIndex.ToString().Replace("pt","")) + 8, fontStyle);
        }
        private void richTextBox_onChange(object sender, EventArgs e)
        {
            //To update fontSize on the top bar dynamically based on selected font
            try
            {
                decimal fontSizeOnLoad = Math.Round((decimal)richTextBox.SelectionFont.Size, 0);
                txtFontSize.ComboBox.SelectedIndex = (Convert.ToInt32(fontSizeOnLoad) - 8);
            }
            catch (Exception)
            {
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UrielGuy.SyntaxHighlightingTextBox;

namespace PLSimsEditor
{
    public partial class Find : Form
    {

        protected int start = -1;

        public Find()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkReplace_CheckedChanged(object sender, EventArgs e)
        {
            lblReplace.Visible = chkReplace.Checked;
            txtReplace.Visible = chkReplace.Checked;

            if (chkReplace.Checked)
            {
                btnFind.Text = "Replace";
                this.Text = "Replace";
            }
            else
            {
                btnFind.Text = "Find";
                this.Text = "Find";
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (chkReplace.Checked)
            {
                ReplaceText(txtFind.Text, txtReplace.Text);
            }
            else
            {
                FindText(txtFind.Text);
            }
        }

        private void FindText(string findText)
        {            
            SyntaxHighlightingTextBox txtBox = ((Editor)Owner).txtEditor;

            if (start == -1)
            {                
                start = txtBox.Find(findText, SearchTypes());
                
            }
            else
            {   
                start = txtBox.Find(findText, start + 1, SearchTypes());                
            }

            if (start != -1)
            {
                txtBox.Focus();                
            }

            if (start == -1)
            {
                MessageBox.Show(this,"Cannot Find the Search Text","Find",MessageBoxButtons.OK);               
            }
        }

        private void ReplaceText(string findText, string replaceText)
        {
            SyntaxHighlightingTextBox txtBox = ((Editor)Owner).txtEditor;

            start = txtBox.Find(findText,SearchTypes());

            if (start != -1)
            {
                txtBox.Focus();
              
                txtBox.SelectedText = replaceText;
                txtBox.Find(findText, SearchTypes());
            }

            if (start == -1)
            {
                MessageBox.Show(this, "Cannot Find the Search Text", "Replace", MessageBoxButtons.OK);                
            }
        }

        private RichTextBoxFinds SearchTypes()
        {
            RichTextBoxFinds findtype = RichTextBoxFinds.None;

            if (chkMatchCase.Checked)
                findtype |= RichTextBoxFinds.MatchCase;

            if (chkWholeWord.Checked)
                findtype |= RichTextBoxFinds.WholeWord;

            return findtype;
        }

        private void Find_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Editor)Owner).showFind = false;
        }
    }
}

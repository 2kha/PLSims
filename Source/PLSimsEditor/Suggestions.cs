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
    public partial class Suggestions : Form
    {
        protected List<string> suggestedWords;

        public Suggestions(List<string> words)
        {
            InitializeComponent();

            suggestedWords = words;
        }

        private void Suggestions_Load(object sender, EventArgs e)
        {
            lstWords.DataSource = suggestedWords; 
        }

        private void lstWords_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SyntaxHighlightingTextBox txtbox = ((Editor)Owner).txtEditor;
            txtbox.SelectedText =  lstWords.SelectedItem.ToString();
            
            this.Close();
        }

        private void lstWords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SyntaxHighlightingTextBox txtbox = ((Editor)Owner).txtEditor;
                txtbox.SelectedText = lstWords.SelectedItem.ToString();

                this.Close();
            }
        }

        private void Suggestions_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Editor)Owner).showSuggestion = false;
        }
    }
}

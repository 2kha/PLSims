using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PLSimsEditor
{
    public partial class ErrorMessage : Form
    {
        public ErrorMessage(string ErrorCode, string ErrorMessage, string LineNumber = "0")
        {
            InitializeComponent();

            lsvErorrs.View = View.Details;

            ListViewItem item = new ListViewItem();

            item.Text = ErrorCode;
            item.SubItems.Add(ErrorMessage);
            item.SubItems.Add(LineNumber);

            lsvErorrs.Items.Add(item);
            
        }

        private void ErrorMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Editor)Owner).showError = false;
        }
    }
}

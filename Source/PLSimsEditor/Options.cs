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
    public partial class Options : Form
    {
        protected Descriptors descriptors;

        public Descriptors TextDescriptors { get { return descriptors; } }

        public Options()
        {
            InitializeComponent();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg =  ((Editor)Owner).fontDlg;

            var result = fontDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                lblFont.Text = fontDlg.Font.Name;
                lblFont.Font = fontDlg.Font;
                lblDisplay.Font = fontDlg.Font;

                btnApply.Enabled = true;
            }
        }

        private void ForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = ((Editor)Owner).colorDlg;

            var result = colorDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                panForeColor.BackColor = colorDlg.Color;
                lblDisplay.ForeColor = colorDlg.Color;

                btnApply.Enabled = true;
            }

        }

        private void BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = ((Editor)Owner).colorDlg;

            var result = colorDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                panBackColor.BackColor = colorDlg.Color;
                lblDisplay.BackColor = colorDlg.Color;

                btnApply.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            descriptors = ((Editor)Owner).TextDescriptors;
            lstDescriptors.DataSource = descriptors;
            lstDescriptors.DisplayMember = "Name";

            lblFont.Text = descriptors.Font.Name;
            lblFont.Font = lblFont.Font;

            panForeColor.BackColor = ((Descriptor)lstDescriptors.SelectedItem).ForeColor;
            panBackColor.BackColor = descriptors.BackColor;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            descriptors.Font = lblFont.Font;
            descriptors.BackColor = panBackColor.BackColor;
            descriptors[lstDescriptors.SelectedIndex].ForeColor = panForeColor.BackColor;
            
            btnApply.Enabled = false;

        }

        private void lstDescriptors_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            lblFont.Font = descriptors.Font;
            lblFont.Text = lblFont.Font.Name;

            panForeColor.BackColor = ((Descriptor)lstDescriptors.SelectedItem).ForeColor;
            panBackColor.BackColor = descriptors.BackColor;

            lblDisplay.Font = lblFont.Font;
            lblDisplay.ForeColor = panForeColor.BackColor;
            lblDisplay.BackColor = panBackColor.BackColor;
            lblDescription.Text = ((Descriptor)lstDescriptors.SelectedItem).Description;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ((Editor)Owner).TextDescriptors = descriptors;
            ((Editor)Owner).LoadEditorSettings();

            Close();

        }

       
    }
}

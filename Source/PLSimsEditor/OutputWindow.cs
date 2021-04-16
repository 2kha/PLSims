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
    public partial class OutputWindow : Form
    {
        public OutputWindow()
        {
            InitializeComponent();
        }

        private void OutputWindow_Load(object sender, EventArgs e)
        {
            txtOutput.Text = PLSimsLibrary.PLSims.Evaluation.OUTPUT_STRING;
        }
    }
}

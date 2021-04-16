namespace PLSimsEditor
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFonts = new System.Windows.Forms.Label();
            this.lstDescriptors = new System.Windows.Forms.ListBox();
            this.lblFont = new System.Windows.Forms.Label();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.btnFont = new System.Windows.Forms.Button();
            this.lblSample = new System.Windows.Forms.Label();
            this.groupColor = new System.Windows.Forms.GroupBox();
            this.panBackColor = new System.Windows.Forms.Panel();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblForeColor = new System.Windows.Forms.Label();
            this.panForeColor = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.groupColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFonts
            // 
            this.lblFonts.AutoSize = true;
            this.lblFonts.Location = new System.Drawing.Point(11, 15);
            this.lblFonts.Name = "lblFonts";
            this.lblFonts.Size = new System.Drawing.Size(28, 13);
            this.lblFonts.TabIndex = 0;
            this.lblFonts.Text = "Font";
            // 
            // lstDescriptors
            // 
            this.lstDescriptors.Location = new System.Drawing.Point(14, 108);
            this.lstDescriptors.Name = "lstDescriptors";
            this.lstDescriptors.Size = new System.Drawing.Size(195, 147);
            this.lstDescriptors.TabIndex = 1;
            this.lstDescriptors.SelectedIndexChanged += new System.EventHandler(this.lstDescriptors_SelectedIndexChanged);
            // 
            // lblFont
            // 
            this.lblFont.BackColor = System.Drawing.Color.White;
            this.lblFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFont.Location = new System.Drawing.Point(12, 40);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(197, 26);
            this.lblFont.TabIndex = 2;
            this.lblFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDisplay
            // 
            this.lblDisplay.BackColor = System.Drawing.Color.White;
            this.lblDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDisplay.Location = new System.Drawing.Point(215, 200);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(265, 55);
            this.lblDisplay.TabIndex = 4;
            this.lblDisplay.Text = "ij : J * ( ) + end";
            this.lblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(212, 39);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(31, 30);
            this.btnFont.TabIndex = 8;
            this.btnFont.Text = "...";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // lblSample
            // 
            this.lblSample.AutoSize = true;
            this.lblSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSample.Location = new System.Drawing.Point(307, 173);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(61, 16);
            this.lblSample.TabIndex = 9;
            this.lblSample.Text = "Sample";
            // 
            // groupColor
            // 
            this.groupColor.Controls.Add(this.panBackColor);
            this.groupColor.Controls.Add(this.lblBackColor);
            this.groupColor.Controls.Add(this.lblForeColor);
            this.groupColor.Controls.Add(this.panForeColor);
            this.groupColor.Location = new System.Drawing.Point(255, 30);
            this.groupColor.Name = "groupColor";
            this.groupColor.Size = new System.Drawing.Size(207, 123);
            this.groupColor.TabIndex = 12;
            this.groupColor.TabStop = false;
            this.groupColor.Text = "Color";
            // 
            // panBackColor
            // 
            this.panBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panBackColor.Location = new System.Drawing.Point(110, 78);
            this.panBackColor.Name = "panBackColor";
            this.panBackColor.Size = new System.Drawing.Size(32, 32);
            this.panBackColor.TabIndex = 15;
            this.panBackColor.Click += new System.EventHandler(this.BackColor_Click);
            // 
            // lblBackColor
            // 
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(39, 85);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(65, 13);
            this.lblBackColor.TabIndex = 14;
            this.lblBackColor.Text = "Back Color :";
            // 
            // lblForeColor
            // 
            this.lblForeColor.AutoSize = true;
            this.lblForeColor.Location = new System.Drawing.Point(39, 39);
            this.lblForeColor.Name = "lblForeColor";
            this.lblForeColor.Size = new System.Drawing.Size(61, 13);
            this.lblForeColor.TabIndex = 13;
            this.lblForeColor.Text = "Fore Color :";
            // 
            // panForeColor
            // 
            this.panForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panForeColor.Location = new System.Drawing.Point(110, 29);
            this.panForeColor.Name = "panForeColor";
            this.panForeColor.Size = new System.Drawing.Size(32, 32);
            this.panForeColor.TabIndex = 12;
            this.panForeColor.Click += new System.EventHandler(this.ForeColor_Click);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(247, 325);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(70, 30);
            this.btnApply.TabIndex = 13;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(402, 325);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Descriptors";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(321, 325);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 30);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDescription.Location = new System.Drawing.Point(12, 266);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(469, 44);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupColor);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.lblDisplay);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.lstDescriptors);
            this.Controls.Add(this.lblFonts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.groupColor.ResumeLayout(false);
            this.groupColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFonts;
        private System.Windows.Forms.ListBox lstDescriptors;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.GroupBox groupColor;
        private System.Windows.Forms.Panel panBackColor;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.Label lblForeColor;
        private System.Windows.Forms.Panel panForeColor;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDescription;
    }
}
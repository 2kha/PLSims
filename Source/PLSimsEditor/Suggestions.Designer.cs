namespace PLSimsEditor
{
    partial class Suggestions
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
            this.lstWords = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstWords
            // 
            this.lstWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstWords.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstWords.FormattingEnabled = true;
            this.lstWords.ItemHeight = 16;
            this.lstWords.Location = new System.Drawing.Point(0, 0);
            this.lstWords.Name = "lstWords";
            this.lstWords.Size = new System.Drawing.Size(168, 163);
            this.lstWords.Sorted = true;
            this.lstWords.TabIndex = 0;
            this.lstWords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstWords_KeyDown);
            this.lstWords.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstWords_MouseDoubleClick);
            // 
            // Suggestions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(168, 163);
            this.Controls.Add(this.lstWords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Suggestions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Suggestions";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Suggestions_FormClosing);
            this.Load += new System.EventHandler(this.Suggestions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstWords;

    }
}
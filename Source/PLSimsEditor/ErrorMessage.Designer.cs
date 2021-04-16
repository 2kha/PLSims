namespace PLSimsEditor
{
    partial class ErrorMessage
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
            this.lsvErorrs = new System.Windows.Forms.ListView();
            this.ErrorCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ErrorMsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LineNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lsvErorrs
            // 
            this.lsvErorrs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ErrorCode,
            this.ErrorMsg,
            this.LineNo});
            this.lsvErorrs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvErorrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvErorrs.ForeColor = System.Drawing.Color.Red;
            this.lsvErorrs.FullRowSelect = true;
            this.lsvErorrs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvErorrs.Location = new System.Drawing.Point(0, 0);
            this.lsvErorrs.Name = "lsvErorrs";
            this.lsvErorrs.Size = new System.Drawing.Size(512, 158);
            this.lsvErorrs.TabIndex = 0;
            this.lsvErorrs.UseCompatibleStateImageBehavior = false;
            // 
            // ErrorCode
            // 
            this.ErrorCode.Text = "Error Code";
            this.ErrorCode.Width = 100;
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.Text = "Error Message";
            this.ErrorMsg.Width = 300;
            // 
            // LineNo
            // 
            this.LineNo.Text = "Line Number";
            this.LineNo.Width = 100;
            // 
            // ErrorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 158);
            this.Controls.Add(this.lsvErorrs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ErrorMessage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error Message";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ErrorMessage_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvErorrs;
        private System.Windows.Forms.ColumnHeader ErrorCode;
        private System.Windows.Forms.ColumnHeader ErrorMsg;
        private System.Windows.Forms.ColumnHeader LineNo;
    }
}
namespace PLSimsEditor
{
    partial class Editor
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
            this.components = new System.ComponentModel.Container();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRunOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsSuggestions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.Line = new System.Windows.Forms.ToolStripStatusLabel();
            this.Column = new System.Windows.Forms.ToolStripStatusLabel();
            this.Length = new System.Windows.Forms.ToolStripStatusLabel();
            this.Lines = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSep1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.InsertNotification = new System.Windows.Forms.ToolStripStatusLabel();
            this.changeNotification = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.undoContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.redoContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.copyContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.clearContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Margin = new System.Windows.Forms.Panel();
            this.LineNumber = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtEditor = new UrielGuy.SyntaxHighlightingTextBox.SyntaxHighlightingTextBox();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.fontDlg = new System.Windows.Forms.FontDialog();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.MainMenuStrip.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.txtContextMenu.SuspendLayout();
            this.Margin.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuRun,
            this.mnuTools,
            this.mnuHelp});
            this.MainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainMenuStrip.Size = new System.Drawing.Size(715, 24);
            this.MainMenuStrip.TabIndex = 1;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.toolStripMenuItem10,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.toolStripMenuItem13,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(120, 22);
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(120, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(117, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(120, 22);
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(120, 22);
            this.mnuFileSaveAs.Text = "Save As..";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(117, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(120, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditUndo,
            this.mnuEditRedo,
            this.toolStripMenuItem5,
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditDelete,
            this.toolStripMenuItem6,
            this.mnuEditSelectAll,
            this.mnuEditClear,
            this.toolStripMenuItem3,
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditUndo
            // 
            this.mnuEditUndo.Name = "mnuEditUndo";
            this.mnuEditUndo.Size = new System.Drawing.Size(122, 22);
            this.mnuEditUndo.Text = "Undo";
            this.mnuEditUndo.Click += new System.EventHandler(this.mnuEditUndo_Click);
            // 
            // mnuEditRedo
            // 
            this.mnuEditRedo.Name = "mnuEditRedo";
            this.mnuEditRedo.Size = new System.Drawing.Size(122, 22);
            this.mnuEditRedo.Text = "Redo";
            this.mnuEditRedo.Click += new System.EventHandler(this.mnuEditRedo_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(119, 6);
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(122, 22);
            this.mnuEditCut.Text = "Cut";
            this.mnuEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(122, 22);
            this.mnuEditCopy.Text = "Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(122, 22);
            this.mnuEditPaste.Text = "Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
            // 
            // mnuEditDelete
            // 
            this.mnuEditDelete.Name = "mnuEditDelete";
            this.mnuEditDelete.Size = new System.Drawing.Size(122, 22);
            this.mnuEditDelete.Text = "Delete";
            this.mnuEditDelete.Click += new System.EventHandler(this.mnuEditDelete_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(119, 6);
            // 
            // mnuEditSelectAll
            // 
            this.mnuEditSelectAll.Name = "mnuEditSelectAll";
            this.mnuEditSelectAll.Size = new System.Drawing.Size(122, 22);
            this.mnuEditSelectAll.Text = "Select All";
            this.mnuEditSelectAll.Click += new System.EventHandler(this.mnuEditSelectAll_Click);
            // 
            // mnuEditClear
            // 
            this.mnuEditClear.Name = "mnuEditClear";
            this.mnuEditClear.Size = new System.Drawing.Size(122, 22);
            this.mnuEditClear.Text = "Clear";
            this.mnuEditClear.Click += new System.EventHandler(this.mnuEditClear_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(119, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.findToolStripMenuItem.Text = "&Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.replaceToolStripMenuItem.Text = "&Replace";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // mnuRun
            // 
            this.mnuRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunRun,
            this.mnuRunClear,
            this.toolStripSeparator1,
            this.mnuRunOutput});
            this.mnuRun.Name = "mnuRun";
            this.mnuRun.Size = new System.Drawing.Size(40, 20);
            this.mnuRun.Text = "&Run";
            // 
            // mnuRunRun
            // 
            this.mnuRunRun.Name = "mnuRunRun";
            this.mnuRunRun.Size = new System.Drawing.Size(112, 22);
            this.mnuRunRun.Text = "&Run";
            this.mnuRunRun.Click += new System.EventHandler(this.mnuRunRun_Click);
            // 
            // mnuRunClear
            // 
            this.mnuRunClear.Name = "mnuRunClear";
            this.mnuRunClear.Size = new System.Drawing.Size(112, 22);
            this.mnuRunClear.Text = "&Clear";
            this.mnuRunClear.Click += new System.EventHandler(this.mnuRunClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // mnuRunOutput
            // 
            this.mnuRunOutput.Name = "mnuRunOutput";
            this.mnuRunOutput.Size = new System.Drawing.Size(112, 22);
            this.mnuRunOutput.Text = "&Output";
            this.mnuRunOutput.Click += new System.EventHandler(this.mnuRunOutput_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsOptions,
            this.mnuToolsSuggestions});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(46, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsOptions
            // 
            this.mnuToolsOptions.Name = "mnuToolsOptions";
            this.mnuToolsOptions.Size = new System.Drawing.Size(138, 22);
            this.mnuToolsOptions.Text = "&Options";
            this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
            // 
            // mnuToolsSuggestions
            // 
            this.mnuToolsSuggestions.Name = "mnuToolsSuggestions";
            this.mnuToolsSuggestions.Size = new System.Drawing.Size(138, 22);
            this.mnuToolsSuggestions.Text = "&Suggestions";
            this.mnuToolsSuggestions.Click += new System.EventHandler(this.mnuToolsSuggestions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Line,
            this.Column,
            this.Length,
            this.Lines,
            this.statusSep1,
            this.InsertNotification,
            this.changeNotification});
            this.StatusBar.Location = new System.Drawing.Point(0, 428);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(715, 24);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "statusStrip1";
            // 
            // Line
            // 
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(35, 19);
            this.Line.Text = "Ln : 1";
            // 
            // Column
            // 
            this.Column.Name = "Column";
            this.Column.Size = new System.Drawing.Size(40, 19);
            this.Column.Text = "Col : 1";
            // 
            // Length
            // 
            this.Length.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(63, 19);
            this.Length.Text = "Length : 0";
            // 
            // Lines
            // 
            this.Lines.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.Lines.Name = "Lines";
            this.Lines.Size = new System.Drawing.Size(53, 19);
            this.Lines.Text = "Lines : 0";
            // 
            // statusSep1
            // 
            this.statusSep1.Name = "statusSep1";
            this.statusSep1.Size = new System.Drawing.Size(444, 19);
            this.statusSep1.Spring = true;
            // 
            // InsertNotification
            // 
            this.InsertNotification.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.InsertNotification.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.InsertNotification.Name = "InsertNotification";
            this.InsertNotification.Size = new System.Drawing.Size(29, 19);
            this.InsertNotification.Text = "INS";
            this.InsertNotification.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // changeNotification
            // 
            this.changeNotification.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.changeNotification.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.changeNotification.ForeColor = System.Drawing.Color.Silver;
            this.changeNotification.Name = "changeNotification";
            this.changeNotification.Size = new System.Drawing.Size(36, 19);
            this.changeNotification.Text = "CHG";
            // 
            // txtContextMenu
            // 
            this.txtContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoContextMenu,
            this.redoContextMenu,
            this.toolStripMenuItem1,
            this.cutContextMenu,
            this.copyContextMenu,
            this.pasteContextMenu,
            this.deleteContextMenu,
            this.toolStripMenuItem2,
            this.selectAllContextMenu,
            this.clearContextMenu});
            this.txtContextMenu.Name = "txtContextMenu";
            this.txtContextMenu.Size = new System.Drawing.Size(123, 192);
            // 
            // undoContextMenu
            // 
            this.undoContextMenu.Name = "undoContextMenu";
            this.undoContextMenu.Size = new System.Drawing.Size(122, 22);
            this.undoContextMenu.Text = "Undo";
            this.undoContextMenu.Click += new System.EventHandler(this.undoContextMenu_Click);
            // 
            // redoContextMenu
            // 
            this.redoContextMenu.Name = "redoContextMenu";
            this.redoContextMenu.Size = new System.Drawing.Size(122, 22);
            this.redoContextMenu.Text = "Redo";
            this.redoContextMenu.Click += new System.EventHandler(this.redoContextMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(119, 6);
            // 
            // cutContextMenu
            // 
            this.cutContextMenu.Name = "cutContextMenu";
            this.cutContextMenu.Size = new System.Drawing.Size(122, 22);
            this.cutContextMenu.Text = "Cut";
            this.cutContextMenu.Click += new System.EventHandler(this.cutContextMenu_Click);
            // 
            // copyContextMenu
            // 
            this.copyContextMenu.Name = "copyContextMenu";
            this.copyContextMenu.Size = new System.Drawing.Size(122, 22);
            this.copyContextMenu.Text = "Copy";
            this.copyContextMenu.Click += new System.EventHandler(this.copyContextMenu_Click);
            // 
            // pasteContextMenu
            // 
            this.pasteContextMenu.Name = "pasteContextMenu";
            this.pasteContextMenu.Size = new System.Drawing.Size(122, 22);
            this.pasteContextMenu.Text = "Paste";
            this.pasteContextMenu.Click += new System.EventHandler(this.pasteContextMenu_Click);
            // 
            // deleteContextMenu
            // 
            this.deleteContextMenu.Name = "deleteContextMenu";
            this.deleteContextMenu.Size = new System.Drawing.Size(122, 22);
            this.deleteContextMenu.Text = "Delete";
            this.deleteContextMenu.Click += new System.EventHandler(this.deleteContextMenu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(119, 6);
            // 
            // selectAllContextMenu
            // 
            this.selectAllContextMenu.Name = "selectAllContextMenu";
            this.selectAllContextMenu.Size = new System.Drawing.Size(122, 22);
            this.selectAllContextMenu.Text = "Select All";
            this.selectAllContextMenu.Click += new System.EventHandler(this.selectAllContextMenu_Click);
            // 
            // clearContextMenu
            // 
            this.clearContextMenu.Name = "clearContextMenu";
            this.clearContextMenu.Size = new System.Drawing.Size(122, 22);
            this.clearContextMenu.Text = "Clear";
            this.clearContextMenu.Click += new System.EventHandler(this.clearContextMenu_Click);
            // 
            // Margin
            // 
            this.Margin.AutoScroll = true;
            this.Margin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Margin.BackColor = System.Drawing.Color.Gainsboro;
            this.Margin.Controls.Add(this.LineNumber);
            this.Margin.Dock = System.Windows.Forms.DockStyle.Left;
            this.Margin.Location = new System.Drawing.Point(0, 24);
            this.Margin.Name = "Margin";
            this.Margin.Size = new System.Drawing.Size(21, 404);
            this.Margin.TabIndex = 4;
            // 
            // LineNumber
            // 
            this.LineNumber.AutoSize = true;
            this.LineNumber.BackColor = System.Drawing.Color.Transparent;
            this.LineNumber.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineNumber.Location = new System.Drawing.Point(0, 0);
            this.LineNumber.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.Size = new System.Drawing.Size(0, 17);
            this.LineNumber.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtEditor);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(21, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(694, 404);
            this.panel2.TabIndex = 5;
            // 
            // txtEditor
            // 
            this.txtEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEditor.CaseSensitive = false;
            this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditor.FilterAutoComplete = false;
            this.txtEditor.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEditor.Location = new System.Drawing.Point(0, 0);
            this.txtEditor.MaxUndoRedoSteps = 50;
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.Size = new System.Drawing.Size(694, 404);
            this.txtEditor.TabIndex = 4;
            this.txtEditor.TabStop = false;
            this.txtEditor.Text = "";
            this.txtEditor.SelectionChanged += new System.EventHandler(this.txtEditor_SelectionChanged);
            this.txtEditor.TextChanged += new System.EventHandler(this.txtEditor_TextChanged);
            this.txtEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEditor_KeyDown);
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.Open);
            // 
            // saveFileDlg
            // 
            this.saveFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.Save);
            // 
            // fontDlg
            // 
            this.fontDlg.Apply += new System.EventHandler(this.Font_Apply);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 452);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Margin);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "Editor";
            this.Text = "PLSims - Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.txtContextMenu.ResumeLayout(false);
            this.Margin.ResumeLayout(false);
            this.Margin.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuRun;
        private System.Windows.Forms.ToolStripMenuItem mnuRunOutput;
        private System.Windows.Forms.ToolStripMenuItem mnuRunRun;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripStatusLabel Line;
        private System.Windows.Forms.ToolStripStatusLabel Column;
        private System.Windows.Forms.ToolStripStatusLabel Length;
        private System.Windows.Forms.ToolStripStatusLabel Lines;
        private System.Windows.Forms.ContextMenuStrip txtContextMenu;
        private System.Windows.Forms.ToolStripMenuItem undoContextMenu;
        private System.Windows.Forms.ToolStripMenuItem redoContextMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cutContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyContextMenu;
        private System.Windows.Forms.ToolStripMenuItem pasteContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteContextMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem selectAllContextMenu;
        private System.Windows.Forms.ToolStripMenuItem clearContextMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditUndo;
        private System.Windows.Forms.ToolStripMenuItem mnuEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCut;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuEditDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSelectAll;
        private System.Windows.Forms.ToolStripMenuItem mnuEditClear;
        private System.Windows.Forms.ToolStripMenuItem mnuRunClear;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel Margin;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LineNumber;
        public UrielGuy.SyntaxHighlightingTextBox.SyntaxHighlightingTextBox txtEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        public System.Windows.Forms.FontDialog fontDlg;
        public System.Windows.Forms.ColorDialog colorDlg;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel InsertNotification;
        private System.Windows.Forms.ToolStripStatusLabel changeNotification;
        private System.Windows.Forms.ToolStripStatusLabel statusSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsSuggestions;
    }
}
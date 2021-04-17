using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UrielGuy.SyntaxHighlightingTextBox;
using PLSimsLibrary;

namespace PLSimsEditor
{
    public partial class Editor : Form
    {

        protected string CurrentFile;
        protected bool insertMode = false;
        public bool showSuggestion = false;
        public bool showFind = false;
        public bool showError = false;

        public Descriptors TextDescriptors { get; set; }
        protected List<string> suggestedWrods;

        public List<string> SuggestedWords { get { return suggestedWrods; } }

        public Editor()
        {
            InitializeComponent();
        }

        private void LoadSettings()
        {
            this.Text = "PLSims - Editor - Untitled";
            System.Console.Title = "PLSims - Console";

            txtEditor.Location = new Point(0, 0);
            txtEditor.Dock = DockStyle.Fill;

            txtEditor.Margin = new Padding(3, 3, 3, 3);
            txtEditor.ShowSelectionMargin = true;
            txtEditor.ContextMenuStrip = txtContextMenu;

            txtEditor.Seperators.Add(' ');
            txtEditor.Seperators.Add('\r');
            txtEditor.Seperators.Add('\n');
            txtEditor.Seperators.Add('-');
            txtEditor.Seperators.Add('+');
            txtEditor.Seperators.Add('*');
            txtEditor.Seperators.Add('/');
            txtEditor.Seperators.Add('(');
            txtEditor.Seperators.Add(')');
            txtEditor.Seperators.Add('[');
            txtEditor.Seperators.Add(']');
            txtEditor.Seperators.Add('\t');
            txtEditor.Seperators.Add(',');
            txtEditor.Seperators.Add('.');
            txtEditor.Seperators.Add('\"');
            txtEditor.Seperators.Add('=');
            txtEditor.Seperators.Add(':');           
            txtEditor.Seperators.Add('%');

            


            txtEditor.WordWrap = false;
            txtEditor.ScrollBars = RichTextBoxScrollBars.Both;

            txtEditor.FilterAutoComplete = false;

            TextDescriptors = new Descriptors();

            TextDescriptors.Font = new System.Drawing.Font("Courier New", 11);
            TextDescriptors.BackColor = Color.White;

            Descriptor commentDescriptor = new Descriptor();

            commentDescriptor.Name = "Comment";            
            commentDescriptor.ForeColor = Color.DarkGreen;            
            commentDescriptor.Description = "Comment";

            TextDescriptors.Add(commentDescriptor);

            Descriptor keywordDescriptor = new Descriptor();

            keywordDescriptor.Name = "Keyword";           
            keywordDescriptor.ForeColor = Color.Blue;           
            keywordDescriptor.Description = "Keywords";

            TextDescriptors.Add(keywordDescriptor);

            Descriptor stringDescriptor = new Descriptor();

            stringDescriptor.Name = "String";            
            stringDescriptor.ForeColor = Color.DarkRed;           
            stringDescriptor.Description = "String";

            TextDescriptors.Add(stringDescriptor);

            Descriptor functionDescriptor = new Descriptor();

            functionDescriptor.Name = "Function";            
            functionDescriptor.ForeColor = Color.DarkCyan;            
            functionDescriptor.Description = "Function";

            TextDescriptors.Add(functionDescriptor);          


            Descriptor regularDescriptor = new Descriptor();

            regularDescriptor.Name = "Regular";
            regularDescriptor.ForeColor = Color.Black;
            regularDescriptor.Description = "Regular Expression";

            TextDescriptors.Add(regularDescriptor);
        }        

        private void Editor_Load(object sender, EventArgs e)
        {
            LoadSettings();
            LoadEditorSettings();

            suggestedWrods = new List<string>();

            foreach (HighlightDescriptor descrpt in txtEditor.HighlightDescriptors)
            {
                suggestedWrods.Add(descrpt.Token[0].ToString().ToUpper() + descrpt.Token.Substring(1,descrpt.Token.Length -1).ToLower());
            }

            ContextMenuSettings();            
        }

        [STAThread]
        static void Main()
        {
            Application.Run(new Editor());
        }

        public  void LoadEditorSettings()
        {
            txtEditor.HighlightDescriptors.Clear();

            string text = txtEditor.Text;

            txtEditor.Rtf = string.Empty;
            txtEditor.ResetText();
            
            //backColor 
            txtEditor.BackColor = TextDescriptors.BackColor;

            //Regular Color
            txtEditor.ForeColor = TextDescriptors["Regular"].ForeColor;


            //Font
            txtEditor.Font = TextDescriptors.Font;

            //String

            Color StringColor = TextDescriptors["String"].ForeColor;
            System.Drawing.Font stringFont = null;

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor("\"", "\"",StringColor , stringFont , DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, false));
          
            //KeyWords

            Color KeyWordColor = TextDescriptors["Keyword"].ForeColor;
            System.Drawing.Font KeywordFont = null;

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_AND, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_BOOLEAN, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_BR, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_ELSE, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_END, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_ENTER, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_FALSE, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_FUNC, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_IF, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_MAIN, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_NOT, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_NPSP, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_NUMBER, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_OBJECT, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_OR, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_PROC, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_PROG, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_REPEAT, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_RETURN, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_SPACE, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_STRING, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_TAB, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_THEN, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_TRUE, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_UNTIL, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_WHILE, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_XOR, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_NULL, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_VOID, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Interpretation.KW_ARRAY, KeyWordColor, KeywordFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            

            //Functions

            Color FunctionColor = TextDescriptors["Function"].ForeColor;
            System.Drawing.Font FunctionFont = null;

            //TODO: Commented on May 08, 2015.
            //txtEditor.HighlightDescriptors.Add(new HighlightDescriptor("[","]", FunctionColor, FunctionFont, DescriptorType.ToCloseToken, DescriptorRecognition.Contains,false));
            
            //Math Funciton
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_ABS, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_CEIL, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_FLOOR, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_RND, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_COS, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_SIN, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_TAN, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_RAD, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_DEG, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_LN, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_EXP, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_FACTO, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            
            
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_NEG, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));            
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_PI, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            
            //Stastical Functions
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_RANDOM, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            //Conversion Functions
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_NUMERIC, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_LOGIC, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            //String Functions
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_STR, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_JOIN, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            //IO Functions

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_INPUT, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.PROC_MSGBOX, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.PROC_OUTPUT, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.PROC_WRITE, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
           
            //Array Functions
            
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_LOWERBOUND, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_UPPERBOUND, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_JSON, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_TOJSON, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_GETJSON, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_LOADJSON, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_SEARCHBYKEYVALUE, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_SEARCHBYKEY, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_SUM, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_PRODUCT, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_MEAN, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_STD, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_VARIANCE, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_MIN, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor(PLSimsLibrary.PLSims.Evaluation.FUNC_MAX, FunctionColor, FunctionFont, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            //Comment

            Color CommentColor = TextDescriptors["Comment"].ForeColor;
            System.Drawing.Font CommentFont = null;

            txtEditor.HighlightDescriptors.Add(new HighlightDescriptor("@", CommentColor, CommentFont, DescriptorType.ToEOL, DescriptorRecognition.StartsWith, false));
                       

            txtEditor.Text = text;
            txtEditor.Refresh();
        }

        private void mnuRunRun_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;            
                
            PLSimsLibrary.PLSims.Control.RUN(txtEditor.Text);

            this.Cursor = Cursors.Default;

            if (PLSimsLibrary.PLSims.Control.SYS_ERR_CODE > 0)
            {
                if (!showError)
                {
                    int ErrCode = PLSimsLibrary.PLSims.Control.SYS_ERR_CODE;

                    int Index = txtEditor.Find(PLSimsLibrary.PLSims.Control.Current_Instruction);

                    string LineNumber = (txtEditor.GetLineFromCharIndex(Index) + 1).ToString();

                    ErrorMessage errMsg = new ErrorMessage(ErrCode.ToString(), PLSimsLibrary.PLSims.Debugging.ERROR_MSG[ErrCode],LineNumber);
                    showError = true;
                    errMsg.Owner = this;
                    errMsg.Show();

                    txtEditor.Focus();
                }
            }
        }

        private void mnuRunOutput_Click(object sender, EventArgs e)
        {
            OutputWindow output = new OutputWindow();
            output.Show();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            About frmAbout = new About();
            frmAbout.ShowDialog();
        }

        private void txtEditor_SelectionChanged(object sender, EventArgs e)
        {
            Line.Text = "Ln : " + (txtEditor.GetLineFromCharIndex(txtEditor.SelectionStart) + 1).ToString();
            Column.Text = "Col : " + (txtEditor.SelectionStart - txtEditor.GetFirstCharIndexOfCurrentLine() + 1).ToString();
        }

        private void ContextMenuSettings()
        {
            undoContextMenu.Enabled = txtEditor.CanUndo;
            mnuEditUndo.Enabled = txtEditor.CanUndo;

            redoContextMenu.Enabled = txtEditor.CanRedo;
            mnuEditRedo.Enabled = txtEditor.CanRedo;

            pasteContextMenu.Enabled = txtEditor.CanPaste(DataFormats.GetFormat(0));
            mnuEditPaste.Enabled = txtEditor.CanPaste(DataFormats.GetFormat(0));
           
            selectAllContextMenu.Enabled = txtEditor.TextLength > 0;
            mnuEditSelectAll.Enabled = txtEditor.TextLength > 0;

            clearContextMenu.Enabled = txtEditor.TextLength > 0;
            mnuEditClear.Enabled = txtEditor.TextLength > 0;
        }

        private void txtEditor_TextChanged(object sender, EventArgs e)
        {            

            Lines.Text = "Lines : " + (txtEditor.Lines.Length).ToString();
            Length.Text = "Length : " + txtEditor.TextLength.ToString();

            if (txtEditor.Modified)
            {
                changeNotification.ForeColor = Color.Blue;                
            }
            else
            {
                changeNotification.ForeColor = Color.DarkGray;
            }

            ContextMenuSettings();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            openFileDlg.Filter = "Text Files (*.txt)|*.txt|PLSims Files (*.pls)|*.pls|All Files (*.*)|*.*";
            openFileDlg.Title = this.Text + " - Open File ";
            openFileDlg.FilterIndex = 2;
            openFileDlg.ShowDialog();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        { 
            Application.Exit();
        }

        private void SaveFileAs()
        {
            saveFileDlg.Filter = "Text Files (*.txt)|*.txt|PLSims Files (*.pls)|*.pls|All Files (*.*)|*.*";
            saveFileDlg.Title = this.Text + " - Save File";
            saveFileDlg.FilterIndex = 2;
            saveFileDlg.ShowDialog();
        }

        private void SaveFile()
        {
            if (!string.IsNullOrEmpty(CurrentFile))
            {
                DialogResult answer = MessageBox.Show(this, "Do you want to save the file?", this.Text, MessageBoxButtons.OKCancel);

                if (answer == System.Windows.Forms.DialogResult.OK)
                {

                    try
                    {

                        this.Cursor = Cursors.WaitCursor;
                        txtEditor.SaveFile(CurrentFile, RichTextBoxStreamType.PlainText);

                        txtEditor.Modified = false;
                        changeNotification.ForeColor = Color.DarkGray;

                        this.Cursor = Cursors.Default;
                    }
                    catch (System.IO.IOException ex)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(this, ex.Message,this.Text);
                    }
                }
                
            }
            else
            {
                SaveFileAs();
            }
        }


        private void Open(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(openFileDlg.FileName))
            {
                txtEditor.Clear();
                txtEditor.ResetText();

                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    var fileStream = openFileDlg.OpenFile();

                    txtEditor.LoadFile(fileStream, RichTextBoxStreamType.PlainText);
                    CurrentFile = openFileDlg.FileName;
                    this.Text = "PLSims - Editor - " + CurrentFile;
                    this.Cursor = Cursors.Default;
                    
                    fileStream.Close();
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(this, ex.Message,this.Text);
                }
            }
        }

        private void Save(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(saveFileDlg.FileName))
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    txtEditor.SaveFile(saveFileDlg.FileName, RichTextBoxStreamType.PlainText);
                    CurrentFile = saveFileDlg.FileName;
                    this.Text = "PLSims - Editor - " + CurrentFile;
                    this.Cursor = Cursors.Default;
                }
                catch (System.IO.IOException ex) 
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(this, ex.Message,this.Text);
                }
            }           

        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            if (txtEditor.Modified)
            {
                DialogResult answer = MessageBox.Show(this, "Do you want to save the file?", this.Text, MessageBoxButtons.YesNoCancel);

                if (answer == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (answer == DialogResult.Cancel)
                {
                    return;
                }
            }

            CurrentFile = string.Empty;
            this.Text = "PLSims - Editor - Untitled";
            txtEditor.Clear();
            txtEditor.ResetText();
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        public void Font_Apply(object sender, EventArgs e)
        {
           
        }

        private void mnuToolsOptions_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Owner = this;
            options.ShowDialog();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showFind)
            {
                Find findform = new Find();
                findform.Owner = this;
                showFind = true;

                findform.Show();
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showFind)
            {
                Find findform = new Find();
                findform.Owner = this;
                findform.chkReplace.Checked = true;
                showFind = true;

                findform.Show();
            }

        }

        private void mnuEditUndo_Click(object sender, EventArgs e)
        {
            txtEditor.Undo();
        }

        private void mnuEditRedo_Click(object sender, EventArgs e)
        {
            txtEditor.Redo();
        }

        private void mnuEditCut_Click(object sender, EventArgs e)
        {
            txtEditor.Cut();
        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {
            txtEditor.Copy();
        }

        private void mnuEditPaste_Click(object sender, EventArgs e)
        {
            txtEditor.Paste();
        }

        private void mnuEditDelete_Click(object sender, EventArgs e)
        {
            txtEditor.SelectedText = string.Empty;
        }

        private void mnuEditSelectAll_Click(object sender, EventArgs e)
        {
            txtEditor.SelectAll();
        }

        private void mnuEditClear_Click(object sender, EventArgs e)
        {
            txtEditor.Clear();
            
        }

        private void mnuRunClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            System.Console.Clear();
            PLSimsLibrary.PLSims.Control.Clear();
            this.Cursor = Cursors.Default;
        }

        private void undoContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.Undo();
        }

        private void redoContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.Redo();
        }

        private void cutContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.Cut();
        }

        private void copyContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.Copy();
        }

        private void pasteContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.Paste();
        }

        private void deleteContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.SelectedText = string.Empty;
        }

        private void selectAllContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.SelectAll();
        }

        private void clearContextMenu_Click(object sender, EventArgs e)
        {
            txtEditor.Clear();
        }

        private void txtEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
               insertMode = !insertMode;
            }

            if (e.KeyCode == Keys.Space  && e.Control)
            {
                //if (!showSuggestion)
                //{
                //    Suggestions suggestion = new Suggestions(SuggestedWords);
                //    suggestion.Owner = this;
                //    showSuggestion = true;

                //    suggestion.Top = MousePosition.Y;
                //    suggestion.Left = MousePosition.X;
                //    suggestion.Show();
                //}
            }

            if (insertMode)
            {
                InsertNotification.Text = "OVR";
            }
            else
            {
                InsertNotification.Text = "INS";
            }
        }

        private void mnuToolsSuggestions_Click(object sender, EventArgs e)
        {
            if (!showSuggestion)
            {
                Suggestions suggestion = new Suggestions(SuggestedWords);
                suggestion.Owner = this;
                showSuggestion = true;

                suggestion.Top = MousePosition.Y;
                suggestion.Left = MousePosition.X;
                suggestion.Show();
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtEditor.Modified)
            {
                DialogResult answer = MessageBox.Show(this, "Do you want to save the file?", this.Text, MessageBoxButtons.YesNoCancel);

                if (answer == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (answer == DialogResult.No)
                {
                   
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
               
            }  
        }       
       
    }
}

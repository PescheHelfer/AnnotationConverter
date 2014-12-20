namespace AnnotationConverter
{
    partial class ConverterGuiMantano
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
            this.openSourceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnSource = new System.Windows.Forms.Button();
            this.lbSourceTitle = new System.Windows.Forms.Label();
            this.lbTargetDbTitle = new System.Windows.Forms.Label();
            this.btnTarget = new System.Windows.Forms.Button();
            this.ckbOutsourcedAnnot = new System.Windows.Forms.CheckBox();
            this.cbBooks = new System.Windows.Forms.ComboBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.nmrOffset = new System.Windows.Forms.NumericUpDown();
            this.lblOffset = new System.Windows.Forms.Label();
            this.lbSource = new System.Windows.Forms.Label();
            this.lbTarget = new System.Windows.Forms.Label();
            this.llbOutsourcedAnnot = new System.Windows.Forms.LinkLabel();
            this.pnlBook = new System.Windows.Forms.Panel();
            this.pnlTarget = new System.Windows.Forms.Panel();
            this.pnlSource = new System.Windows.Forms.Panel();
            this.pnlImportADE = new System.Windows.Forms.Panel();
            this.ckbImportDone = new System.Windows.Forms.CheckBox();
            this.lbFromAndroid = new System.Windows.Forms.Label();
            this.lbFromAndroidTitle = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pnlTargetBook = new System.Windows.Forms.Panel();
            this.lblBookFound = new System.Windows.Forms.Label();
            this.cbTargetBooks = new System.Windows.Forms.ComboBox();
            this.lbTargetBookTitle = new System.Windows.Forms.Label();
            this.pnlFinalSteps = new System.Windows.Forms.Panel();
            this.lbToAndroid = new System.Windows.Forms.Label();
            this.lbToAndroidTitle = new System.Windows.Forms.Label();
            this.pnlFormat = new System.Windows.Forms.Panel();
            this.lbType = new System.Windows.Forms.Label();
            this.lbColor = new System.Windows.Forms.Label();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openTargetFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmrOffset)).BeginInit();
            this.pnlBook.SuspendLayout();
            this.pnlTarget.SuspendLayout();
            this.pnlSource.SuspendLayout();
            this.pnlImportADE.SuspendLayout();
            this.pnlTargetBook.SuspendLayout();
            this.pnlFinalSteps.SuspendLayout();
            this.pnlFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // openSourceFileDialog
            // 
            this.openSourceFileDialog.Filter = "SQLite files (*.db)|*.db|All files (*.*)|*.*";
            this.openSourceFileDialog.Title = "Choose the Source File";
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(10, 29);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(75, 23);
            this.btnSource.TabIndex = 0;
            this.btnSource.Text = "Browse";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // lbSourceTitle
            // 
            this.lbSourceTitle.AutoSize = true;
            this.lbSourceTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSourceTitle.Location = new System.Drawing.Point(14, 8);
            this.lbSourceTitle.Name = "lbSourceTitle";
            this.lbSourceTitle.Size = new System.Drawing.Size(496, 13);
            this.lbSourceTitle.TabIndex = 2;
            this.lbSourceTitle.Text = "2) Choose source: database containing the books (books.db on device or a copy of " +
    "it)";
            // 
            // lbTargetDbTitle
            // 
            this.lbTargetDbTitle.AutoSize = true;
            this.lbTargetDbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTargetDbTitle.Location = new System.Drawing.Point(14, 10);
            this.lbTargetDbTitle.Name = "lbTargetDbTitle";
            this.lbTargetDbTitle.Size = new System.Drawing.Size(448, 13);
            this.lbTargetDbTitle.TabIndex = 5;
            this.lbTargetDbTitle.Text = "4) Choose target database: backup of Mantano-DB (mreader-premium/lite.db) ";
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(10, 29);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(75, 23);
            this.btnTarget.TabIndex = 3;
            this.btnTarget.Text = "Browse";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // ckbOutsourcedAnnot
            // 
            this.ckbOutsourcedAnnot.AutoSize = true;
            this.ckbOutsourcedAnnot.Location = new System.Drawing.Point(14, 63);
            this.ckbOutsourcedAnnot.Name = "ckbOutsourcedAnnot";
            this.ckbOutsourcedAnnot.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbOutsourcedAnnot.Size = new System.Drawing.Size(212, 17);
            this.ckbOutsourcedAnnot.TabIndex = 6;
            this.ckbOutsourcedAnnot.Text = "*Book contains outsourced annotations";
            this.ckbOutsourcedAnnot.UseVisualStyleBackColor = true;
            this.ckbOutsourcedAnnot.CheckedChanged += new System.EventHandler(this.ckbOutsourcedAnnot_CheckedChanged);
            // 
            // cbBooks
            // 
            this.cbBooks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBooks.FormattingEnabled = true;
            this.cbBooks.Location = new System.Drawing.Point(10, 33);
            this.cbBooks.Name = "cbBooks";
            this.cbBooks.Size = new System.Drawing.Size(527, 21);
            this.cbBooks.TabIndex = 7;
            this.cbBooks.SelectedIndexChanged += new System.EventHandler(this.cbBooks_SelectedIndexChanged);
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConvert.Enabled = false;
            this.btnConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.Location = new System.Drawing.Point(20, 575);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 8;
            this.btnConvert.Text = "7) Convert";
            this.btnConvert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // lblBookTitle
            // 
            this.lblBookTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBookTitle.AutoSize = true;
            this.lblBookTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookTitle.Location = new System.Drawing.Point(14, 12);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(340, 13);
            this.lblBookTitle.TabIndex = 2;
            this.lblBookTitle.Text = "3) Choose the book containing the annotations (epub only)";
            // 
            // nmrOffset
            // 
            this.nmrOffset.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmrOffset.Location = new System.Drawing.Point(462, 56);
            this.nmrOffset.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nmrOffset.Name = "nmrOffset";
            this.nmrOffset.Size = new System.Drawing.Size(75, 20);
            this.nmrOffset.TabIndex = 9;
            this.nmrOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrOffset.ThousandsSeparator = true;
            this.nmrOffset.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmrOffset.Visible = false;
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(292, 64);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(164, 13);
            this.lblOffset.TabIndex = 10;
            this.lblOffset.Text = "Offset for outsourced annotations";
            this.lblOffset.Visible = false;
            // 
            // lbSource
            // 
            this.lbSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSource.Location = new System.Drawing.Point(95, 26);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(442, 29);
            this.lbSource.TabIndex = 11;
            this.lbSource.Text = "e.g.:  READER(?:)\\Sony_Reader\\database\\books.db";
            this.lbSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTarget
            // 
            this.lbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarget.Location = new System.Drawing.Point(95, 26);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(442, 29);
            this.lbTarget.TabIndex = 11;
            this.lbTarget.Text = "e.g.:  ..\\Mantano\\mreader-premium.db";
            this.lbTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llbOutsourcedAnnot
            // 
            this.llbOutsourcedAnnot.AutoSize = true;
            this.llbOutsourcedAnnot.Location = new System.Drawing.Point(14, 83);
            this.llbOutsourcedAnnot.Name = "llbOutsourcedAnnot";
            this.llbOutsourcedAnnot.Size = new System.Drawing.Size(390, 13);
            this.llbOutsourcedAnnot.TabIndex = 12;
            this.llbOutsourcedAnnot.TabStop = true;
            this.llbOutsourcedAnnot.Text = "*If you use the trick to get past the limitation of 500 annotations as described " +
    "here";
            this.llbOutsourcedAnnot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbOutsourcedAnnot_LinkClicked);
            // 
            // pnlBook
            // 
            this.pnlBook.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlBook.Controls.Add(this.llbOutsourcedAnnot);
            this.pnlBook.Controls.Add(this.lblOffset);
            this.pnlBook.Controls.Add(this.nmrOffset);
            this.pnlBook.Controls.Add(this.cbBooks);
            this.pnlBook.Controls.Add(this.ckbOutsourcedAnnot);
            this.pnlBook.Controls.Add(this.lblBookTitle);
            this.pnlBook.Enabled = false;
            this.pnlBook.Location = new System.Drawing.Point(11, 218);
            this.pnlBook.Name = "pnlBook";
            this.pnlBook.Size = new System.Drawing.Size(546, 109);
            this.pnlBook.TabIndex = 13;
            // 
            // pnlTarget
            // 
            this.pnlTarget.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlTarget.Controls.Add(this.lbTarget);
            this.pnlTarget.Controls.Add(this.lbTargetDbTitle);
            this.pnlTarget.Controls.Add(this.btnTarget);
            this.pnlTarget.Enabled = false;
            this.pnlTarget.Location = new System.Drawing.Point(11, 335);
            this.pnlTarget.Name = "pnlTarget";
            this.pnlTarget.Size = new System.Drawing.Size(546, 64);
            this.pnlTarget.TabIndex = 14;
            // 
            // pnlSource
            // 
            this.pnlSource.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlSource.Controls.Add(this.lbSource);
            this.pnlSource.Controls.Add(this.lbSourceTitle);
            this.pnlSource.Controls.Add(this.btnSource);
            this.pnlSource.Enabled = false;
            this.pnlSource.Location = new System.Drawing.Point(11, 145);
            this.pnlSource.Name = "pnlSource";
            this.pnlSource.Size = new System.Drawing.Size(546, 65);
            this.pnlSource.TabIndex = 15;
            // 
            // pnlImportADE
            // 
            this.pnlImportADE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlImportADE.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlImportADE.Controls.Add(this.ckbImportDone);
            this.pnlImportADE.Controls.Add(this.lbFromAndroid);
            this.pnlImportADE.Controls.Add(this.lbFromAndroidTitle);
            this.pnlImportADE.Location = new System.Drawing.Point(11, 12);
            this.pnlImportADE.Name = "pnlImportADE";
            this.pnlImportADE.Size = new System.Drawing.Size(546, 125);
            this.pnlImportADE.TabIndex = 15;
            // 
            // ckbImportDone
            // 
            this.ckbImportDone.AutoSize = true;
            this.ckbImportDone.Location = new System.Drawing.Point(14, 105);
            this.ckbImportDone.Name = "ckbImportDone";
            this.ckbImportDone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbImportDone.Size = new System.Drawing.Size(112, 17);
            this.ckbImportDone.TabIndex = 13;
            this.ckbImportDone.Text = "OK, I\'ve done that";
            this.ckbImportDone.UseVisualStyleBackColor = true;
            this.ckbImportDone.CheckedChanged += new System.EventHandler(this.ckbImportDone_CheckedChanged);
            // 
            // lbFromAndroid
            // 
            this.lbFromAndroid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFromAndroid.Location = new System.Drawing.Point(14, 37);
            this.lbFromAndroid.Name = "lbFromAndroid";
            this.lbFromAndroid.Size = new System.Drawing.Size(520, 59);
            this.lbFromAndroid.TabIndex = 12;
            this.lbFromAndroid.Text = "The library is located in a folder similar to:  /storage/emulated/0/Mantano\r\n\r\nTh" +
    "e folder must contain a file called mreader-premium.db or mreader-lite.db.\r\nCopy" +
    " the entire folder and create a backup.";
            this.lbFromAndroid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbFromAndroidTitle
            // 
            this.lbFromAndroidTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFromAndroidTitle.Location = new System.Drawing.Point(14, 8);
            this.lbFromAndroidTitle.Name = "lbFromAndroidTitle";
            this.lbFromAndroidTitle.Size = new System.Drawing.Size(450, 26);
            this.lbFromAndroidTitle.TabIndex = 2;
            this.lbFromAndroidTitle.Text = "1) Make sure the book has already been imported in Mantano and copy the Mantano l" +
    "ibrary from your Android device to your computer";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressBar.Location = new System.Drawing.Point(21, 575);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(527, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 16;
            this.progressBar.Visible = false;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pnlTargetBook
            // 
            this.pnlTargetBook.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlTargetBook.Controls.Add(this.lblBookFound);
            this.pnlTargetBook.Controls.Add(this.cbTargetBooks);
            this.pnlTargetBook.Controls.Add(this.lbTargetBookTitle);
            this.pnlTargetBook.Enabled = false;
            this.pnlTargetBook.Location = new System.Drawing.Point(11, 407);
            this.pnlTargetBook.Name = "pnlTargetBook";
            this.pnlTargetBook.Size = new System.Drawing.Size(546, 89);
            this.pnlTargetBook.TabIndex = 14;
            // 
            // lblBookFound
            // 
            this.lblBookFound.AutoSize = true;
            this.lblBookFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookFound.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblBookFound.Location = new System.Drawing.Point(14, 55);
            this.lblBookFound.Name = "lblBookFound";
            this.lblBookFound.Size = new System.Drawing.Size(92, 13);
            this.lblBookFound.TabIndex = 13;
            this.lblBookFound.Text = "Book identified";
            this.lblBookFound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBookFound.Visible = false;
            // 
            // cbTargetBooks
            // 
            this.cbTargetBooks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetBooks.Enabled = false;
            this.cbTargetBooks.FormattingEnabled = true;
            this.cbTargetBooks.Location = new System.Drawing.Point(10, 29);
            this.cbTargetBooks.Name = "cbTargetBooks";
            this.cbTargetBooks.Size = new System.Drawing.Size(527, 21);
            this.cbTargetBooks.TabIndex = 8;
            this.cbTargetBooks.SelectedIndexChanged += new System.EventHandler(this.cbTargetBooks_SelectedIndexChanged);
            // 
            // lbTargetBookTitle
            // 
            this.lbTargetBookTitle.AutoSize = true;
            this.lbTargetBookTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTargetBookTitle.Location = new System.Drawing.Point(14, 10);
            this.lbTargetBookTitle.Name = "lbTargetBookTitle";
            this.lbTargetBookTitle.Size = new System.Drawing.Size(358, 13);
            this.lbTargetBookTitle.TabIndex = 5;
            this.lbTargetBookTitle.Text = "5) Choose target book: existing document within Mantano-DB ";
            // 
            // pnlFinalSteps
            // 
            this.pnlFinalSteps.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlFinalSteps.Controls.Add(this.lbToAndroid);
            this.pnlFinalSteps.Controls.Add(this.lbToAndroidTitle);
            this.pnlFinalSteps.Enabled = false;
            this.pnlFinalSteps.Location = new System.Drawing.Point(11, 606);
            this.pnlFinalSteps.Name = "pnlFinalSteps";
            this.pnlFinalSteps.Size = new System.Drawing.Size(546, 76);
            this.pnlFinalSteps.TabIndex = 15;
            // 
            // lbToAndroid
            // 
            this.lbToAndroid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbToAndroid.Location = new System.Drawing.Point(14, 24);
            this.lbToAndroid.Name = "lbToAndroid";
            this.lbToAndroid.Size = new System.Drawing.Size(520, 49);
            this.lbToAndroid.TabIndex = 12;
            this.lbToAndroid.Text = "• Ensure you still have the backup\r\n• Make sure Mantano is NOT currently running!" +
    "!!\r\n• Overwrite the existing .db-file";
            this.lbToAndroid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbToAndroidTitle
            // 
            this.lbToAndroidTitle.AutoSize = true;
            this.lbToAndroidTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbToAndroidTitle.Location = new System.Drawing.Point(14, 8);
            this.lbToAndroidTitle.Name = "lbToAndroidTitle";
            this.lbToAndroidTitle.Size = new System.Drawing.Size(396, 13);
            this.lbToAndroidTitle.TabIndex = 2;
            this.lbToAndroidTitle.Text = "8) Copy the modified Mantano database back to your Android device";
            // 
            // pnlFormat
            // 
            this.pnlFormat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlFormat.Controls.Add(this.lbType);
            this.pnlFormat.Controls.Add(this.lbColor);
            this.pnlFormat.Controls.Add(this.cbColor);
            this.pnlFormat.Controls.Add(this.cbType);
            this.pnlFormat.Controls.Add(this.label2);
            this.pnlFormat.Enabled = false;
            this.pnlFormat.Location = new System.Drawing.Point(11, 504);
            this.pnlFormat.Name = "pnlFormat";
            this.pnlFormat.Size = new System.Drawing.Size(546, 63);
            this.pnlFormat.TabIndex = 14;
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(14, 33);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(31, 13);
            this.lbType.TabIndex = 10;
            this.lbType.Text = "Type";
            // 
            // lbColor
            // 
            this.lbColor.AutoSize = true;
            this.lbColor.Location = new System.Drawing.Point(300, 33);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(31, 13);
            this.lbColor.TabIndex = 9;
            this.lbColor.Text = "Color";
            // 
            // cbColor
            // 
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(337, 30);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(200, 21);
            this.cbColor.TabIndex = 8;
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(51, 30);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(216, 21);
            this.cbType.TabIndex = 8;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "6) Choose formatting options";
            // 
            // openTargetFileDialog
            // 
            this.openTargetFileDialog.Filter = "SQLite files (*.db)|*.db|All files (*.*)|*.*";
            this.openTargetFileDialog.Title = "Choose the Target File";
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(98, 575);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(16, 23);
            this.btnHelp.TabIndex = 8;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(473, 575);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ConverterGuiMantano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(569, 694);
            this.Controls.Add(this.pnlFinalSteps);
            this.Controls.Add(this.pnlImportADE);
            this.Controls.Add(this.pnlSource);
            this.Controls.Add(this.pnlFormat);
            this.Controls.Add(this.pnlTargetBook);
            this.Controls.Add(this.pnlTarget);
            this.Controls.Add(this.pnlBook);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.progressBar);
            this.Name = "ConverterGuiMantano";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Annotation Converter       --- Sony PRS-T#  >>  Mantano Reader ---";
            this.Load += new System.EventHandler(this.ConverterGUIMantano_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmrOffset)).EndInit();
            this.pnlBook.ResumeLayout(false);
            this.pnlBook.PerformLayout();
            this.pnlTarget.ResumeLayout(false);
            this.pnlTarget.PerformLayout();
            this.pnlSource.ResumeLayout(false);
            this.pnlSource.PerformLayout();
            this.pnlImportADE.ResumeLayout(false);
            this.pnlImportADE.PerformLayout();
            this.pnlTargetBook.ResumeLayout(false);
            this.pnlTargetBook.PerformLayout();
            this.pnlFinalSteps.ResumeLayout(false);
            this.pnlFinalSteps.PerformLayout();
            this.pnlFormat.ResumeLayout(false);
            this.pnlFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openSourceFileDialog;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label lbSourceTitle;
        private System.Windows.Forms.Label lbTargetDbTitle;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.CheckBox ckbOutsourcedAnnot;
        private System.Windows.Forms.ComboBox cbBooks;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.NumericUpDown nmrOffset;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.LinkLabel llbOutsourcedAnnot;
        private System.Windows.Forms.Panel pnlBook;
        private System.Windows.Forms.Panel pnlTarget;
        private System.Windows.Forms.Panel pnlSource;
        private System.Windows.Forms.Panel pnlImportADE;
        private System.Windows.Forms.CheckBox ckbImportDone;
        private System.Windows.Forms.Label lbFromAndroid;
        private System.Windows.Forms.Label lbFromAndroidTitle;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Panel pnlTargetBook;
        private System.Windows.Forms.Label lbTargetBookTitle;
        private System.Windows.Forms.ComboBox cbTargetBooks;
        private System.Windows.Forms.Panel pnlFinalSteps;
        private System.Windows.Forms.Label lbToAndroid;
        private System.Windows.Forms.Label lbToAndroidTitle;
        private System.Windows.Forms.Label lblBookFound;
        private System.Windows.Forms.Panel pnlFormat;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openTargetFileDialog;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnClose;
    }
}
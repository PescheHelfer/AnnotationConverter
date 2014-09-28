namespace AnnotationConverter
{
    partial class ConverterGUI
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
            this.lbTargetTitle = new System.Windows.Forms.Label();
            this.btnTarget = new System.Windows.Forms.Button();
            this.ckbOutsourcedAnnot = new System.Windows.Forms.CheckBox();
            this.cbBooks = new System.Windows.Forms.ComboBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.saveTargetFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.nmrOffset = new System.Windows.Forms.NumericUpDown();
            this.lblOffset = new System.Windows.Forms.Label();
            this.lbSource = new System.Windows.Forms.Label();
            this.lbTarget = new System.Windows.Forms.Label();
            this.llbOutsourcedAnnot = new System.Windows.Forms.LinkLabel();
            this.pnlBook = new System.Windows.Forms.Panel();
            this.pnlTarget = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFileFound = new System.Windows.Forms.Label();
            this.pnlSource = new System.Windows.Forms.Panel();
            this.pnlImportADE = new System.Windows.Forms.Panel();
            this.ckbImportDone = new System.Windows.Forms.CheckBox();
            this.lbImportADE = new System.Windows.Forms.Label();
            this.lbImportADETitle = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.nmrOffset)).BeginInit();
            this.pnlBook.SuspendLayout();
            this.pnlTarget.SuspendLayout();
            this.pnlSource.SuspendLayout();
            this.pnlImportADE.SuspendLayout();
            this.SuspendLayout();
            // 
            // openSourceFileDialog
            // 
            this.openSourceFileDialog.Filter = "Sony SQLite files (*.db)|*.db|All files (*.*)|*.*";
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
            this.lbSourceTitle.Size = new System.Drawing.Size(498, 13);
            this.lbSourceTitle.TabIndex = 2;
            this.lbSourceTitle.Text = "2) Choose Source: database containing the books (books.db on device or a copy of " +
    "it)";
            // 
            // lbTargetTitle
            // 
            this.lbTargetTitle.AutoSize = true;
            this.lbTargetTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTargetTitle.Location = new System.Drawing.Point(14, 10);
            this.lbTargetTitle.Name = "lbTargetTitle";
            this.lbTargetTitle.Size = new System.Drawing.Size(323, 13);
            this.lbTargetTitle.TabIndex = 5;
            this.lbTargetTitle.Text = "4) Choose Target: Existing ADE annotation file (.annot) ";
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
            this.ckbOutsourcedAnnot.Location = new System.Drawing.Point(14, 58);
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
            this.cbBooks.Location = new System.Drawing.Point(10, 28);
            this.cbBooks.Name = "cbBooks";
            this.cbBooks.Size = new System.Drawing.Size(527, 21);
            this.cbBooks.TabIndex = 7;
            this.cbBooks.SelectedIndexChanged += new System.EventHandler(this.cbBooks_SelectedIndexChanged);
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(237, 386);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 8;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // saveTargetFileDialog
            // 
            this.saveTargetFileDialog.DefaultExt = "xml";
            this.saveTargetFileDialog.Filter = "Annotation files (*.annot)|*.annot|All files (*.*)|*.*";
            this.saveTargetFileDialog.OverwritePrompt = false;
            this.saveTargetFileDialog.Title = "Choose the Target File";
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
            this.lblOffset.Location = new System.Drawing.Point(292, 59);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(164, 13);
            this.lblOffset.TabIndex = 10;
            this.lblOffset.Text = "Offset for outsourced annotations";
            this.lblOffset.Visible = false;
            // 
            // lbSource
            // 
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
            this.lbTarget.Text = "e.g.:  ..\\My Documents\\My Digital Editions\\Annotations(\\.external)";
            this.lbTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llbOutsourcedAnnot
            // 
            this.llbOutsourcedAnnot.AutoSize = true;
            this.llbOutsourcedAnnot.Location = new System.Drawing.Point(14, 78);
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
            this.pnlBook.Controls.Add(this.llbOutsourcedAnnot);
            this.pnlBook.Controls.Add(this.lblOffset);
            this.pnlBook.Controls.Add(this.nmrOffset);
            this.pnlBook.Controls.Add(this.cbBooks);
            this.pnlBook.Controls.Add(this.ckbOutsourcedAnnot);
            this.pnlBook.Controls.Add(this.lblBookTitle);
            this.pnlBook.Enabled = false;
            this.pnlBook.Location = new System.Drawing.Point(2, 190);
            this.pnlBook.Name = "pnlBook";
            this.pnlBook.Size = new System.Drawing.Size(546, 109);
            this.pnlBook.TabIndex = 13;
            // 
            // pnlTarget
            // 
            this.pnlTarget.Controls.Add(this.lbTarget);
            this.pnlTarget.Controls.Add(this.label1);
            this.pnlTarget.Controls.Add(this.lbTargetTitle);
            this.pnlTarget.Controls.Add(this.btnTarget);
            this.pnlTarget.Controls.Add(this.lblFileFound);
            this.pnlTarget.Enabled = false;
            this.pnlTarget.Location = new System.Drawing.Point(2, 312);
            this.pnlTarget.Name = "pnlTarget";
            this.pnlTarget.Size = new System.Drawing.Size(546, 64);
            this.pnlTarget.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(337, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "--> will be overwritten!";
            // 
            // lblFileFound
            // 
            this.lblFileFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileFound.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblFileFound.Location = new System.Drawing.Point(4, 29);
            this.lblFileFound.Name = "lblFileFound";
            this.lblFileFound.Size = new System.Drawing.Size(90, 23);
            this.lblFileFound.TabIndex = 12;
            this.lblFileFound.Text = "File identified:";
            this.lblFileFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFileFound.Visible = false;
            // 
            // pnlSource
            // 
            this.pnlSource.Controls.Add(this.lbSource);
            this.pnlSource.Controls.Add(this.lbSourceTitle);
            this.pnlSource.Controls.Add(this.btnSource);
            this.pnlSource.Enabled = false;
            this.pnlSource.Location = new System.Drawing.Point(2, 112);
            this.pnlSource.Name = "pnlSource";
            this.pnlSource.Size = new System.Drawing.Size(546, 65);
            this.pnlSource.TabIndex = 15;
            // 
            // pnlImportADE
            // 
            this.pnlImportADE.Controls.Add(this.ckbImportDone);
            this.pnlImportADE.Controls.Add(this.lbImportADE);
            this.pnlImportADE.Controls.Add(this.lbImportADETitle);
            this.pnlImportADE.Location = new System.Drawing.Point(2, 12);
            this.pnlImportADE.Name = "pnlImportADE";
            this.pnlImportADE.Size = new System.Drawing.Size(546, 79);
            this.pnlImportADE.TabIndex = 15;
            // 
            // ckbImportDone
            // 
            this.ckbImportDone.AutoSize = true;
            this.ckbImportDone.Location = new System.Drawing.Point(14, 56);
            this.ckbImportDone.Name = "ckbImportDone";
            this.ckbImportDone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbImportDone.Size = new System.Drawing.Size(112, 17);
            this.ckbImportDone.TabIndex = 13;
            this.ckbImportDone.Text = "OK, I\'ve done that";
            this.ckbImportDone.UseVisualStyleBackColor = true;
            this.ckbImportDone.CheckedChanged += new System.EventHandler(this.ckbImportDone_CheckedChanged);
            // 
            // lbImportADE
            // 
            this.lbImportADE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbImportADE.Location = new System.Drawing.Point(14, 27);
            this.lbImportADE.Name = "lbImportADE";
            this.lbImportADE.Size = new System.Drawing.Size(520, 29);
            this.lbImportADE.TabIndex = 12;
            this.lbImportADE.Text = "During the import, ADE creates an .annot file in ..\\My Documents\\My Digital Editi" +
    "ons\\Annotations\\.external";
            this.lbImportADE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbImportADETitle
            // 
            this.lbImportADETitle.AutoSize = true;
            this.lbImportADETitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbImportADETitle.Location = new System.Drawing.Point(14, 8);
            this.lbImportADETitle.Name = "lbImportADETitle";
            this.lbImportADETitle.Size = new System.Drawing.Size(359, 13);
            this.lbImportADETitle.TabIndex = 2;
            this.lbImportADETitle.Text = "1) Import the book of choice into Adobe Digital Editions (ADE)";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 386);
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
            // ConverterGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 418);
            this.Controls.Add(this.pnlImportADE);
            this.Controls.Add(this.pnlSource);
            this.Controls.Add(this.pnlTarget);
            this.Controls.Add(this.pnlBook);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.progressBar);
            this.Name = "ConverterGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Annotation Converter       --- Sony PRS-T#  >>  Adobe Digital Editions ---";
            this.Load += new System.EventHandler(this.ConverterGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmrOffset)).EndInit();
            this.pnlBook.ResumeLayout(false);
            this.pnlBook.PerformLayout();
            this.pnlTarget.ResumeLayout(false);
            this.pnlTarget.PerformLayout();
            this.pnlSource.ResumeLayout(false);
            this.pnlSource.PerformLayout();
            this.pnlImportADE.ResumeLayout(false);
            this.pnlImportADE.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openSourceFileDialog;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label lbSourceTitle;
        private System.Windows.Forms.Label lbTargetTitle;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.CheckBox ckbOutsourcedAnnot;
        private System.Windows.Forms.ComboBox cbBooks;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.SaveFileDialog saveTargetFileDialog;
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
        private System.Windows.Forms.Label lbImportADE;
        private System.Windows.Forms.Label lbImportADETitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFileFound;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}
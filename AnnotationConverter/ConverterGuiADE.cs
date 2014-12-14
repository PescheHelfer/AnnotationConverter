using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AnnotationConverter
{
    public partial class ConverterGuiADE : Form
    {
        private string _targetFile;
        private string _lbTargetDefaultText;

        private AbsImport _import;
        private AbsExport _export;

        private List<Tuple<long, string, string, string>> _liBooksContent = new List<Tuple<long, string, string, string>>();
        private Helpers.Utils.EReader _importEReader = Helpers.Utils.EReader.SonyPRST;
        private Helpers.Utils.EReader _exportEReader = Helpers.Utils.EReader.AdobeDigitalEditions;

        public ConverterGuiADE()
        {
            InitializeComponent();
            // set initial Directory for OpenFileDialog --> inactive, the last selected folder will be chosen
            // openSourceFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // saveTargetFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        #region Handler Methods

        private void ConverterGUI_Load(object sender, EventArgs e)
        {
            // Add a link to the LinkLabel.
            var linkMobileread = new LinkLabel.Link();
            linkMobileread.LinkData = "http://www.mobileread.com/forums/showthread.php?t=177426";
            llbOutsourcedAnnot.Links.Add(linkMobileread);
            InstantiateSourceAndTarget();
            _lbTargetDefaultText = lbTarget.Text;
        }

        private void ckbImportDone_CheckedChanged(object sender, EventArgs e)
        {
            pnlSource.Enabled = ckbImportDone.Checked;
            pnlSource.BackColor = ckbImportDone.Checked ? System.Drawing.SystemColors.ControlLight : System.Drawing.SystemColors.Control;
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            openSourceFileDialog.InitialDirectory = Properties.Settings.Default.SonyPRSDir;
            var result = openSourceFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _import.DataSource = openSourceFileDialog.FileName;
                lbSource.Text = _import.DataSource;
                this.lbSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                _liBooksContent.Clear();

                try
                {
                    ((ImportFromSonyPRST) _import).VerifySonyPRSTDb();
                    PopulateCbBooks();
                    Properties.Settings.Default.SonyPRSDir = new FileInfo(_import.DataSource).Directory.FullName;
                    Properties.Settings.Default.Save();
                    pnlBook.Enabled = true;
                    pnlBook.BackColor = System.Drawing.SystemColors.ControlLight;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The selected file cannot be processed.\r\nThe following error has occured:\r\n\r\n" + ex.Message,
                        "Error: invalid file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void llbOutsourcedAnnot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void ckbOutsourcedAnnot_CheckedChanged(object sender, EventArgs e)
        {
            lblOffset.Visible = ckbOutsourcedAnnot.Checked;
            nmrOffset.Visible = ckbOutsourcedAnnot.Checked;
            ((ImportFromSonyPRST) _import).HasOutsourcedAnnotations = ckbOutsourcedAnnot.Checked;
        }

        private void cbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Tuple<long, string, string, string>) cbBooks.SelectedItem).Item1 < 0)
            {
                return;
            }

            pnlTarget.Enabled = true;
            pnlTarget.BackColor = System.Drawing.SystemColors.ControlLight;
            _import.ContentID = (long) cbBooks.SelectedValue;
            _import.FileName = ((Tuple<long, string, string, string>) cbBooks.SelectedItem).Item4;

            string path;
            string fileName;
            var fileExists = DetermineTargetPath(out path, out fileName);

            if (fileExists)
            {
                _targetFile = path + fileName;
                btnTarget.Enabled = false;
                btnTarget.Visible = false;
                lblFileFound.Visible = true;
                lbTarget.Text = _targetFile;
                lbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                btnConvert.Enabled = true;
            }
            else
            {
                _targetFile = "";
                btnTarget.Enabled = true;
                btnTarget.Visible = true;
                lblFileFound.Visible = false;
                lbTarget.Text = _lbTargetDefaultText;
                lbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic,
                    System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                btnConvert.Enabled = false;
            }
        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            //try to find the target file automatically
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fileName;
            var fileExists = DetermineTargetPath(out path, out fileName);

            // Target file not found
            if (DialogResult.Cancel == MessageBox.Show("The annotation file was not found automatically."
                                                       + "\r\n\r\nPlease make sure the book has been previously"
                                                       + "\r\nimportet into Adobe Digital Editions and"
                                                       + "\r\nsearch for the .annot file manually."
                                                       + "\r\nPlease check the .external-folder as well."
                                                       + "\r\n\r\nThe file name used by ADE may differ from the reader.",
                "Warning: File not found",
                MessageBoxButtons.OKCancel))
            {
                return;
            }

            // User presses ok on FileSaveDialog
            saveTargetFileDialog.InitialDirectory = path;
            saveTargetFileDialog.FileName = fileName;
            if (DialogResult.OK == saveTargetFileDialog.ShowDialog())
            {
                _targetFile = saveTargetFileDialog.FileName;
                lbTarget.Text = _targetFile;
                lbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                btnConvert.Enabled = true;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Existing annotations will be overwritten"
                                                   + "\r\nby the annotations from the reader."
                                                   + "\r\n\r\nAnnotations in the book that were"
                                                   + "\r\nmade in Adobe Digital Editions and"
                                                   + "\r\ndon't exist on the reader will be lost!"
                                                   + "\r\n\r\nContinue?",
                "Warning: File will be overwritten",
                MessageBoxButtons.YesNo))
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            btnConvert.Visible = false;
            progressBar.Visible = true;
            // this.Enabled = false;
            _import.GetTotalCountOfAnnotations();

            // Kickoff the worker thread to begin it's DoWork function.
            bgWorker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // see: http://stackoverflow.com/questions/11936837/c-sharp-backgroundworker-dowork-method-calling-another-class-and-progressreport
            var bgw = (BackgroundWorker) sender;
            var eh = new ProgressChangedEventHandler((obj, pcea) => bgw.ReportProgress(pcea.ProgressPercentage));
            _import.ProgressChanged += eh; //start listening to progress events sent by the import class

            _export.PrepareTarget(_targetFile);
            try
            {
                _import.ReadAnnotations();
                _import.ReadBookmarks();
            }
            finally
            {
                _export.CloseTarget();
                _import.ProgressChanged -= eh; //necessary to stop listening
            }

            //Report 100% completion on operation completed
            bgw.ReportProgress(100);
            e.Result = 100;
        }

        private void bgWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            progressBar.Visible = false;
            btnConvert.Visible = true;
            this.Enabled = true;
            ckbOutsourcedAnnot.Checked = false;
            Cursor.Current = Cursors.Default;
            MessageBox.Show(String.Format("{0} bookmarks and {1} annotations have been converted."
                , _import.CountBookmarks.ToString()
                , _import.CountAnnotations.ToString())
                , "Conversion completed");
            btnConvert.Enabled = false;
            pnlTarget.Enabled = false;
            cbBooks.SelectedIndex = 0;
            _import.CountAnnotations = _import.CountBookmarks = _import.TotalCount = 0;
        }

        #endregion

        #region Various Methods

        private void InstantiateSourceAndTarget()
        {
            switch (_exportEReader)
            {
                    //case AnnotationConverter.Utils.Utils.EReader.SonyPRST:
                    //    break;
                case AnnotationConverter.Helpers.Utils.EReader.AdobeDigitalEditions:
                    _export = new ExportToADE();
                    break;
                case AnnotationConverter.Helpers.Utils.EReader.Mantano:
                    _export = new ExportToMantano();
                    break;
                default:
                    _export = null;
                    // ToDo: RaiseError: indicated EReader is not supported
                    break;
            }

            switch (_importEReader)
            {
                case AnnotationConverter.Helpers.Utils.EReader.SonyPRST:
                    _import = new ImportFromSonyPRST(_export);
                    break;
                    //case AnnotationConverter.Utils.Utils.EReader.AdobeDigitalEditions:
                    //    break;
                default:
                    _import = null;
                    // ToDo: RaiseError: indicated EReader is not supported
                    break;
            }
        }

        private void PopulateCbBooks()
        {
            _import.BuildBookList(_liBooksContent);

            cbBooks.DisplayMember = "Item3";
            cbBooks.ValueMember = "Item1";
            cbBooks.DataSource = new BindingSource(_liBooksContent, null);
            cbBooks.SelectedIndex = 0;
        }

        /// <summary>
        /// Tries to find the target file automatically
        /// </summary>
        /// <param name="path">Path of the folder supposedly containing the ADE .annot file</param>
        /// <param name="fileName">Supposed name of the .annot file to be overwritten with the annotations from the reader</param>
        /// <returns>true: target file found</returns>
        private bool DetermineTargetPath(out string path, out string fileName)
        {
            //try to find the target file automatically
            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var pathAnnotations = myDocs + @"\My Digital Editions\Annotations\";
            var pathAnnotationsExternal = pathAnnotations + @".external\";
            var fileNameAnnotations = _import.FileName + ".annot";
            var fileNameAnnotationsExternal = _import.FileName.Replace(".epub", "") + ".annot";

            if (File.Exists(pathAnnotationsExternal + fileNameAnnotationsExternal))
            {
                path = pathAnnotationsExternal;
                fileName = fileNameAnnotationsExternal;
                return true;
            }

            if (File.Exists(pathAnnotationsExternal + fileNameAnnotations))
            {
                path = pathAnnotationsExternal;
                fileName = fileNameAnnotations;
                return true;
            }

            if (File.Exists(pathAnnotations + fileNameAnnotations))
            {
                path = pathAnnotations;
                fileName = fileNameAnnotations;
                return true;
            }

            if (File.Exists(pathAnnotations + fileNameAnnotationsExternal))
            {
                path = pathAnnotations;
                fileName = fileNameAnnotationsExternal;
                return true;
            }

            // Directory exists but file not found
            if (Directory.Exists(pathAnnotations))
            {
                path = pathAnnotations;
                fileName = fileNameAnnotations;
                return false;
            }

            // not even the directory has been found
            path = myDocs;
            fileName = fileNameAnnotationsExternal;
            return false;
        }

        #endregion
    }
}
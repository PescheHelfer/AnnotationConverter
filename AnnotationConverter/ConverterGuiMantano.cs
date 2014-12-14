using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AnnotationConverter.Properties;

namespace AnnotationConverter
{
    public partial class ConverterGuiMantano : Form
    {
        private string _targetFile;
        private string _lbTargetDefaultText;
        private long _targetDocumentId;

        private AbsImport _import;
        private AbsExport _export;

        private List<Tuple<long, string, string, string>> _liBooksContent = new List<Tuple<long, string, string, string>>();
        private List<Tuple<long, string, string>> _liTargetBooksContent = new List<Tuple<long, string, string>>();
        private Dictionary<int, string> _diAnnotationTypes = new Dictionary<int, string>();
        private Dictionary<int, string> _diHighlightColors;
        private Dictionary<int, string> _diUnderlineColors;
        private Helpers.Utils.EReader _importEReader = Helpers.Utils.EReader.SonyPRST;
        private Helpers.Utils.EReader _exportEReader = Helpers.Utils.EReader.Mantano;

        public ConverterGuiMantano()
        {
            InstantiateSourceAndTarget();
            InitializeComponent();
        }

        #region Handler Methods

        private void ConverterGUIMantano_Load(object sender, EventArgs e)
        {
            // Instantiate and populate dictionaries
            _diAnnotationTypes = new Dictionary<int, string>
            {
                {0, "highlight"},
                {1, "underline"},
                {2, "strikethrough"},
                {3, "vertical bar in margin"}
            };

            _diHighlightColors = new Dictionary<int, string>
            {
                {-131216, "yellow"},
                {-5177488, "light green"},
                {-9379841, "cyan"},
                {-26923, "magenta"},
                {-84410, "orange"},
            };

            _diUnderlineColors = new Dictionary<int, string>
            {
                {-16761601, "blue"},
                {-2226104, "red"},
                {-16468926, "green"},
                {-16777216, "black"}
            };

            // Bind ComboBoxes
            cbType.DataSource = new BindingSource(_diAnnotationTypes, null);
            cbType.DisplayMember = "Value";
            cbType.ValueMember = "Key";

            cbColor.DataSource = new BindingSource(_diHighlightColors, null);
            cbColor.DisplayMember = "Value";
            cbColor.ValueMember = "Key";

            // Add a link to the LinkLabel.
            var linkMobileread = new LinkLabel.Link {LinkData = "http://www.mobileread.com/forums/showthread.php?t=177426"};
            llbOutsourcedAnnot.Links.Add(linkMobileread);

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
            if (result != DialogResult.OK)
            {
                return;
            }

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
                pnlBook.BackColor = System.Drawing.SystemColors.ControlLight;
                pnlBook.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The selected file cannot be processed.\r\nThe following error has occured:\r\n\r\n" + ex.Message +
                    "\r\n\r\nPlease make sure you have chosen books.db.",
                    "Error: invalid file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                pnlTarget.BackColor = System.Drawing.SystemColors.Control;
                pnlTarget.Enabled = false;
                return;
            }

            if (!pnlTarget.Enabled)
            {
                pnlTarget.BackColor = System.Drawing.SystemColors.ControlLight;
                pnlTarget.Enabled = true;
            }

            if (cbTargetBooks.SelectedValue != null)
            {
                EvaluateBookLists();
            }

            _import.ContentID = (long) cbBooks.SelectedValue;
        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            //  ((ExportToMantano) _export).DbFile = _targetFile;

            Properties.Settings.Default.Reload();
            openTargetFileDialog.InitialDirectory = Properties.Settings.Default.MantanoDir;
            var result = openTargetFileDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            _targetFile = openTargetFileDialog.FileName;
            ((ExportToMantano) _export).DbFile = _targetFile;

            lbTarget.Text = _targetFile;
            this.lbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            _liTargetBooksContent.Clear();

            try
            {
                ((ExportToMantano) _export).VerifyMantanoDb();
                PopulateCbTargetBooks();
                Properties.Settings.Default.MantanoDir = new FileInfo(_targetFile).Directory.FullName;
                Properties.Settings.Default.Save();
                EvaluateBookLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The selected file cannot be processed.\r\nThe following error has occured:\r\n\r\n" + ex.Message +
                    "\r\n\r\nPlease make sure you have chosen mreader-premium/light.db.",
                    "Error: invalid file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cbTargetBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTargetBooks.SelectedValue != null && (long) cbTargetBooks.SelectedValue > -1)
            {
                pnlFormat.Enabled = true;
                pnlFormat.BackColor = System.Drawing.SystemColors.ControlLight;
                btnConvert.Enabled = true;
                _targetDocumentId = (long) cbTargetBooks.SelectedValue;
            }
            else
            {
                pnlFormat.Enabled = false;
                pnlFormat.BackColor = System.Drawing.SystemColors.Control;
                btnConvert.Enabled = false;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = (ComboBox) sender;
            int highlightStyle;

            if (_import == null)
            {
                return;
            }

            if ((cb.SelectedValue) is KeyValuePair<int, string>)
            {
                highlightStyle = ((KeyValuePair<int, string>) cb.SelectedValue).Key;
            }
            else if ((cb.SelectedValue) is int)
            {
                highlightStyle = (int) cb.SelectedValue;
            }
            else
            {
                return;
            }

            cbColor.DataSource = highlightStyle == 0 ? new BindingSource(_diHighlightColors, null) : new BindingSource(_diUnderlineColors, null);
            _import.HighlightStyle = highlightStyle;
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = (ComboBox) sender;
            int color;

            if (_import == null)
            {
                return;
            }

            if ((cb.SelectedValue) is KeyValuePair<int, string>)
            {
                color = ((KeyValuePair<int, string>) cb.SelectedValue).Key;
            }
            else if ((cb.SelectedValue) is int)
            {
                color = (int) cb.SelectedValue;
            }
            else
            {
                return;
            }

            _import.Color = color;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("New and changed annotations will be added"
                                                   + "\r\nto the indicated database."
                                                   + "\r\n\r\n(see [?] for more details)"
                                                   + "\r\n\r\nContinue?",
                "Annotations will be added",
                MessageBoxButtons.YesNo))
            {
                return;
            }
            _export.RecordsSkipped = _export.RecordsInserted = _export.RecordsUpdated = 0;
            Cursor.Current = Cursors.WaitCursor;
            btnConvert.Visible = false;
            btnHelp.Visible = btnCancel.Visible = progressBar.Visible = false;
            // this.Enabled = false;
            _import.GetTotalCountOfAnnotations();

            // Kickoff the worker thread to begin it's DoWork function.
            bgWorker.RunWorkerAsync();
            pnlFinalSteps.Enabled = true;
            pnlFinalSteps.BackColor = System.Drawing.SystemColors.ControlLight;
            btnHelp.Visible = false;
            btnCancel.Visible = progressBar.Visible = true;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("• Annotation exists in the Source but not in Mantano"
                            + "\r\n\t-> Copy from Source to Mantano"
                            + "\r\n\r\n• Annotation does not exist in the Source but in Mantano"
                            + "\r\n\t-> Nothing (no deletions)"
                            + "\r\n\r\n• Annotation starts or ends at the same position on both"
                            + "\r\n  but has differnt length and is"
                            + "\r\n  - newer on the source"
                            + "\r\n\t-> update from Source to Mantano"
                            + "\r\n  - newer on the Mantano"
                            + "\r\n\t-> Nothing"
                            + "\r\n\r\n• Annotation exists in both, but content is newer and not empty in Source"
                            + "\r\n\t-> update from Source to Mantano"
                            + "\r\n\r\n• Annotation overlaps, but does not start nor end at the same position"
                            + "\r\n\t-> Copy from Source to Mantano"
                            + "\r\n\t(can't recognize the overlap, leading to partial duplication)",
                "Scenarios",
                MessageBoxButtons.OK);
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

            _export.PrepareTarget(_targetDocumentId);
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
            MessageBox.Show(String.Format("{0} bookmarks and {1} annotations have been converted:"
                                          + "\r\n{2} records have been skipped (already existing),"
                                          + "\r\n{3} records have been inserted"
                                          + "\r\nand {4} records have been updated."
                                          + "\r\n{5} errors have occured."
                , _import.CountBookmarks.ToString()
                , _import.CountAnnotations.ToString()
                , _export.RecordsSkipped.ToString()
                , _export.RecordsInserted.ToString()
                , _export.RecordsUpdated.ToString()
                , _export.RecordErrors.ToString())
                , "Conversion completed");
            btnConvert.Enabled = false;
            pnlFormat.Enabled = false;
            btnHelp.Visible = true;
            cbBooks.SelectedIndex = 0;
            cbTargetBooks.SelectedIndex = 0;
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

        private void PopulateCbTargetBooks()
        {
            ((ExportToMantano) _export).BuildTargetBookList(_liTargetBooksContent);

            cbTargetBooks.DisplayMember = "Item3";
            cbTargetBooks.ValueMember = "Item1";
            cbTargetBooks.DataSource = new BindingSource(_liTargetBooksContent, null);
            cbTargetBooks.SelectedIndex = 0;
        }

        /// <summary>
        /// Tries to find the book automatically
        /// </summary>
        /// <returns></returns>
        private long MatchBookLists()
        {
            if (cbTargetBooks.Items.Count == 0 || cbBooks.Items.Count == 0)
            {
                return -1;
            }

            var matchingItem = _liTargetBooksContent.Find(item => item.Item2 == ((Tuple<long, string, string, string>) cbBooks.SelectedItem).Item2);
            if (matchingItem == null)
            {
                return -1;
            }

            return matchingItem.Item1;
        }

        private void EvaluateBookLists()
        {
            cbTargetBooks.SelectedValue = MatchBookLists();
            if ((long) cbTargetBooks.SelectedValue > -1)
            {
                btnConvert.Enabled = true;
                lblBookFound.Text = "Book identified";
                this.lblBookFound.ForeColor = System.Drawing.Color.DarkGreen;
                pnlFormat.Enabled = true;
                pnlFormat.BackColor = System.Drawing.SystemColors.ControlLight;
            }
            else
            {
                btnConvert.Enabled = false;
                lblBookFound.Text =
                    "Book not found automatically, please select manually.\nIf it does not exist in Mantano yet, import it first and start at step 1).";
                this.lblBookFound.ForeColor = System.Drawing.Color.DarkRed;
                cbTargetBooks.Enabled = true;
            }

            lblBookFound.Visible = true;
            pnlTargetBook.Enabled = true;
            pnlTargetBook.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        #endregion
    }
}
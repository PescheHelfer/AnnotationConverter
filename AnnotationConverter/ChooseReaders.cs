using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnotationConverter.Helpers;

namespace AnnotationConverter
{
    public partial class FrmChooseReaders : Form
    {
        private Dictionary<Utils.EReader, string> _diImportReaders = new Dictionary<Utils.EReader, string>();
        private Dictionary<Utils.EReader, string> _diExportReaders = new Dictionary<Utils.EReader, string>();

        public FrmChooseReaders()
        {
            _diImportReaders.Add(Utils.EReader.SonyPRST, "Sony PRS-T1/-T2/-T3");
            _diExportReaders.Add(Utils.EReader.Mantano, "Mantano Ebook Reader Free/Pro");
            _diExportReaders.Add(Utils.EReader.AdobeDigitalEditions, "Adobe Digital Editions 2.x/3.x");
            InitializeComponent();
        }

        #region Handler Methods
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var rdb = sender as RadioButton;
            if (rdb != null && rdb.Checked)
            {
                rdb.Parent.Tag = (Utils.EReader)rdb.Tag;
            }
        }

        private void AddRadioButtons(Dictionary<Utils.EReader, string> readers, GroupBox targetBox)
        {
            int vPos = 20;
            int i = 0;
            foreach (KeyValuePair<Utils.EReader, string> pair in readers)
            {
                var rdb = new RadioButton
                {
                    Name = "rdb" + pair.Value,
                    Text = pair.Value,
                    Tag = pair.Key,
                    Location = new Point(20, vPos),
                    AutoSize = true
                };
                rdb.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
                targetBox.Controls.Add(rdb);
                rdb.Checked = (i == 0);
                vPos += 20;
                i++;
            }
        }

        private void OpenFormCloseThis(Form form)
        {
            this.Hide();
            form.Closed += (sender, args) => this.Close();
            form.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((Utils.EReader)gbImport.Tag == Utils.EReader.SonyPRST)
            {
                switch ((Utils.EReader)gbExport.Tag)
                {
                    case Utils.EReader.AdobeDigitalEditions:
                        OpenFormCloseThis(new ConverterGuiADE());
                        break;
                    case Utils.EReader.Mantano:
                        OpenFormCloseThis(new ConverterGuiMantano());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region Various Methods
        private void ChooseReaders_Load(object sender, EventArgs e)
        {
            int radioHeight = 20 * Math.Max(_diImportReaders.Count(), _diExportReaders.Count());
            gbImport.Height = gbExport.Height = radioHeight + 30;
            this.Height = radioHeight + 140;
            AddRadioButtons(_diImportReaders, gbImport);
            AddRadioButtons(_diExportReaders, gbExport);
        } 
        #endregion
    }
}
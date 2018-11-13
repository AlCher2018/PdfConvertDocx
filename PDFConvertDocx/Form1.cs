using PDFConvertDocxRu.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConvertDocxRu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            labelWait.Visible = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                CheckFileExists = true, CheckPathExists = true,
                Filter = "Файлы PDF|*.pdf", FilterIndex = 0,
                Multiselect = false,
                Title = "Выберите файл для преобразования..."
            };
            if (string.IsNullOrEmpty(AppConfig.InFolderDefault) == false)
            {
                openFile.InitialDirectory = AppConfig.InFolderDefault;
            }

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.labelWait.Visible = true;
                this.Refresh();

                PdfDocConverterSautin converter = new PdfDocConverterSautin()
                {
                    IsOpenAfterConverting = AppConfig.IsOpenOutfile
                };
                bool resConv = converter.Convert(openFile.FileName);

                this.labelWait.Visible = false;
                this.Refresh();

                if (resConv == false)
                {
                    MessageBox.Show("ERROR: " + converter.ErrorMessage, "Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Load time: {converter.LoadTime}, Save time: {converter.SaveTime}", "Convertion was successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}

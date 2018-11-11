using PDFConvertDocxRu.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDFConvertDocxRu
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            AppConfig.ReadValues();
            textBoxInFilesFolder.Text = AppConfig.InFolderDefault;
            //textBoxOutFilesFolder.Text = AppConfig.OutFolderDefault;
            checkBoxIsOpenOutFile.Checked = AppConfig.IsOpenOutfile;

            base.OnLoad(e);
        }

        private void cmdSelectFolderInFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog()
            {
                SelectedPath = this.textBoxInFilesFolder.Text,
                ShowNewFolderButton = true, Description = "Выберите папку с PDF-файлами"
            };

            DialogResult result= folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxInFilesFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            AppConfig.InFolderDefault = textBoxInFilesFolder.Text;
            AppConfig.IsOpenOutfile = checkBoxIsOpenOutFile.Checked;

            AppConfig.SaveValues();
        }

        private void cmdAssocToPdf_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Under construction...");
            return;

            //            string ico = Application.StartupPath + "\\icon.ico";
//            string exe = Application.ExecutablePath;

//            if (!FileAssociation.IsAssociated(".pdf"))
//            {
//                // TODO
////                FileAssociation.Associate(".pdf", "proga", ".pdf", ico, exe);
//            }
        }

        private void cmdSelectFolderOutFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog()
            {
                SelectedPath = this.textBoxInFilesFolder.Text,
                ShowNewFolderButton = true,
                Description = "Выберите папку с PDF-файлами"
            };

            DialogResult result = folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxInFilesFolder.Text = folderBrowser.SelectedPath;
            }
        }
    }
}

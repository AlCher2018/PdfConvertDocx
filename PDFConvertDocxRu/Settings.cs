using PDFConvertDocxRu.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            textBoxOutFilesFolder.Text = AppConfig.OutFolderDefault;
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
            string dir = textBoxInFilesFolder.Text;
            if (checkDir(dir)) AppConfig.InFolderDefault = dir;

            dir = textBoxOutFilesFolder.Text;
            if (checkDir(dir)) AppConfig.OutFolderDefault = dir;

            AppConfig.IsOpenOutfile = checkBoxIsOpenOutFile.Checked;
            AppConfig.SaveValues();
        }

        private bool checkDir(string dir)
        {
            bool retVal = false;
            if (Directory.Exists(dir) == false)
            {
                DialogResult res = MessageBox.Show($"Папка '{dir}' не существует.\n\nСоздать папку?", "Проверка папки...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        DirectoryInfo dirInfo = Directory.CreateDirectory(dir);
                        retVal = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Ошибка создания папки:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            return retVal;
        }

        private void cmdAssocToPdf_Click(object sender, EventArgs e)
        {
            string ico = Application.StartupPath + "\\icon.ico";
            string exe = Application.ExecutablePath;

            FileAssociation.Associate(".pdf", AppDomain.CurrentDomain.FriendlyName, ".pdf", ico, exe);
            //if (!FileAssociation.IsAssociated(".pdf"))
            //{
            //    FileAssociation.Associate(".pdf", AppDomain.CurrentDomain.FriendlyName, ".pdf", ico, exe);
            //}
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

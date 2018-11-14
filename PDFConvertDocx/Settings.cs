using PDFConvertDocxRu.Services;
using System;
using System.IO;
using System.Windows.Forms;

namespace PDFConvertDocxRu
{
    public partial class Settings : Form
    {
        private const string FileExt = "pdf";
        private const string ProgID = "pdffile";
        private const string ProgMenuItem = "Конвертировать в DOCX и перевести";

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

        private void CmdSelectFolderInFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog()
            {
                SelectedPath = this.textBoxInFilesFolder.Text,
                ShowNewFolderButton = true, Description = "Выберите папку с исходными PDF-файлами"
            };

            DialogResult result= folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxInFilesFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void CmdOk_Click(object sender, EventArgs e)
        {
            string dir = textBoxInFilesFolder.Text;
            if (CheckDir(dir)) AppConfig.InFolderDefault = dir;

            dir = textBoxOutFilesFolder.Text;
            if (CheckDir(dir)) AppConfig.OutFolderDefault = dir;

            AppConfig.IsOpenOutfile = checkBoxIsOpenOutFile.Checked;

            AppConfig.SaveValues();
        }

        private bool CheckDir(string dir)
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

        private void CmdAssocToPdf_Click(object sender, EventArgs e)
        {
            string exeFilePath = Application.ExecutablePath;

            if (FileAssociation.IsAssociated(FileExt, ProgMenuItem, exeFilePath))
            {
                MessageBox.Show($"Приложение '{exeFilePath}' уже связано с расширением '{FileExt}'", "Проверка связи расширения с приложением", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                bool result = FileAssociation.Associate(FileExt, ProgID, ProgMenuItem, exeFilePath);
                if (result)
                {
                    MessageBox.Show($"Приложение '{exeFilePath}' успешно ассоциаровано с расширением '{FileExt}'", "Установка связи расширения с приложением", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Приложение '{exeFilePath}' НЕ ассоциировано с расширением '{FileExt}'\n\nError: " + FileAssociation.ErrorMessage, "Установка связи расширения с приложением", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CmdSelectFolderOutFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog()
            {
                SelectedPath = this.textBoxOutFilesFolder.Text,
                ShowNewFolderButton = true,
                Description = "Выберите папку для сохранения переведенных файлов (DOCX-файлы)"
            };

            DialogResult result = folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxOutFilesFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void CmdUnassocFromPdf_Click(object sender, EventArgs e)
        {
            bool result = FileAssociation.UnAssociate(FileExt, ProgMenuItem);
            if (result)
            {
                MessageBox.Show($"Пункт '{ProgMenuItem}' успешно удален из контекстного меню расширения '{FileExt}'", "Удаление связи расширения с приложением", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Пункт '{ProgMenuItem}' НЕ удален из контекстного меню расширения '{FileExt}'\n\nError: " + FileAssociation.ErrorMessage, "Удаление связи расширения с приложением", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

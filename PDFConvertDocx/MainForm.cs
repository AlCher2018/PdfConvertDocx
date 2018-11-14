using PDFConvertDocxRu.Services;
using System;
using System.Windows.Forms;

namespace PDFConvertDocxRu
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            labelWait.Visible = false;
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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
                Program.ConvertToDocx(openFile.FileName, 
                    () => 
                    {
                        this.labelWait.Visible = true;
                        this.Refresh();
                    },
                    () =>
                    {
                        this.labelWait.Visible = false;
                        this.Refresh();
                    });
            }
        }
    }
}

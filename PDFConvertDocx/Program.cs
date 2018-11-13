using PDFConvertDocxRu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFConvertDocxRu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppConfig.ReadValues();

            // если pdf-file передан в аргументах
            if ((args != null) && (args.Length > 0))
            {
                string appName = (new System.IO.FileInfo(Application.ExecutablePath)).Name;
                string filePath = args[0];
                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show($"PROGRAM TERMINATED\nFile '{filePath}' is not exists.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }

                PdfDocConverterSautin converter = new PdfDocConverterSautin()
                {
                    IsOpenAfterConverting = AppConfig.IsOpenOutfile,
                    DocFilePath = AppConfig.OutFolderDefault
                };
                bool resConv = converter.Convert(filePath);

                if (resConv == false)
                {
                    MessageBox.Show("Convert error: " + converter.ErrorMessage, appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Convert successfull with load time: {converter.LoadTime}, save time: {converter.SaveTime}", appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // интерактивный режим
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}

using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFConvertDocxRu.Services
{
    // SautinSoft - https://sautinsoft.com/index.php
    // https://sautinsoft.com/products/document/index.php?gclid=Cj0KCQiAlIXfBRCpARIsAKvManwZeM3q2amAdPrKhh-YT36ryfwfsh1tb4v74himar8zXMblsMgTo2UaAqSyEALw_wcB
    // --> https://www.nuget.org/packages/sautinsoft.document/ --> Install-Package sautinsoft.document -Version 3.5.9.26
    public class PdfDocConverterSautin : IPdfDocConverter
    {
        #region inmplement IPdfDocConverter
        public string PdfFilePath { get; set; }
        public string DocFilePath { get; set; }
        public bool IsOpenAfterConverting { get; set; }
        public string ErrorMessage { get; private set; }

        public bool Convert()
        {
            return DoConvert();
        }
        public bool Convert(string pdfFilePath)
        {
            this.PdfFilePath = pdfFilePath;
            return DoConvert();
        }
        #endregion

        // public filelds (without interface)
        public TimeSpan LoadTime { get; private set; }
        public TimeSpan SaveTime { get; private set; }


        private readonly string _typeName;
        // CTOR
        public PdfDocConverterSautin()
        {
            _typeName = this.GetType().Name;
        }

        private bool DoConvert()
        {
            LoadTime = TimeSpan.Zero;
            SaveTime = TimeSpan.Zero;

            #region check parameters
            if (string.IsNullOrEmpty(this.PdfFilePath))
            {
                SetErrMsg("The class hasn't get the PdfFilePath property");
                return false;
            }
            if (System.IO.File.Exists(this.PdfFilePath) == false)
            {
                SetErrMsg($"The file '{this.PdfFilePath}' ({this.PdfFilePath.Length} chars) is not exists or OS not reads the full path.");
                return false;
            }
            #endregion

            DateTime dt = DateTime.MinValue;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.PdfFilePath);
            string dir = (string.IsNullOrEmpty(DocFilePath) ? fileInfo.DirectoryName : DocFilePath);
            if (!dir.EndsWith(@"\")) dir += @"\";
            string name = fileInfo.Name.Replace(".pdf", "");  // get name only, without extension
            string outFile = dir + name + ".docx";

            // get file
            DocumentCore dc = null;
            try
            {
                dt = DateTime.Now;
                dc = DocumentCore.Load(this.PdfFilePath);
            }
            catch (Exception ex)
            {
                SetErrMsg($"File '{this.PdfFilePath}' load error: " + ex.Message);
                // даже если будет ошибка, то сначала выполнится блок finally, а потом только return !!!!!!!!!
                return false;
            }
            finally
            {
                LoadTime = DateTime.Now - dt;
            }

            // save file
            DocxSaveOptions options = new DocxSaveOptions()
            {
                Format = DocxFormat.Docx
            };
            try
            {
                dt = DateTime.Now;
                dc.Save(outFile, options);
            }
            catch (Exception ex)
            {
                SetErrMsg($"File '{outFile}' save error: " + ex.Message);
                return false;
            }
            finally
            {
                SaveTime = DateTime.Now - dt;
            }

            // Open the result for demonstation purposes.
            if (IsOpenAfterConverting)
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outFile) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    SetErrMsg($"File '{outFile}' open error: " + ex.Message);
                    return false;
                }
            }

            // successfully convert, save and open
            return true;
        }


        private void SetErrMsg(string errMsg)
        {
            ErrorMessage = _typeName + "|" + errMsg;
        }
    }
}

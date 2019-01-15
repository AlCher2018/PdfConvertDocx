using InvoiceTranslator.Services;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SautinSoft.Document.Drawing;


namespace InvoiceTranslator
{
    static class Program
    {
        private static string AppName;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppName = (new System.IO.FileInfo(Application.ExecutablePath)).Name;
            AppConfig.ReadValues();

            // если pdf-file передан в аргументах
            if ((args != null) && (args.Length > 0))
            {
                string filePath = args[0];

                ConvertToDocx(filePath, null, null);
            }
            // интерактивный режим
           else
           {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        public static void ConvertToDocx(string filePath, Action actionBefore, Action actionAfter)
        {
            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show($"PROGRAM TERMINATED\nFile '{filePath}' is not exists.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            PdfDocConverterSautin converter = new PdfDocConverterSautin()
            {
                IsOpenAfterConverting = AppConfig.IsOpenOutfile,
                DocFilePath = AppConfig.OutFolderDefault
            };

            actionBefore?.Invoke();
            // преобразовать
            bool resConv = converter.Convert(filePath);
            // перевести
            if (resConv)
            {
                /*
'Hand tool'='Ручной инструмент'
'Total USD'='Стоимость'
'USD /Unit'='USD/ед.'
'Consignee'='Грузополучатель'
'Metal working machine tool parts and accessories'='Запасные части для сменного инструмента'
'Quantity'='Кол-во'
'USA (US)'='США'
'Material'='материал'
'Customer'='Заказчик'
'Shipment'='Отгрузка'
'Original'='Оригинал'
'Article'='Статья'
'Account'='Группа'
'Package'='Упаковка'
'FINLAND'='ФИНЛЯНДИЯ'
'DENMARK'='ДАНИЯ'
'GERMANY'='ГЕРМАНИЯ'
'Ukraine'='Украина'
'INVOICE'='Инвойс'
'Height'='Высота'
'claim.'=' или требованиях'
'Length'='Длина'
'Ref No'='№'
'Weight'='Вес'
'Item D'='Ед.'
'FRANCE'='ФРАНЦИЯ'
'POLAND'='ПОЛЬША'
'RUSSIA'='РОССИЯ'
                 */
                Dictionary<string, string> dict = new Dictionary<string, string>()
                {
                    {"Invoice", "Инвойс"},
                    {"Page", "Стр"},
                    {"60 days from invoice date, net", "60 дней от даты поставки товара"},
                    {"Quantity", "Кол-во"},
                    {"USA (US)", "США"},
                    {"Material", "материал"},
                    {"Customer", "Заказчик"},
                    {"Shipment", "Отгрузка"},
                    {"Original", "Оригинал"},
                    {"Article", "Статья"},
                    {"Account", "Группа"},
                    {"Package", "Упаковка"}
                };

                DocumentCore doc = DocumentCore.Load(converter.DocFileFullName);
                List<Paragraph> paragraphs = doc.GetChildElements(true, new ElementType[] { ElementType.Paragraph }).Select(i => i as Paragraph).ToList();

                foreach (Paragraph item in paragraphs)
                {
                    foreach (Run run in item.Inlines.OfType<Run>())
                    {
                        if (string.IsNullOrEmpty(run.Text.Trim())) continue;

                        if (dict.ContainsKey(run.Text))
                        {
                            run.Text = dict[run.Text];
                        }
                        else
                        {
                            foreach (string key in dict.Keys)
                            {
                                if (run.Text.Contains(key)) run.Text = run.Text.Replace(key, dict[key]);
                            }
                        }
                    }
                }

                doc.Save(converter.DocFileFullName);
            }
            actionAfter?.Invoke();

            if (resConv == false)
            {
                MessageBox.Show("Convert error: " + converter.ErrorMessage, AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Convert successfull with load time: {converter.LoadTime}, save time: {converter.SaveTime}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private static void RemoveTrialObjects()
        {
            //string fName = @"C:\Users\Leschenko.V\Source\Repos\PDFConvertDocx\PDFConvertDocx\bin\Debug\inFolder\NSE_1_Certificate.pdf";
            //string fNameResult = @"C:\Users\Leschenko.V\Source\Repos\PDFConvertDocx\PDFConvertDocx\bin\Debug\outFolder\NSE_1_Certificate.docx";

            //DocumentCore doc = DocumentCore.Load(fName);
            //// поиск блока с триальным текстом
            //List<Shape> shapes = doc.GetChildElements(true, new ElementType[] { ElementType.Shape }).Select(i => i as Shape).ToList();
            ////List<Shape> notContentShapes = new List<Shape>();
            //bool hasContent;
            //foreach (Shape shape in shapes)
            //{
            //    hasContent = false;
            //    if (shape.Content.ToString().StartsWith("Created by the trial version"))
            //    {
            //        while (shape.Text.Blocks.Count > 0) shape.Text.Blocks.RemoveAt(0);
            //        //shape.Fill.SetEmpty();
            //        shape.Fill.SetSolid(new Color(0));
            //        hasContent = true;
            //    }
            //    //if (!hasContent) notContentShapes.Add(shape);
            //}

            //notContentShapes.ForEach(s => { s.Content.Delete(); });

            // https://sautinsoft.com/products/document/examples/delete-content-net-csharp-vb.php
            // Remove the text "This" from all paragraphs in 1st section.
            //foreach (Paragraph par in doc.Sections[0].GetChildElements(true, ElementType.Paragraph))
            //{
            //    var findText = par.Content.Find("trial");

            //    if (findText != null)
            //    {
            //        foreach (ContentRange cr in findText) cr.Delete();
            //    }
            //}
            //            doc.Save(fNameResult);

        }

    }
}

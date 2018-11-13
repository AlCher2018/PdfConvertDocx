using System;
using System.Collections.Generic;
using System.Text;

namespace PDFConvertDocxRu.Services
{
    public interface IPdfDocConverter
    {
        // полный путь к PDF-файлу (исходный файл)
        string PdfFilePath { get; set; }
        // полный путь к doc-файлу (результат)
        string DocFilePath { get; }

        // окрывать ли результирующий файл в MS Word после конвертации
        bool IsOpenAfterConverting { get; set; }

        // сообщение об ошибке
        string ErrorMessage { get; }
        // метод, преобразовывающий pdf-файл в doc-формат
        // возвращает признак успешности преобразования и создания выходного файла
        bool Convert();
        bool Convert(string pdfFilePath);

    }
}

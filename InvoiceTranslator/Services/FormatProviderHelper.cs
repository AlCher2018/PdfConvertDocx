using System;
using System.Globalization;


namespace InvoiceTranslator
{
    public static class FormatProviderHelper
    {

        public static IFormatProvider DotFormatter()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            return nfi;
        }

        public static IFormatProvider NumDecimalFormatProvider = System.Globalization.CultureInfo.InvariantCulture;

    }  // class
}

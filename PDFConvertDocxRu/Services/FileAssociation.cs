using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFConvertDocxRu.Services
{
    public static class FileAssociation
    {
        private static string _errMsg;
        private static string ErrorMessage { get { return _errMsg; } }

        // Associate file extension with progID, description, icon and application
        // in OpenWithList subKey
        public static bool Associate(string extension, string progID, string description, string icon, string application)
        {
            bool retVal = false;
            _errMsg = null;

            try
            {
                RegistryKey regExt = Registry.ClassesRoot.OpenSubKey(extension, true);
                if (regExt == null)
                {
                    // создать ветку для расширения
                    if (progID != null && progID.Length > 0)
                    {
                        using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(progID))
                        {
                            if (description != null)
                                key.SetValue("", description);
                            if (icon != null)
                                key.CreateSubKey("Icon_for_" + extension).SetValue("", ToShortPathName(icon));
                        }
                    }
                    else
                    {
                        _errMsg = $"В ветке реестра {Registry.ClassesRoot} не найден ключ для расширения '{extension}' и не указан progID";
                        regExt.Dispose();
                        return false;
                    }
                }

                RegistryKey regKeyOpenWith = regExt.OpenSubKey("OpenWithList", true);
                if (regKeyOpenWith == null)
                {
                    regKeyOpenWith = regExt.CreateSubKey("OpenWithList");
                }
                if (application != null)
                {
                    regKeyOpenWith.CreateSubKey(@"Shell\Open\Command").SetValue("",ToShortPathName(application) + " \"%1\"");
                }
                
                retVal = true;
                regKeyOpenWith.Dispose();
                regExt.Dispose();
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }

            return retVal;
        }

        // Return true if extension already associated in registry
        public static bool IsAssociated(string extension)
        {
            bool retVal = false;
            _errMsg = null;
            try
            {
                RegistryKey keyExt = Registry.ClassesRoot.OpenSubKey(extension, false);
                retVal = (keyExt != null);
                keyExt.Dispose();
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
            }

            return retVal;
        }

        // проверка ассоциации расширения (extension) в списке программ "Открыть с помощью..." (OpenWithList)
        public static bool IsAssocAppOpenList(string extension, string appName)
        {
            bool retVal = false;
            _errMsg = null;
            try
            {
                RegistryKey keyExt = Registry.ClassesRoot.OpenSubKey(extension, false);
                if (keyExt != null)
                {
                    keyExt = keyExt.OpenSubKey("OpenWithList", false);
                    if (keyExt != null)
                    {

                    }
                }
                keyExt.Dispose();
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
            }
            return retVal;
        }

        [DllImport("Kernel32.dll")]
        private static extern uint GetShortPathName(string lpszLongPath,
            [Out] StringBuilder lpszShortPath, uint cchBuffer);

        // Return short path format of a file name
        private static string ToShortPathName(string longName)
        {
            StringBuilder s = new StringBuilder(1000);
            uint iSize = (uint)s.Capacity;
            uint iRet = GetShortPathName(longName, s, iSize);
            return s.ToString();
        }
    }
}

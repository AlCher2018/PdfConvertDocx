using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFConvertDocxRu.Services
{
    // сделано согласно:
    // https://www.howtogeek.com/107965/how-to-add-any-application-shortcut-to-windows-explorers-context-menu/
    //


    public static class FileAssociation
    {
        // HKEY_CLASSES_ROOT\.[extention]
        private static RegistryKey extKey = null;
        // HKEY_CLASSES_ROOT\[progId]
        private static RegistryKey appProgIdKey = null;
        // HKEY_CLASSES_ROOT\[progId]\shell\[menuItem]\command
        private static RegistryKey commandKey = null;

        public static string ErrorMessage { get; private set; }


        // Associate file extension with progID, description, icon and application
        public static bool Associate(string extension, string progId, string menuItemName, string progFileFullName)
        {
            ErrorMessage = null;
            if (extension.StartsWith(".")) extension = extension.Substring(1);

            // проверка наличия приложения на диске
            FileInfo fInfo = new FileInfo(progFileFullName);
            if (fInfo.Exists == false)
            {
                ErrorMessage = $"File '{progFileFullName}' not exists";
                return false;
            }

            // [HKEY_CLASSES_ROOT\.pdf] and get progId
            extKey = GetRegKey(Registry.ClassesRoot, "." + extension, false);
            if (extKey == null)
            {
                if (string.IsNullOrEmpty(progId))
                {
                    ErrorMessage = "You must pass progId for extension '" + extension + "'";
                    return false;
                }
                extKey = CreateFolder(Registry.ClassesRoot , "." + extension, progId);
                if (extKey == null) return false;
            }
            else
            {
                progId = extKey.GetValue("").ToString();
            }

            // HKEY_CLASSES_ROOT\[progId]
            appProgIdKey = GetRegKey(Registry.ClassesRoot, progId, true);
            if (appProgIdKey == null)
            {
                appProgIdKey = CreateFolder(Registry.ClassesRoot, progId, null);
                if (appProgIdKey == null)
                {
                    CloseResources(); return false;
                }
            }

            // HKEY_CLASSES_ROOT\[progId]\shell\[menuItem]\command
            commandKey = GetRegKey(appProgIdKey, $"shell\\{menuItemName}\\command", true);
            string keyValue = progFileFullName + " \"%1\"";
            // если нет такого ключа, то создаем с установкой значения по умолчанию
            if (commandKey == null)
            {
                commandKey = CreateFolder(appProgIdKey, $"shell\\{menuItemName}\\command", keyValue);
                if (commandKey == null)
                {
                    CloseResources(); return false;
                }
            }
            // иначе проверяем путь к приложению
            else
            {
                string keyValueReg = commandKey.GetValue("").ToString();
                if (keyValueReg.Equals(keyValue, StringComparison.OrdinalIgnoreCase) == false)
                {
                    try
                    {
                        commandKey.SetValue("", keyValue);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
                        CloseResources(); return false;
                    }
                }
            }

            return true;
        }

        public static bool UnAssociate(string extension, string menuItemName)
        {
            ErrorMessage = null;
            if (extension.StartsWith(".")) extension = extension.Substring(1);

            // [HKEY_CLASSES_ROOT\.pdf] and get progId
            string progId = null;
            extKey = GetRegKey(Registry.ClassesRoot, "." + extension, false);
            if (extKey == null) return false;
            progId = extKey.GetValue("").ToString();

            // HKEY_CLASSES_ROOT\[progId]
            appProgIdKey = GetRegKey(Registry.ClassesRoot, progId, true);
            if (appProgIdKey == null)
            {
                CloseResources(); return false;
            }

            // trying to delete HKEY_CLASSES_ROOT\[progId]\shell\[menuItem]\command
            try
            {
                appProgIdKey.DeleteSubKeyTree($"shell\\{menuItemName}");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                CloseResources(); return false;
            }

            return true;
        }


        // Return true if extension already associated in registry
        // проверяется расширение, имя пункта меню и полное имя прилоежения
        public static bool IsAssociated(string extension, string menuItemName, string progFileFullName)
        {
            ErrorMessage = null;
            if (extension.StartsWith(".")) extension = extension.Substring(1);

            // проверка наличия приложения на диске
            FileInfo fInfo = new FileInfo(progFileFullName);
            if (fInfo.Exists == false)
            {
                ErrorMessage = $"File '{progFileFullName}' not exists";
                return false;
            }

            // все ключи открывать для чтения
            // расширение находится в ветке Classes_Root\.[extention], значение по умолчанию - progId
            // [HKEY_CLASSES_ROOT\.pdf]
            // @="pdffile"      - значение по умолчанию (progId)
            extKey = GetRegKey(Registry.ClassesRoot, "." + extension, false);
            if (extKey == null)
            {
                CloseResources(); return false;
            }

            string progId = extKey.GetValue("").ToString();
            // проверка наличия HKEY_CLASSES_ROOT\[progId]
            appProgIdKey = GetRegKey(Registry.ClassesRoot, progId, false);
            if (appProgIdKey == null)
            {
                CloseResources(); return false;
            }

            // проверка наличия ветки HKEY_CLASSES_ROOT\[progId]\shell\[menuItem]
            RegistryKey key = GetRegKey(Registry.ClassesRoot, $"{progId}\\shell\\{menuItemName}", false);
            if (key == null)
            {
                CloseResources(); return false;
            }
            key.Dispose(); key = null;

            // проверка полного имени приложения в
            // HKEY_CLASSES_ROOT\[progId]\shell\[menuItem]\command
            commandKey = GetRegKey(appProgIdKey, $"shell\\{menuItemName}\\command", false);
            if (commandKey == null)
            {
                CloseResources(); return false;
            }
            string appFilePath = commandKey.GetValue("").ToString();
            if (appFilePath.Equals(progFileFullName, StringComparison.OrdinalIgnoreCase) == false)
            {
                CloseResources(); return false;
            }

            return true;
        }

        private static RegistryKey GetRegKey(RegistryKey parentKey, string path, bool writable)
        {
            if (ErrorMessage != null) ErrorMessage = null;
            RegistryKey key = null;

            try
            {
                key = parentKey.OpenSubKey(path, writable);
                if (key == null)
                {
                    ErrorMessage = "Not exists '" + parentKey.Name + "\\" + path + "' key.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return key;
        }

        private static RegistryKey CreateFolder(RegistryKey parentKey, string path, string defaultValue)
        {
            if (ErrorMessage != null) ErrorMessage = null;
            RegistryKey key = null;

            try
            {
                key = parentKey.CreateSubKey(path, true);
                if (key == null)
                {
                    ErrorMessage = "The folder '" + parentKey.Name + "\\" + path + "' has not created.";
                }
                else if (string.IsNullOrEmpty(defaultValue) == false)
                {
                    key.SetValue("", defaultValue);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return key;
        }


        private static void CloseResources()
        {
            if (extKey != null) { extKey.Dispose(); extKey = null; }
            if (appProgIdKey != null) { appProgIdKey.Dispose(); appProgIdKey = null; }
            if (commandKey != null) { commandKey.Dispose(); commandKey = null; }
        }

    }
}

using IntegraLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PDFConvertDocxRu.Services
{
    public static class AppConfig
    {
        public static string InFolderDefault { get; set; }
        public static string OutFolderDefault { get; set; }
        public static bool IsOpenOutfile { get; set; }

        // прочитать настройки из config-файла
        public static void ReadValues()
        {
            InFolderDefault = getCfgFolder("InFolderDefault", @"inFolder");
            OutFolderDefault = getCfgFolder("OutFolderDefault", @"outFolder");
            IsOpenOutfile = GetBoolValue("IsOpenOutfile");
        }

        private static string getCfgFolder(string key, string defaultValue)
        {
            string sVal = ConfigurationManager.AppSettings[key];
            if (sVal.IsNull()) sVal = defaultValue;

            // относительный путь
            DirectoryInfo dirInfo = new DirectoryInfo(sVal);
            if (dirInfo.Exists == false)
            {
                string parentDir = Application.StartupPath;
                if (!parentDir.EndsWith("\\")) parentDir += "\\";

                sVal = parentDir + sVal;
            }
            else
            {
                sVal = dirInfo.FullName;
            }

            return sVal;
        }

        // записать настройки в config-файл
        public static bool SaveValues()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("InFolderDefault", InFolderDefault);
            dict.Add("OutFolderDefault", OutFolderDefault);
            dict.Add("IsOpenOutfile", (IsOpenOutfile?"1":"0"));

            string errorMsg;
            SaveAppSettings(dict, out errorMsg);
            bool retVal = (errorMsg == null ? true : false);
            return retVal;
        }

        #region common config-file methods
        public static string GetStringValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static bool GetBoolValue(string key, bool defaultValue = false)
        {
            string value = ConfigurationManager.AppSettings[key];
            bool retVal = (value == null ? defaultValue : value.ToBool());
            return retVal;
        }
        public static int GetIntValue(string key, int defaultValue = 0)
        {
            string value = ConfigurationManager.AppSettings[key];
            int retVal = (value == null ? defaultValue : value.ToInt());
            return retVal;
        }
        public static double GetDoubleValue(string key, double defaultValue = 0d)
        {
            string value = ConfigurationManager.AppSettings[key];
            double retVal = (value == null ? defaultValue : value.ToDouble());
            return retVal;
        }

        public static DateTime GetDateTimeValue(string key, IFormatProvider provider = null)
        {
            string value = ConfigurationManager.AppSettings[key];
            DateTime retVal = DateTime.MinValue;
            if (value != null)
            {
                DateTime dt;
                if (provider == null) provider = System.Globalization.CultureInfo.InvariantCulture;
                if (DateTime.TryParse(value, provider, System.Globalization.DateTimeStyles.None, out dt)) retVal = dt;
            }
            return retVal;
        }
        public static TimeSpan GetTimeSpanValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            TimeSpan retVal = TimeSpan.Zero;
            if (value != null)
            {
                TimeSpan ts;
                if (TimeSpan.TryParse(value, System.Globalization.CultureInfo.InvariantCulture, out ts)) retVal = ts;
            }
            return retVal;
        }

        // работа с config-файлом как с XML-документом - сохраняем комментарии
        // параметр appSettingsDict - словарь из ключа и значения (string), которые необх.сохранить в разделе appSettings
        public static bool SaveAppSettings(Dictionary<string, string> appSettingsDict, out string errorMsg)
        {
            // Open App.Config of executable
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string cfgFilePath = config.FilePath;
            bool isSeparateAppSettings = false;
            string appCfgFile = config.AppSettings.SectionInformation.ConfigSource;
            if (appCfgFile.IsNull() == false)
            {
                isSeparateAppSettings = true;
                cfgFilePath = System.IO.Path.GetDirectoryName(cfgFilePath) + "\\" + appCfgFile;
            }

            try
            {
                errorMsg = null;
                string filename = cfgFilePath;

                //Load the config file as an XDocument
                XDocument document = XDocument.Load(filename, LoadOptions.PreserveWhitespace);
                if (document.Root == null)
                {
                    errorMsg = "Document was null for XDocument load.";
                    return false;
                }

                // получить раздел appSettings
                XElement xAppSettings;
                if (isSeparateAppSettings)   // в отдельном файле
                {
                    xAppSettings = document.Root;
                }
                else    // в App.config
                {
                    xAppSettings = document.Root.Element("appSettings");
                    if (xAppSettings == null)
                    {
                        xAppSettings = new XElement("appSettings");
                        document.Root.Add(xAppSettings);
                    }
                }

                // цикл по ключам словаря значений
                foreach (KeyValuePair<string, string> item in appSettingsDict)
                {
                    XElement appSetting = xAppSettings.Elements("add").FirstOrDefault(x => x.Attribute("key").Value == item.Key);
                    if (appSetting == null)
                    {
                        //Create the new appSetting
                        xAppSettings.Add(new XElement("add", new XAttribute("key", item.Key), new XAttribute("value", item.Value)));
                    }
                    else
                    {
                        //Update the current appSetting
                        appSetting.Attribute("value").Value = item.Value;
                    }
                }

                //Save the changes to the config file.
                document.Save(filename, SaveOptions.DisableFormatting);

                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");

                return true;
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("There was an exception while trying to update the config file ({0}): {1}",
                    cfgFilePath, ex.ToString());
                return false;
            }
        }

        public static bool SaveAppSettings(string key, string value, out string errorMsg)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>() { { key, value } };

            bool retVal = SaveAppSettings(dict, out errorMsg);
            if (retVal == false)
            {
                errorMsg += string.Format(": key={0}, value={1}", key, value);
            }
            return retVal;
        }
        #endregion
    }
}

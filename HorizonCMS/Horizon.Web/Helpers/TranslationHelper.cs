using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.Helpers
{

    public static class TranslationHelper
    {
        public static string isActive(string page, Uri currentUrl)
        {
            string Result = string.Empty;

            if (!string.IsNullOrEmpty(page) && !string.IsNullOrEmpty(currentUrl.ToString()))
            {
                if (!string.IsNullOrEmpty(page))
                    Result = page.Equals(currentUrl.AbsolutePath.Substring(currentUrl.AbsolutePath.LastIndexOf('/') + 1)) ? "active" : "";
            }

            return Result;
        }



        public static string _t(string name, Dictionary<string, string> LanguageDictionary)
        {
            string Result = name;

            if (LanguageDictionary != null)
            {
                if (!string.IsNullOrEmpty(name))
                    Result = LanguageDictionary.ContainsKey(name) && !string.IsNullOrEmpty(LanguageDictionary[name].ToString()) ? LanguageDictionary[name].ToString() : name;
            }

            return Result;
        }
    }
}
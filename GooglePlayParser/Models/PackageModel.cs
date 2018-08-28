using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooglePlayParser.Models
{
    public class PackageModel
    {
        public string Data { get; set; }

        public string GetUrl()
        {
            if (Data == null)
                return null;
            return Data.StartsWith("https://play.google.com/") ? Data : $"https://play.google.com/store/apps/details?id={Data}";
        }

        public string GetPackageName()
        {
            if (Data == null)
                return null;
            return Data.StartsWith("https://play.google.com/") ? Data.Split('=')[1] : Data;
        }
    }
}
﻿using GooglePlayParser.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GooglePlayParserLibrary
{
    public class ParserManager
    {
        public static HtmlDocument GetPageDocument(string packageName)
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                string pageSource = webClient.DownloadString($"https://play.google.com/store/apps/details?id={packageName}");

                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(pageSource);

                return document;
            }
        }

        public static ApplicationModel GetApplicationData(HtmlDocument doc, string packageName)
        {
            // Ищем необходимые поля в документе...
            ApplicationModel data = new ApplicationModel();
            data.PackageName = packageName;
            data.Name = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'AHFaub')]")?.InnerText;
            data.Icon = doc.DocumentNode.SelectSingleNode("//img[contains(@class, 'T75of ujDFqe')]")?.GetAttributeValue("src", "");
            data.Description = WebUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'DWPxHb')]")?.InnerText);

            var nodes1 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'hAyfc')]");
            foreach (var node in nodes1)
            {
                string header = node.ChildNodes[0].InnerText;
                switch (header)
                {
                    case "Updated": data.UpdateTime = DateTime.Parse(node.ChildNodes[1].InnerText).ToShortDateString(); break;
                    case "Installs": data.InstallCount = node.ChildNodes[1].InnerText; break;
                    case "In-app Products": data.InternalPrice = node.ChildNodes[1].InnerText; break;
                }
            }

            var news = doc.DocumentNode.SelectNodes("//*[contains(@class, 'DWPxHb')]");
            if (news != null && news.Count > 1)
                data.WhatsNew = news[1]?.InnerText;
            data.Email = doc.DocumentNode.SelectNodes("//div[contains(@class, 'hAyfc')]/span/div/span/div[2]")?[1]?.InnerText;

            string ratingCount = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'EymY4b')]/span[2]")?.InnerText;
            data.RatingCount = int.Parse(ratingCount.Replace(",", ""));

            string rating = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'BHMmbe')]")?.InnerText;
            data.Rating = Math.Round(double.Parse(rating.Replace('.', ',')), 2);

            string price = doc.DocumentNode.SelectSingleNode("//button[contains(@class, 'LkLjZd ScJHi HPiPcc IfEcue')]")?.InnerText;
            data.Price = price == "Install" ? 0 : int.Parse(price.Split(new char[] { ' ', ' ' })[1].Split('.')[0]);

            try
            {
                var date1 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'hAyfc')][1]/span/div/span");
                string updateTime = date1[0]?.InnerText;
                data.UpdateTime = DateTime.Parse(updateTime).ToShortDateString();
            }
            catch
            {
                var date2 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'hAyfc')][2]/span/div/span");
                string updateTime = date2[0]?.InnerText;
                data.UpdateTime = DateTime.Parse(updateTime).ToShortDateString();
            }


            // Записываем все скриншоты
            List<string> screenshots = new List<string>();
            var imgElements = doc.DocumentNode.SelectNodes("//button[contains(@class, 'NIc6yf')]/img");
            foreach (var el in imgElements)
            {
                string src = el.GetAttributeValue("src", "");
                if (src != "")
                    screenshots.Add(src);
            }

            //data.Screenshots = JsonConvert.SerializeObject(screenshots);
            data.Screenshots = screenshots;

            return data;
        }
    }
}

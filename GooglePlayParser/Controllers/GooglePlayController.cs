using GooglePlayParser.Models;
using GooglePlayParserLibrary;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GooglePlayParser.Controllers
{
    public class GooglePlayController : ApiController
    {
        private static Dictionary<string, ApplicationModel> _applications = new Dictionary<string, ApplicationModel>();

        // GET: api/GooglePlay
        public object Get()
        {
            return _applications;
        }

        // GET: api/GooglePlay?id=com.rockstargames.gtavc
        public object Get(string id)
        {
            if (id == null) return new
            {
                error = "Название пакета не может быть пустым!"
            };

            if (!_applications.ContainsKey(id))
            {
                HtmlDocument doc = null;

                try
                {
                    doc = ParserManager.GetPageDocument(id);
                }
                catch (Exception ex)
                {
                    return new
                    {
                        error = $"Такого приложения на Google Play не существует."
                    };
                }

                try
                {
                    ApplicationModel app = ParserManager.GetApplicationData(doc, id);
                    if (!app.Verify())
                        return new
                        {
                            error = $"Объект собран не полностью."
                        };
                    _applications[app.PackageName] = app;
                    return app;
                }
                catch (Exception ex)
                {
                    return new
                    {
                        error = ex.Message
                    };
                }
            }
            return _applications[id];
        }

        // POST: api/GooglePlay
        [HttpPost]
        public object Post([FromBody]PackageModel model)
        {
            ApplicationModel app = null;
            HtmlDocument doc = null;

            try
            {
                doc = ParserManager.GetPageDocument(model.GetPackageName());
            }
            catch (Exception ex)
            {
                return new
                {
                    error = $"Такого приложения на Google Play не существует."
                };
            }

            try
            {
                app = ParserManager.GetApplicationData(doc, model.GetPackageName());
                if (!app.Verify())
                    return new
                    {
                        error = $"Объект собран не полностью."
                    };
                _applications[app.PackageName] = app;
            }
            catch (Exception ex)
            {
                return new
                {
                    error = $"Серверная ошибка. [DEBUG DATA (not for production)]: {ex.Message}."
                };
            }
            return app;
        }
    }
}

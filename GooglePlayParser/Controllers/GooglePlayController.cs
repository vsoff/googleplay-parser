using GooglePlayParser.Models;
using GooglePlayParserLibrary;
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
            if (!_applications.ContainsKey(id))
                return null;

            return _applications[id];
        }

        // POST: api/GooglePlay
        [HttpPost]
        public object Post([FromBody]PackageModel model)
        {
            ApplicationModel app = null;
            try
            {
                app = ParserManager.GetApplicationData(model.GetPackageName());
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

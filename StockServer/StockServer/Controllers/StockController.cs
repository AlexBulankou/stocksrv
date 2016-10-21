namespace StockServer.Controllers
{
    using Microsoft.ApplicationInsights;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Helpers;
    using System.Web.Http;
    using System.Web.Http.Cors;


    [Authorize]
    public class StockController : ApiController
    {

        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public object Quote(string id)
        {
            var telemetryClient = new TelemetryClient();
            string url = "https://finance.google.com/finance/info?client=ig&q=" + id;
            if (new Random().NextDouble() > 0.7)
            {
                url = "https://finance.google.com/finance/info?client=ig&c=" + id;
            }

            try
            {
                telemetryClient.TrackTrace("Calling " + url);
                using (var response = WebRequest.CreateHttp(url).GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            return JsonConvert.DeserializeObject(reader.ReadToEnd().Substring(4));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                throw;
            }
        }
    }
}

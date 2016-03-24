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

namespace StockServer.Controllers
{
    [Authorize]
    public class StockController : ApiController
    {
       
        // GET api/values/5
        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        [EnableCors("*","*","*")]
        public object Quote(string id)
        {
            using (var response = WebRequest.CreateHttp("https://finance.google.com/finance/info?client=ig&q=" + id).GetResponse())
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
    }
}

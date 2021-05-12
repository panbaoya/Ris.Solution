using Ris.Bll;
using Ris.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ris.Api.Hl7.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post([FromBody]  string imageNumber)
        {
            IRegisterBll bll = new RegisterBll();
            var result=bll.ImageStatus(imageNumber);
            return result?"更新成功.":"更新失败.";
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

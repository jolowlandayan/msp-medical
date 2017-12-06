using System;
using System.Collections.Generic;
using System.Web.Http;
using msp_medical.Util;
using msp_medical.Infrastructure;
using msp_medical.Infrastructure.Entities;
using Newtonsoft.Json;
using msp_medical.Infrastructure.Database;
using System.Linq;

namespace msp_medical.Controllers
{
    public class TicketsController : ApiController
    {

      
        [HttpPost]
        public IHttpActionResult Post([FromBody]string value)
        {
            int ticketId = 0;

           var data = LoadFromJson(value);


            using (var context = new DbConfiguration())
            {
                context.PatientInfo.Add(data);
                context.SaveChanges();
            }



            if (data != null)
            {
                ticketId = data.PatientId;
            }
            else
            {
                ticketId = -1;
            }
            return this.Ok(ticketId);
        }

        [HttpGet]
        public IHttpActionResult GetAll() {

            var data = new List<PatientInfo>();
            using (var context = new DbConfiguration())
            {
                data = context.PatientInfo.ToList();
                
            }

            if (data.Count != 0)
            {
                return this.Ok(data);
            }
            else {
                return this.BadRequest();
            }
            
        }
        private PatientInfo LoadFromJson(string response)
        {
            var outObject = JsonConvert.DeserializeObject<PatientInfo>(response);
            return outObject;
        }
    }
}
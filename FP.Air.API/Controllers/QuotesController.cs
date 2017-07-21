using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FP.Air.API.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        // GET: api/values
        [HttpGet, Route("api/quotes/{from}/{to}/{outboundDate}/{inboundDate}")]
        public IEnumerable<string> Get(string from, string to, string outboundDate, string inboundDate)
        {
            //GatewaySearch.GatewaySearchClient a = new GatewaySearch.GatewaySearchClient(GatewaySearch.GatewaySearchClient.EndpointConfiguration.BasicHttpBinding_IGatewaySearch);
            //var response = a.SearchFlightAvailabilityAsync(new GatewaySearch.FPFlightSearchRequest1(new GatewaySearch.FlightSearchRequest {  }, GatewaySearch.SearchVersion.WCF_VERSION1));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

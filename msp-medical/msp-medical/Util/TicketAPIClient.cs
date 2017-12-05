using msp_medical.viewModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace msp_medical.Util
{


    public class TicketAPIClient
    {
        public async Task<int> PostTicketAsync(PatientInfo PatientDetails)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(@"http://msp-medical20171204060458.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                    var response = await client.PostAsJsonAsync("api/tickets", PatientDetails);
                    return await response.Content.ReadAsAsync<int>();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
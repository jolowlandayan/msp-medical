using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace msp_medical.Util
{
    public class Ticket
    {
        public string name { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string maritalStatus { get; set; }
        public DateTime birthday { get; set; }
        public string residence { get; set; }
        public double contactNo { get; set; }
        public DateTime dateOfAdmission { get; set; }
    }

    public class TicketAPIClient
    {
        public async Task<int> PostTicketAsync(string Name, string Sex, string Age, string MaritalStatus, DateTime Birthday, string Residence, double ContactNo, DateTime DateofAdmission)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(@"http://localhost:3979/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var ticket = new Ticket
                    {
                        name = Name,
                        sex = Sex,
                        age = Age,
                        maritalStatus = MaritalStatus,
                        birthday = Birthday,
                        residence = Residence,
                        contactNo = ContactNo,
                        dateOfAdmission = DateofAdmission
                    };

                    var response = await client.PostAsJsonAsync("api/tickets", ticket);
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
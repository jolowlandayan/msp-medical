using System;
using System.Collections.Generic;
using System.Web.Http;
using msp_medical.Util;

namespace msp_medical.Controllers
{
    public class TicketsController : ApiController
    {
        private static int nextTicketId = 1;
        private static Dictionary<int, Ticket> tickets = new Dictionary<int, Ticket>();

        [HttpPost]
        public IHttpActionResult Post(Ticket ticket)
        {
            int ticketId;

            Console.WriteLine("Ticket accepted: name:" + ticket.name + " age:" + ticket.age + " gender:" + ticket.sex);

            lock (tickets)
            {
                ticketId = nextTicketId++;
                TicketsController.tickets.Add(ticketId, ticket);
            }

            return this.Ok(ticketId.ToString());
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using msp_medical.Util;
using msp_medical.viewModels;

namespace msp_medical.Controllers
{
    public class TicketsController : ApiController
    {
        private static int nextTicketId = 1;
        private static Dictionary<int, PatientInfo> tickets = new Dictionary<int, PatientInfo>();

        [HttpPost]
        public IHttpActionResult Post(PatientInfo ticket)
        {
            int ticketId;

            lock (tickets)
            {
                ticketId = nextTicketId++;
                TicketsController.tickets.Add(ticketId, ticket);
            }

            return this.Ok(ticketId.ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace msp_medical.Infrastructure.Entities
{
    [Serializable]
    public class State
    {
        public int Id { get; set; }
        public string ETag { get; set; }
        public string Data { get; set; }
    }
}
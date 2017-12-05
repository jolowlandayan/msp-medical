using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace msp_medical.Infrastructure.Entities
{
    public class PatientPicture
    {
        public int Id { get; set; }
        
        public string PictureURL { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual PatientInfo Patient { get; set; }

        public int PatientId { get; set; }
    }
}
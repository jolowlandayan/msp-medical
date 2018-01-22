using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace msp_medical.Infrastructure.Entities
{
    [Serializable]
    public class PatientInfo
    {
        public int PatientId { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public string Age { get; set; }

        public string MaritalStatus { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public DateTime DateOfAdmission { get; set; }

        public string AggravatingFactors { get; set; }

        public string RelievingFactors { get; set; }

        public string Intensity { get; set; }

        public string Timing { get; set; }

        public string Medications { get; set; }

        public string PreviousHospitalization { get; set; }

        public string MedicationsTaken { get; set; }

        public string Diseases { get; set; }

        public string InjuriesAccidents { get; set; }

        public string Operations { get; set; }

        public string Allergies { get; set; }

        public string WaterSupply { get; set; }

        public string DrinkingWater { get; set; }

        public string HouseholdMembers { get; set; }

        public string Description { get; set; }


    }
}
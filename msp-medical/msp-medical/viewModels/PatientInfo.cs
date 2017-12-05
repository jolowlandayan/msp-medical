using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace msp_medical.viewModels

{
    [Serializable]
    public class PatientInfo
    {
       public string name;
       public string sex;
       public string age;
       public string maritalStatus;
       public DateTime birthday;
       public string residence;
       public double contactNo;
       public DateTime dateOfAdmission = DateTime.Now;

       public string Description;
       public string aggravatingFactors;
       public string relievingfactors;
       public string intensity;
       public string timing;
       public string medications;
       public string previousHospitalization;
       public string medicationsTaken;
       public string diseases;
       public string injuriesAccidents;
       public string operations;
       public string allergies;
       public string waterSupply;
       public string drinkingWater;
       public string householdMembers;

    }
}
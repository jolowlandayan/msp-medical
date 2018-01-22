using msp_medical.Infrastructure.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace msp_medical.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "List of patients for appointments for the day.";

            var test = GetAllPatients();

            ViewBag.PatientList = test;

            return View();
        }

        public ActionResult Contact(int patientDetail)
        {
            ViewBag.Message = "patient details";

            var patient = Patients(patientDetail);
            ViewBag.PatientDetail = patient;
            return View();
        }

        private List<PatientInfo> GetAllPatients()
        {
            string responseText = string.Empty;
            var patients = new List<PatientInfo>();
            var userApi = "/api/tickets";
            var finalUrl = "http://msp-medical20171204060458.azurewebsites.net" + userApi;
            var request = (HttpWebRequest)WebRequest.Create(finalUrl);

            request.Accept = "application/json";
            request.Method = "GET";
            request.ContentType = "application/json";
            var test = new List<PatientInfo>();

            try
            {
                using (var response = request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();

                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                        test = LoadFromJson(responseText);
                    }





                }
                return test;
            }
            catch (WebException e)
            {
                
                return null;
            }
        }
        private List<PatientInfo> Patients(int id)
        {
            string responseText = string.Empty;
            var patients = new List<PatientInfo>();
            var userApi = "/api/tickets?id=" + id;

            //var finalUrl = "http://localhost:3979" + userApi;

            var finalUrl = "http://msp-medical20171204060458.azurewebsites.net" + userApi;  
            var request = (HttpWebRequest)WebRequest.Create(finalUrl);

            request.Accept = "application/json";
            request.Method = "GET";
            request.ContentType = "application/json";
            var test = new List<PatientInfo>();

            try
            {
                using (var response = request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();

                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                        test = LoadFromJson(responseText);
                    }

                }
                return test;
            }
            catch (WebException e)
            {

                return null;
            }
        }
        private List<PatientInfo> LoadFromJson(string response)
        {
            var outObject = JsonConvert.DeserializeObject<List<PatientInfo>>(response);
            return outObject;
        }

    }
}
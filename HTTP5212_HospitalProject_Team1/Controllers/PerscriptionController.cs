using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HTTP5212_HospitalProject_Team1.Models;
using System.Web.Script.Serialization;

namespace HTTP5212_HospitalProject_Team1.Controllers
{
    public class PerscriptionController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static PerscriptionController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }


        // GET: Perscription/List
        public ActionResult List()
        {
            string url = "PerscriptionData/ListPerscription";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PerscriptionDto> perscriptions = response.Content.ReadAsAsync<IEnumerable<PerscriptionDto>>().Result;

            return View(perscriptions);
        }

        // GET: Perscription/Details/5
        public ActionResult Details(int id)
        {
            string url = "PerscriptionData/FindPerscription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PerscriptionDto selectedperscription = response.Content.ReadAsAsync<PerscriptionDto>().Result;

            return View(selectedperscription);
       
        }

        public ActionResult Error()
        {
            return View();
        }

        //GET: User New
        public ActionResult New()
        {
            //string url = "patientdata/listpatient";
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<PatientDto> PatientOptions = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            //return View(PatientOptions);
            return View();
        }

        // POST: Perscription/Create
        [HttpPost]
        public ActionResult Create(Perscription perscription)
        {
            Debug.WriteLine("the inputted perscription is:");
            Debug.WriteLine(perscription.Prescription);
            //objective: add a new perscription into our system using the api
            //curl -H "Content-Type:application/json" -d @perscription.json https://localhost:44397/api/PerscriptionData/AddPerscription
            string url = "PerscriptionData/AddPerscription";

            string jsonpayload = jss.Serialize(perscription);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(url);
            Debug.WriteLine(content);
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Perscription/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "PerscriptionData/FindPerscription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PerscriptionDto SelectedPerscription = response.Content.ReadAsAsync<PerscriptionDto>().Result;

            return View(SelectedPerscription);
            

           
        }

        // POST: Perscription/Update/5
        [HttpPost]
        public ActionResult Update(int id, Perscription perscription)
        {
            string url = "PerscriptionData/UpdatePerscription/" + id;
            Debug.WriteLine(url + "This?");
            string jsonpayload = jss.Serialize(perscription);
            Debug.WriteLine(jsonpayload + "or This?");
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }           
        }

        // GET: Perscription/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "PerscriptionData/FindPerscription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PerscriptionDto SelectedPerscription = response.Content.ReadAsAsync<PerscriptionDto>().Result;
            return View(SelectedPerscription); ;
        }

        // POST: Perscription/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "PerscriptionData/DeletePerscription/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HTTP5212_HospitalProject_Team1.Models;
using System.Web.Script.Serialization;
using Microsoft.AspNetCore.Authorization;
using HTTP5212_HospitalProject_Team1.Models.ViewModels;


namespace HTTP5212_HospitalProject_Team1.Controllers
{
    public class AppointmentController : Controller
    {
        public static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AppointmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/AppointmentData/");
        }




        // GET: Appointment/List
        [System.Web.Mvc.Authorize]
        public ActionResult List()
        {
            //retrieve a list of Appointments from appointmentdatacontroller
            //curl https://localhost:44397/api/AppointmentData/ListAppointments


            string url = "ListAppointments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode); 

            IEnumerable<AppointmentDto> appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;
            // Debug.WriteLine("Number of appointments receieved: ");
            //Debug.WriteLine(shifts.Count());

            return View(appointments);
        }

        // GET: Appointment/Details/5
        [System.Web.Mvc.Authorize]
        public ActionResult Details(int id)
        {
            //retrieve one shift from shiftdatacontroller
            //curl https://localhost:44397/api/AppointmentData/FindAppointment/{id}

            string url = "FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            AppointmentDto selectedappointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            //Debug.WriteLine("shift receieved: ");
            //Debug.WriteLine(selectedshift.ShiftID);

            return View(selectedappointment);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Appointment/New
        [System.Web.Mvc.Authorize]
        public ActionResult New()
        {
            //api/AppointmentData/AddAppointment
           // string url = "AppointmentData/ListAppointment";
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<AppointmentDto> AppointmentOptions = response.Content.ReadAsAsync<IEnumerable<EmployeeDto>>().Result;

            //return View(AppointmentOptions);
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Create(Appointment appointment)
        {
            Debug.WriteLine("the json payload is:");
            //Debug.WriteLine(appointment.TypeOfAppointment);
            //objective: add a new appointment into the system using the API
            //curl -H "Content-Type:application/json" -d @appointment.json https://localhost:44397/api/AppointmentData/addappointment

            string url = "addappointment";


            string jsonpayload = jss.Serialize(appointment);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
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

        // GET: Appointment/Edit/5
        [System.Web.Mvc.Authorize]
        public ActionResult Edit(int id)
        {
            
            string url = "FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentDto selectedappointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            return View(selectedappointment);

        }

        // POST: Appointment/Update/5
        [HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Update(int id, Appointment appointment)
        {
            string url = "UpdateAppointment/" + id;
            Debug.WriteLine(url + "This?");
            string jsonpayload = jss.Serialize(appointment);
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

        // GET: Appointment/Delete/5
        [System.Web.Mvc.Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentDto SelectedAppointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            return View(SelectedAppointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Delete(int id)
        {
            string url = "DeleteAppointment/" + id;
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

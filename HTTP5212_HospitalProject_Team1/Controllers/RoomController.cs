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
    public class RoomController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static RoomController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }

        // GET: Room/List
        public ActionResult List()
        {
            //objective: communicate with Room data api to retrieve a list of tickets
            //curl https://localhost:44397/api/roomdata/listrooms
           
            string url = "roomdata/listrooms";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<RoomDto> rooms = response.Content.ReadAsAsync<IEnumerable<RoomDto>>().Result;
            //Debug.WriteLine("Number of rooms received : ");
            //Debug.WriteLine(room.Count());

            return View(rooms);
        }

        // GET: Room/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with Room data api to retrieve a list of tickets
            //curl https://localhost:44397/api/roomdata/findroom/{id}
           
            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            RoomDto selectedroom = response.Content.ReadAsAsync<RoomDto>().Result;
            //Debug.WriteLine("Number of rooms received : ");
            //Debug.WriteLine(room.Count());

            
            return View(selectedroom);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: User/New
        public ActionResult New()
        {
            //string url = "patientdata/listpatient";
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<PatientDto> PatientOptions = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            //return View(PatientOptions);
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        public ActionResult Create(Room room)
        {
            Debug.WriteLine("the inputted room is:");
            Debug.WriteLine(room.RoomNumber);
            //objective: add a new room into our system using the api
            //curl -H "Content-Type:application/json" -d @room.json https://localhost:44397/api/roomdata/addroom
            string url = "roomdata/addroom";

            string jsonpayload = jss.Serialize(room);
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

        // GET: Room/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RoomDto SelectedRoom = response.Content.ReadAsAsync<RoomDto>().Result;
            return View(SelectedRoom);
        }

        // POST: Room/Update/5
        [HttpPost]
        public ActionResult Update(int id, Room room)
        {
            string url = "roomdata/updateroom/" + id;
            Debug.WriteLine(url + "This?");
            string jsonpayload = jss.Serialize(room);
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

        // GET: Room/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RoomDto SelectedRoom = response.Content.ReadAsAsync<RoomDto>().Result;
            return View(SelectedRoom); ;
        }

        // POST: Room/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "roomdata/deleteroom/" + id;
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

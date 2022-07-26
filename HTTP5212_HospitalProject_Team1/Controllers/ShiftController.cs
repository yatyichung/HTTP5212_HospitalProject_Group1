using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HTTP5212_HospitalProject_Team1.Models;


namespace HTTP5212_HospitalProject_Team1.Controllers
{
    public class ShiftController : Controller
    {

        public static readonly HttpClient client;

        static ShiftController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/ShiftData/");
        }




        // GET: Shift/List
        public ActionResult List()
        {
            //retrieve a list of shifts from shiftdatacontroller
            //curl https://localhost:44397/api/ShiftData/ListShifts


            string url = "ListShifts";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode); 

            IEnumerable<ShiftDto> shifts = response.Content.ReadAsAsync<IEnumerable<ShiftDto>>().Result;
           // Debug.WriteLine("Number of shifts receieved: ");
            //Debug.WriteLine(shifts.Count());

            return View(shifts);
        }

        // GET: Shift/Details/5
        public ActionResult Details(int id)
        {
            //retrieve one shift from shiftdatacontroller
            //curl https://localhost:44397/api/ShiftData/FindShift/{id}

            string url = "FindShift/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            ShiftDto selectedshift = response.Content.ReadAsAsync<ShiftDto>().Result;
            //Debug.WriteLine("shift receieved: ");
            //Debug.WriteLine(selectedshift.ShiftID);

            return View(selectedshift);
        }

        // GET: Shift/New
        public ActionResult New()
        {
            //api/ShiftData/AddShift
            return View();
        }

        // POST: Shift/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shift/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shift/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shift/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shift/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

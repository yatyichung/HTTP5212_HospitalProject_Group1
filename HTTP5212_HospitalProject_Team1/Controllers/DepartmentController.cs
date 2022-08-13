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
    public class DepartmentController : Controller
    {
        public static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }

        // GET: Department/List
        [System.Web.Mvc.Authorize]
        public ActionResult List()
        {
            string url = "DepartmentData/ListDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // GET: Department/Details/5
        [System.Web.Mvc.Authorize]
        public ActionResult Details(int id)
        {
            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto selecteddepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(selecteddepartment);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Department/New
        [System.Web.Mvc.Authorize]

        public ActionResult New()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Create(Department department)
        {
            string url = "DepartmentData/adddepartment";
            string jsonpayload = jss.Serialize(department);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return Redirect("Error");
            }
        }

        // GET: Department/Edit/5
        [System.Web.Mvc.Authorize]
        public ActionResult Edit(int id)
        {
            string url = "DepartmentData/findDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto selecteddepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(selecteddepartment);
        }

        // POST: Department/Update/5
        [HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Update(int id, Department department)
        {
            string url = "DepartmentData/updatedepartment/" + id;
            string jsonpayload = jss.Serialize(department);
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

        // GET: Department/Delete/5
        [System.Web.Mvc.Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "DepartmentData/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto selecteddepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(selecteddepartment);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Delete(int id)
        {
            string url = "DepartmentData/deletedepartment/" + id;
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
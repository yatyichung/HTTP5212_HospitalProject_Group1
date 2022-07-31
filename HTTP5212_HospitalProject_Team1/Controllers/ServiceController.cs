using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HTTP5212_HospitalProject_Team1.Models;
using System.Web.Script.Serialization;
using HTTP5212_HospitalProject_Team1.Models.ViewModels;

namespace HTTP5212_HospitalProject_Team1.Controllers
{
    public class ServiceController : Controller
    {
        public static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ServiceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }

        // GET: Service/List
        public ActionResult List()
        {
            string url = "ServiceData/ListServices";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ServiceDto> services = response.Content.ReadAsAsync<IEnumerable<ServiceDto>>().Result;
            return View(services);
        }

        // GET: Service/Details/5
        public ActionResult Details(int id)
        {
            string url = "ServiceData/FindService/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ServiceDto selectedservice = response.Content.ReadAsAsync<ServiceDto>().Result;
            return View(selectedservice);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Service/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Service/Create
        [HttpPost]
        public ActionResult Create(Service service)
        {
            string url = "ServiceData/addservice";
            string jsonpayload = jss.Serialize(service);
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

        // GET: Service/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateService ViewModel = new UpdateService();

            string url = "ServiceData/findService/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ServiceDto selectedservice = response.Content.ReadAsAsync<ServiceDto>().Result;
            
            ViewModel.selectedservice = selectedservice;
            url = "DepartmentData/ListDepartments";
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> DeptOptions = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            ViewModel.DeptOptions = DeptOptions;
            return View(ViewModel);
        }

        // POST: Service/Update/5
        [HttpPost]
        public ActionResult Update(int id, Service service)
        {
            string url = "ServiceData/updateservice/" + id;
            string jsonpayload = jss.Serialize(service);
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

        // GET: Service/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ServiceData/findservice/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ServiceDto selectedservice = response.Content.ReadAsAsync<ServiceDto>().Result;
            return View(selectedservice);
        }

        // POST: Service/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "ServiceData/deleteservice/" + id;
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
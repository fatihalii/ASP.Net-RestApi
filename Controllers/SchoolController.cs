using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Final.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        public ActionResult Index()
        {
            IEnumerable<Student> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/");
                var responseTask = client.GetAsync("student");
                responseTask.Wait();

                var result = responseTask.Result;


                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Student>>();
                    readTask.Wait();


                    students = readTask.Result;
                }

                else
                {
                    students = Enumerable.Empty<Student>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }




            return View(students);
        }
    }
}
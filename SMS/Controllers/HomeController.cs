using Newtonsoft.Json;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            StudentDetailsViewModel model = new StudentDetailsViewModel();
            SelectListItem item = new SelectListItem() { Text = "All", Value= "-1" };
            model.Enrollments = RestClient.RestClientGet<List<EnrollmentViewModel>>(Constants.BaseUrl,Constants.EnrollmentYearsMethod)
                                            .Select(enr => new SelectListItem { Text = enr.Enrollment.ToString(), Value = enr.Id.ToString() })
                                            .ToList();
            model.Services = RestClient.RestClientGet<List<StudentServiceViewModel>>(Constants.BaseUrl,Constants.SchoolServicessMethod)
                                            .Select(ser => new SelectListItem { Text = ser.ServiceName.ToString(), Value = ser.Id.ToString() })
                                            .ToList();
            model.Students = RestClient.RestClientGet<List<StudentViewModel>>(Constants.BaseUrl, Constants.StudentsMethod)
                                            .Select(std => new SelectListItem { Text = std.FullName.ToString(), Value = std.Id.ToString() })
                                            .ToList();
            model.Enrollments.Insert(0, item);
            model.Services.Insert(0, item);
            model.Students.Insert(0, item);

            return View(model);
        }

        [HttpPost]
        public ActionResult GetAllStudentList(string paramData)
        {
            try
            {
                var filterData = JsonConvert.DeserializeObject<StudentFilterModel>(paramData);
                var result = RestClient.RestClientPost<List<StudentDetailsModel>, StudentFilterModel>(Constants.BaseUrl, Constants.StudentDetailsMethod, filterData);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Failed" });
            }
        }
    }
}
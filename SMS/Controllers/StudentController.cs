using Newtonsoft.Json;
using SMS.Models;
using SMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace SMS.Controllers
{
    public class StudentController : ApiController
    {
        private IStudentService _studentService;
        public StudentController()
        {
            _studentService = new StudentDetailService();
        }
        [ActionName("EnrollmentYears")]
        public IHttpActionResult GetEnrollmentList()
        {
            var enrollmentList= _studentService.GetEnrollmentList();
            if (enrollmentList == null || enrollmentList.Count <= 0)
                return NotFound();
            return Ok(enrollmentList);

        }
        [ActionName("SchoolServices")]
        public IHttpActionResult GetServiceList()
        {
            var serviceList = _studentService.GetServiceList();
            if (serviceList == null || serviceList.Count <= 0)
                return NotFound();
            return Ok(serviceList);
        }
        [ActionName("Students")]
        public IHttpActionResult GetStudentList()
        {
            var studentList = _studentService.GetStudentList();
            if (studentList == null || studentList.Count <= 0)
                return NotFound();
            return Ok(studentList);
        }
        [HttpPost]
        [ActionName("StudentDetails")]
        public HttpResponseMessage GetAllStudentList([FromBody] StudentFilterModel paramData)
        {

            Thread.Sleep(3000);
            //var result =  _studentService.FetchStudentdetails(paramData);
            var result = _studentService.FetchStudentdetailsV2(paramData);

            if (result == null || result.Count <= 0)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

       

    }
}

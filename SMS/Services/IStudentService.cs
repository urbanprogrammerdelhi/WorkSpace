using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    interface IStudentService
    {
        List<StudentViewModel> GetStudentList();
        List<StudentServiceViewModel> GetServiceList();
        List<EnrollmentViewModel> GetEnrollmentList();
        List<StudentDetailsModel> FetchStudentdetails(StudentFilterModel filter);
        List<StudentDetailsModel> FetchStudentdetailsV2(StudentFilterModel filter);
    }
}

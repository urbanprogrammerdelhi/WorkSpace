using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMS.Models;

namespace SMS.Services
{
    public class StudentDetailService : IStudentService
    {
        private StudentdataContext _context;

        public List<StudentDetailsModel> FetchStudentdetails(StudentFilterModel filter)
        {
            _context = new StudentdataContext();
            var studentDetails = _context.sp_FetchStudentDetails(filter.StudentId, filter.EnrollmentId, filter.ServiceId);
            if (studentDetails != null)
            {
                var result = studentDetails.Select(student =>
                new StudentDetailsModel
                {
                    Id = student.Id                    
                    ,DateOfBirth = student.DateOfBirth                    
                    ,EndDate = student.Enddate
                    ,Startdate = student.StartDate
                    ,EnrollmentYear = student.Enrollment
                    ,FullName = student.FirstName + " " + student.LastName
                    ,ServiceName = student.ServiceName
                }).ToList();
                return result;
            }
            return new List<StudentDetailsModel>();
        }

        public List<EnrollmentViewModel> GetEnrollmentList()
        {
            _context = new StudentdataContext();
            var enrollmentList = _context.EnrollmentMasters
                .Select(enrollment => new EnrollmentViewModel { Id = enrollment.Id, Enrollment = enrollment.SchoolYear.Value })
                .ToList();
            return enrollmentList;

        }

        public List<StudentServiceViewModel> GetServiceList()
        {
            _context = new StudentdataContext();
            var servceList = _context.ServiceMasters
                .Select(service => new StudentServiceViewModel { Id = service.Id, ServiceName = service.ServiceName })
                .ToList();
            return servceList;
        }

        public List<StudentViewModel> GetStudentList()
        {
            _context = new StudentdataContext();
            var studentList = _context.Students
              .Select(student => new StudentViewModel { Id = student.Id, FullName = student.FirstName + " " + student.LastName })
              .ToList();
            return studentList;
        }

        public List<StudentDetailsModel> FetchStudentdetailsV2(StudentFilterModel filter)
        {
            _context = new StudentdataContext();
            StudentFilterModel primaryFilter = new StudentFilterModel();
            var studentDetails = _context.sp_FetchStudentDetails(primaryFilter.StudentId, primaryFilter.EnrollmentId, primaryFilter.ServiceId).AsEnumerable();
            var advancedfilter = new AdvancedFilter();
           
           var spec1=new StudentSpecification(filter.StudentId);
            var spec2 =new EnrollmentSpecification(filter.EnrollmentId);
            var spec3=new ServiceSpecification(filter.ServiceId);
            //var result= advancedfilter.Filter(studentDetails, spec1);
            //result = advancedfilter.Filter(result, spec2);
            //result = advancedfilter.Filter(result, spec3);
            var result = advancedfilter.AdvancedFilterMethod(studentDetails,new List<ISpecification<sp_FetchStudentDetails_Result>>{ spec1, spec2, spec3});
            if (result != null)
            {
                var output = result.Distinct().Select(student =>
                new StudentDetailsModel
                {
                    Id = student.Id
                    ,
                    DateOfBirth = student.DateOfBirth
                    ,
                    EndDate = student.Enddate
                    ,
                    Startdate = student.StartDate
                    ,
                    EnrollmentYear = student.Enrollment
                    ,
                    FullName = student.FirstName + " " + student.LastName
                    ,
                    ServiceName = student.ServiceName
                }).ToList();
                return output;
            }
            return new List<StudentDetailsModel>();
        }
    }
}
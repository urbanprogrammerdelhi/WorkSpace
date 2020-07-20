using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Models
{
    public class StudentDetailsViewModel
    {
        public int Selectedstudent { get; set; }
        public int SelectedServices { get; set; }
        public int SelectedEnrollment { get; set; }
        public List<SelectListItem> Students { get; set; }
        public List<SelectListItem> Services { get; set; }
        public List<SelectListItem> Enrollments { get; set; }
        
    }
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
    public class StudentServiceViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
    }
    public class EnrollmentViewModel
    {
        public int Id { get; set; }
        public int Enrollment { get; set; }
    }
    [Serializable]
    public class StudentFilterModel1
    {
        public string StudentId { get; set; }
        public string EnrollmentId { get; set; }
        public string ServiceId { get; set; }
    }
    [Serializable]
    public class StudentFilterModel
    {
        public int? StudentId { get; set; }
        public int? EnrollmentId { get; set; }
        public int? ServiceId { get; set; }
    }
    public class StudentDetailsModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public int? EnrollmentYear { get; set; }
        public string Startdate { get; set; }
        public string EndDate { get; set; }
        public string ServiceName { get; set; }
    }
}
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS
{
    public interface ISpecification<T>
    {
        bool IsMatched(sp_FetchStudentDetails_Result model);
    }
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
    public class StudentSpecification : ISpecification<sp_FetchStudentDetails_Result>
    {
        private int? _studentId;
        public StudentSpecification(int? studentId)
        {
            this._studentId = studentId;
        }
        public bool IsMatched(sp_FetchStudentDetails_Result model)
        {
            if (_studentId <= 0)
                return true;
            return model.Id == _studentId;
        }
    }

    public class EnrollmentSpecification : ISpecification<sp_FetchStudentDetails_Result>
    {
        private int? _enrollmentId;
        public EnrollmentSpecification(int? enrollmentId)
        {
            this._enrollmentId = enrollmentId;
        }
        public bool IsMatched(sp_FetchStudentDetails_Result model)
        {
            if (_enrollmentId <= 0)
                return true;
            return model.EnrollmentId == _enrollmentId;
        }
    }
    public class ServiceSpecification : ISpecification<sp_FetchStudentDetails_Result>
    {
        private int? _serviceId;
        public ServiceSpecification(int? serviceId)
        {
            this._serviceId = serviceId;
        }
        public bool IsMatched(sp_FetchStudentDetails_Result model)
        {
            if (_serviceId <= 0)
                return true;
            return model.ServiceId == _serviceId;
        }
    }
    public class AdvancedFilter : IFilter<sp_FetchStudentDetails_Result>
    {
        public IEnumerable<sp_FetchStudentDetails_Result> Filter
            (IEnumerable<sp_FetchStudentDetails_Result> items,
            ISpecification<sp_FetchStudentDetails_Result> spec)
        {
            foreach (var i in items)
                if (spec.IsMatched(i))
                    yield return i;




        }

        public IEnumerable<sp_FetchStudentDetails_Result> AdvancedFilterMethod
        (IEnumerable<sp_FetchStudentDetails_Result> items,
        List<ISpecification<sp_FetchStudentDetails_Result>> spec)
        {
            var result = new List<sp_FetchStudentDetails_Result>(items);

            foreach (var sp in spec)
            {
                result = new List<sp_FetchStudentDetails_Result>(Filter(result, sp));
            }
            return result;



        }
    }
}




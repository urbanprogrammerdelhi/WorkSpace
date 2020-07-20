using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SMS
{
    public class RestClient
    {
        public static T RestClientGet<T>(string baseUrl,string methodName)
        {
            T output = Activator.CreateInstance<T>();
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                //HTTP GET
                var responseTask = client.GetAsync(methodName);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<T>();
                    readTask.Wait();
                    output = readTask.Result;
                }                
            }
            return output;
        }

        public static TOut RestClientPost<TOut,TIn>(string baseUrl, string methodName,TIn input)
        {
            TOut output = Activator.CreateInstance<TOut>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BaseUrl);
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<TIn>(methodName, input);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TOut>();
                    readTask.Wait();
                    output= readTask.Result;
                }
            }
            return output;
        }
    }
    public class Constants
    {
        public const string EnrollmentYearsMethod = "EnrollmentYears";
        public const string SchoolServicessMethod = "SchoolServices";
        public const string StudentsMethod = "Students";
        public const string StudentDetailsMethod = "StudentDetails";
        public static readonly string BaseUrl = Convert.ToString(ConfigurationManager.AppSettings["BaseAPIURL"]);
    }
}
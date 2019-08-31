using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MemberAppClient
{
    public static class Utility
    {
        public static string ToCamelCase(this string currentString)
        {
            return Regex.Replace(currentString, "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
        }

        public static ApiResponse GetHttpResponseMessage(HttpResponseMessage httpResponseMessage)
        {
            return new ApiResponse
            {
                IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                //Message = httpResponseMessage.RequestMessage.Headers.ToString()
            };
        }
        public static StringContent ToJsonString<T>(this T currentObject)
        {
            string json = JsonConvert.SerializeObject(currentObject);
            return new StringContent(json, UnicodeEncoding.UTF8, "application/json");
        }
    }
}

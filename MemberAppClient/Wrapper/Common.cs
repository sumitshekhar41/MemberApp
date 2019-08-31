using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MemberAppClient.Wrapper
{
    public static class Common
    {
        static Common()
        {
            SetAllConfiguration();
        }

        private static void SetAllConfiguration()
        {
            BaseAddress = System.Configuration.ConfigurationManager.AppSettings[nameof(BaseAddress)];
            API_Member = System.Configuration.ConfigurationManager.AppSettings[nameof(API_Member)];
        }

        public static string BaseAddress { get; private set; }
        public static string API_Member { get; private set; }


        public static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Common.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}

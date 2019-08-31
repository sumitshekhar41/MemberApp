using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace MemberAppClient.Wrapper
{
    public class MemberManagemetWrapper
    {

        public async Task<List<Member>> GetMembers()
        {
            using(var client = Common.GetHttpClient())
            {

                var response = await client.GetAsync($"{Common.API_Member}");

                if(response.IsSuccessStatusCode)
                {
                    var str = response.Content.ReadAsAsync<object>().Result;
                    var contentData = JsonConvert.DeserializeObject<IEnumerable<Member>>(str.ToString());
                    return contentData.ToList();
                }

                return new List<Member>();
            }
        }

        public async Task<ApiResponse> Add(Member member)
        {
            using(var client = Common.GetHttpClient())
            {
                var response = await client.PostAsync(Common.API_Member, member.ToJsonString());
                return Utility.GetHttpResponseMessage(response);
            }
        }

        public async Task<ApiResponse> Update(Member member)
        {
            using(var client = Common.GetHttpClient())
            {
                var response = await client.PutAsync(Common.API_Member, member.ToJsonString());
                return Utility.GetHttpResponseMessage(response);
            }
        }

        public async Task<bool> Delete(List<int> idsToDelete)
        {
            using(var client = Common.GetHttpClient())
            {
                string ids = string.Join(",", idsToDelete.Select(n => n.ToString()).ToArray());
                var response = await client.PutAsync(Common.API_Member + "?idsToDelete=" + ids, ids.ToJsonString());

                return response.IsSuccessStatusCode;
            }
        }

    }
}

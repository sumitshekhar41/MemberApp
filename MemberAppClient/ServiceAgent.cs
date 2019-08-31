using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MemberAppClient.Wrapper;

namespace MemberAppClient
{
    public class ServiceAgent
    {
        private readonly MemberManagemetWrapper _memberManagementWrapper;

        public ServiceAgent() : this(new MemberManagemetWrapper())
        {
        }
        public ServiceAgent(MemberManagemetWrapper memberManagementWrapper)
        {
            _memberManagementWrapper = memberManagementWrapper;
        }

        public async Task<ApiResponse> Add(Member currentObject)
        {
            return await _memberManagementWrapper.Add(currentObject);
        }

        public async Task<ObservableCollection<Member>> GetAll()
        {
           var members = await  _memberManagementWrapper.GetMembers();

            var Members = new ObservableCollection<Member>();
            foreach(var item in members)
            {
                Members.Add(item);
            }
            return Members;
        }
    }
}

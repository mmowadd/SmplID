using Refit;
using SmplID.Derayah.Models.HCM;
using System;
using System.Text;
using System.Threading.Tasks;


namespace SmplID.Derayah.Data
{
    public class HCMData
    {
        private readonly IHCMData _hcmbData;
        public HCMData(string username, string password, string baseUrl)
        {
            _hcmbData = RestService.For<IHCMData>(baseUrl, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                Task.FromResult(Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password)))
            });
        }

        public async Task<Response<Employee>> GetEmployees() => await _hcmbData.GetEmployees();
        public async Task<Response<Department>> GetDepartments() => await _hcmbData.GetDepartments();
        public async Task<Response<Location>> GetLocations() => await _hcmbData.GetLocations();
        public async Task<Response<Position>> GetPositions() => await _hcmbData.GetPositions();
        public async Task<Response<User>> GetUsers() => await _hcmbData.GetUsers();
        public async Task<ScimResponse<Role>> GetRoles() => await _hcmbData.GetRoles();
    }
}

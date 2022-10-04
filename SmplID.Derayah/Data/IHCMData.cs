using Refit;
using SmplID.Derayah.Models.HCM;
using System.Threading.Tasks;

namespace SmplID.Derayah.Data
{

    [Headers("Authorization: Basic")]
    internal interface IHCMData
    {

        [Get(path: "/emps?limit=999999&&expand=assignments")]
        Task<Response<Employee>> GetEmployees();

        [Get(path: "/departmentsLov?fields=OrganizationId,Name")]
        Task<Response<Department>> GetDepartments();

        [Get(path: "/locations?fields=LocationId,LocationCode,LocationName,AddressLine1,Country,TownOrCity")]
        Task<Response<Location>> GetLocations();

        [Get(path: "/positions?fields=PositionId,PositionCode,Name")]
        Task<Response<Position>> GetPositions();

        [Get(path: "/userAccounts")]
        Task<Response<User>> GetUsers();

        [Get(path: "/Roles")]
        Task<ScimResponse<Role>> GetRoles();
    }
}

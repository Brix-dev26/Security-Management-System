using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;

namespace UI.DataConsumers
{
    public class SecurityStaffConsumer
    {
        private readonly ApiService _apiService;

        public SecurityStaffConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<SecurityStaffViewModel>> GetSecurityStaffById(int secId)
        {
            return await _apiService.httpClient.GetJsonAsync<SecurityStaffViewModel>($"api/SecurityStaff/GetSecurityStaffById/{secId}");
        }

        public async Task<APIReturn<SecurityStaffViewModel>> AddSecurityStaff(SecurityStaffViewModel securityStaff)
        {
            return await _apiService.httpClient.PostJsonAsync<SecurityStaffViewModel, SecurityStaffViewModel>("api/SecurityStaff/AddSecurityStaff", securityStaff);
        }

        public async Task<APIReturn<string>> Login(SecurityStaffLoginViewModel loginModel)
        {
            return await _apiService.httpClient.PostJsonAsync<string, SecurityStaffLoginViewModel>("api/SecurityStaff/Login", loginModel);
        }

        public async Task<APIReturn<bool>> UpdateGateBySecurityId(int secId, int gateId)
        {
            string url = $"api/SecurityStaff/UpdateGateBySecurityId/{secId}/{gateId}"; return await _apiService.httpClient.PostJsonAsync<bool, object>(url, null);
        }
    }
}

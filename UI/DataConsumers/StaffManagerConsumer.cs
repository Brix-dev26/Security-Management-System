using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;

namespace UI.DataConsumers
{
    public class StaffManagerConsumer
    {
        private readonly ApiService _apiService;

        public StaffManagerConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }
     
        public async Task<APIReturn<StaffManagerViewModel>> AddStaffManager(StaffManagerViewModel staffManager)
        {
            return await _apiService.httpClient.PostJsonAsync<StaffManagerViewModel, StaffManagerViewModel>("api/StaffManager/AddStaffManager", staffManager);
        }

        public async Task<APIReturn<string>> Login(StaffManagerViewModel loginModel)
        {
            return await _apiService.httpClient.PostJsonAsync<string, StaffManagerViewModel>("api/StaffManager/Login", loginModel);
        }

        public async Task<APIReturn<StaffManagerViewModel>> GetStaffManagerById(int staffManagerId)
        {
            return await _apiService.httpClient.GetJsonAsync<StaffManagerViewModel>($"api/StaffManager/GetStaffManagerById/{staffManagerId}");
        }
    }
}

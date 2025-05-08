using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UI.DataConsumers
{
    public class CampusConsumer
    {
        private readonly ApiService _apiService;

        public CampusConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<string>> AddCampus(CampusViewModel campus)
        {
            return await _apiService.httpClient.PostJsonAsync<string, CampusViewModel>(
                "api/Campus/AddCampus", campus);
        }

        public async Task<APIReturn<CampusViewModel>> GetCampusById(int campusId)
        {
            return await _apiService.httpClient.GetJsonAsync<CampusViewModel>(
                $"api/Campus/GetCampusById/{campusId}");
        }

        public async Task<APIReturn<List<CampusViewModel>>> GetAllCampuses()
        {
            return await _apiService.httpClient.GetJsonAsync<List<CampusViewModel>>(
                "api/Campus/GetAllCampuses");
        }
    }
}

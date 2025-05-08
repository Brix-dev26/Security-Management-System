using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;

namespace UI.DataConsumers
{
    public class GateConsumer
    {
        private readonly ApiService _apiService;

        public GateConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<string>> AddGate(GateViewModel gate)
        {
            return await _apiService.httpClient.PostJsonAsync<string, GateViewModel>(
                "api/Gate/AddGate", gate);
        }

        public async Task<APIReturn<GateViewModel>> GetGateById(int gateId)
        {
            return await _apiService.httpClient.GetJsonAsync<GateViewModel>(
                $"api/Gate/GetGateById/{gateId}");
        }

        public async Task<APIReturn<List<GateViewModel>>> GetGatesByCampusId(int campusId)
        {
            return await _apiService.httpClient.GetJsonAsync<List<GateViewModel>>(
              $"api/Gate/GetGatesByCampusId/{campusId}");
        }

    }
}

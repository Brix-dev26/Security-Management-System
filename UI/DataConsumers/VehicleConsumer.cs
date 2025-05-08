using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UI.DataConsumers
{
    public class VehicleConsumer
    {
        private readonly ApiService _apiService;

        public VehicleConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<string>> AddVehicle(VehicleViewModel vehicle)
        {
            return await _apiService.httpClient.PostJsonAsync<string, VehicleViewModel>(
                "api/Vehicle/AddVehicle", vehicle);
        }

        public async Task<APIReturn<List<VehicleViewModel>>> GetVehiclesByVisitorId(int visitorId)
        {
            return await _apiService.httpClient.GetJsonAsync<List<VehicleViewModel>>(
                $"api/Vehicle/GetVehiclesByVisitorId/{visitorId}");
        }

        public async Task<APIReturn<VehicleViewModel>> GetVehicleById(int vehicleId)
        {
            return await _apiService.httpClient.GetJsonAsync<VehicleViewModel>(
                $"api/Vehicle/GetVehicleById/{vehicleId}");
        }
    }
}

using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UI.DataConsumers
{
    public class EmergencyEventConsumer
    {
        private readonly ApiService _apiService;

        public EmergencyEventConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<EmergencyEventViewModel>> AddEmergencyEvent(EmergencyEventViewModel emergencyEvent)
        {
            return await _apiService.httpClient.PostJsonAsync<EmergencyEventViewModel, EmergencyEventViewModel>(
                "api/EmergencyEvent/AddEmergencyEvent", emergencyEvent);
        }

        public async Task<APIReturn<EmergencyEventViewModel>> GetEmergencyEventById(int emergencyEventId)
        {
            return await _apiService.httpClient.GetJsonAsync<EmergencyEventViewModel>(
                $"api/EmergencyEvent/GetEmergencyEventById/{emergencyEventId}");
        }

        public async Task<APIReturn<List<EmergencyEventViewModel>>> GetEmergencyEventsBySecurityStaffId(int securityStaffId)
        {
            return await _apiService.httpClient.GetJsonAsync<List<EmergencyEventViewModel>>(
                $"api/EmergencyEvent/GetEmergencyEventsBySecurityStaffId/{securityStaffId}");
        }
    }
}

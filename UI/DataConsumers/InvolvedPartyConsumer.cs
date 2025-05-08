using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UI.DataConsumers
{
    public class InvolvedPartyConsumer
    {
        private readonly ApiService _apiService;

        public InvolvedPartyConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<InvolvedPartyViewModel>> AddInvolvedParty(InvolvedPartyViewModel involvedParty)
        {
            return await _apiService.httpClient.PostJsonAsync<InvolvedPartyViewModel, InvolvedPartyViewModel>(
                "api/InvolvedParty/AddInvolvedParty", involvedParty);
        }

        public async Task<APIReturn<InvolvedPartyViewModel>> GetInvolvedPartyById(int involvedPartyId)
        {
            return await _apiService.httpClient.GetJsonAsync<InvolvedPartyViewModel>(
                $"api/InvolvedParty/GetInvolvedPartyById/{involvedPartyId}");
        }

        public async Task<APIReturn<List<InvolvedPartyViewModel>>> GetInvolvedPartiesByEmergencyId(int emergencyId)
        {
            return await _apiService.httpClient.GetJsonAsync<List<InvolvedPartyViewModel>>(
                $"api/InvolvedParty/GetInvolvedPartiesByEmergencyId/{emergencyId}");
        }

        public async Task<APIReturn<InvolvedPartyViewModel>> GetEmergencyIdsBySecurityStaffId(int secId)
        {
            return await _apiService.httpClient.GetJsonAsync<InvolvedPartyViewModel>(
                $"api/InvolvedParty/GetEmergencyIdsBySecurityStaffId/{secId}");
        }
    }
}

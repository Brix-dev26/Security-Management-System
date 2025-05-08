using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UI.DataConsumers
{
    public class VisitorConsumer
    {
        private readonly ApiService _apiService;

        public VisitorConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<string>> AddVisitor(VisitorViewModel visitor)
        {
            return await _apiService.httpClient.PostJsonAsync<string, VisitorViewModel>(
                "api/Visitor/AddVisitor", visitor);
        }

        public async Task<APIReturn<List<VisitorViewModel>>> GetVisitorsByPhoneNumber(string contactNumber)
        {
            return await _apiService.httpClient.GetJsonAsync<List<VisitorViewModel>>(
                $"api/Visitor/GetVisitorsByPhoneNumber/{contactNumber}");
        }

        public async Task<APIReturn<List<VisitorViewModel>>> GetVisitorsByNationalIdCard(string nationalIdCard)
        {
            return await _apiService.httpClient.GetJsonAsync<List<VisitorViewModel>>(
                $"api/Visitor/GetVisitorsByNationalIdCard/{nationalIdCard}");
        }

        public async Task<APIReturn<List<VisitorViewModel>>> GetVisitorsByPassportId(string passportId)
        {
            return await _apiService.httpClient.GetJsonAsync<List<VisitorViewModel>>(
                $"api/Visitor/GetVisitorsByPassportId/{passportId}");
        }

        public async Task<APIReturn<string>> UpdateVisitorById(VisitorViewModel visitor)
        {
            return await _apiService.httpClient.PostJsonAsync<string, VisitorViewModel>(
                "api/Visitor/UpdateVisitorById", visitor);
        }

        public async Task<APIReturn<VisitorViewModel>> GetVisitorById(int visitorId)
        {
            return await _apiService.httpClient.GetJsonAsync<VisitorViewModel>(
                $"api/Visitor/GetVisitorById/{visitorId}");
        }

        public async Task<APIReturn<List<VisitorViewModel>>> GetBlacklistedVisitors()
        {
            return await _apiService.httpClient.GetJsonAsync<List<VisitorViewModel>>(
                "api/Visitor/GetBlacklistedVisitors");
        }
    }
}

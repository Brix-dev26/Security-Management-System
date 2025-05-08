using Shared.UI;
using UI.Helpers;
using UI.HttpClientServices;
using ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UI.DataConsumers
{
    public class LogEntryConsumer
    {
        private readonly ApiService _apiService;

        public LogEntryConsumer(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<APIReturn<string>> AddLogEntry(LogEntryViewModel logEntry)
        {
            return await _apiService.httpClient.PostJsonAsync<string, LogEntryViewModel>(
                "api/LogEntry/AddLogEntry", logEntry);
        }


        public async Task<APIReturn<string>> UpdateExitTimeByLogId(int logEntryId, int exitGateId, int? secExitId)
        {
            return await _apiService.httpClient.PostJsonAsync<string, object>(
                $"api/LogEntry/UpdateExitTimeByLogId?logEntryId={logEntryId}&exitGateId={exitGateId}&secExitId={secExitId}", null);
        }

        public async Task<APIReturn<List<LogEntryViewModel>>> GetLogIdByVisitorId(int visitorId)
        {
            return await _apiService.httpClient.GetJsonAsync<List<LogEntryViewModel>>(
                $"api/LogEntry/GetLogIdByVisitorId/{visitorId}");
        }

        public async Task<APIReturn<List<LogEntryViewModel>>> GetLogHistoryByDateRange(DateTime fromDate, DateTime toDate, int campusId, int? gateId)
        {
            return await _apiService.httpClient.GetJsonAsync<List<LogEntryViewModel>>(
                $"api/LogEntry/GetLogHistoryByDateRange?fromDate={fromDate:yyyy-MM-ddTHH:mm:ss}&toDate={toDate:yyyy-MM-ddTHH:mm:ss}&campusId={campusId}&gateId={gateId}");
        }




    }
}

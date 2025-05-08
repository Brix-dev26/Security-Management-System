using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;
using Shared.Domain;

namespace Domain.IServices
{
    public interface ILogEntryService
    {
        public Task<ServiceReturn<LogEntryViewModel>> AddLogEntry(LogEntryViewModel logEntryViewModel);
        public Task<ServiceReturn<List<LogEntryViewModel>>> GetLogEntriesByVisitorId(int visitorId);
        public Task<ServiceReturn<string>> UpdateExitTimeByLogId(int logEntryId, int? exitGateId, int? secExitId); 
        public Task<ServiceReturn<List<LogEntryViewModel>>> GetLogIdByVisitorId(int visitorId);
        public Task<ServiceReturn<List<LogEntryViewModel>>> GetLogHistoryByDateRange(DateTime fromDate, DateTime toDate, int campusId, int? gateId = null);

    }
}
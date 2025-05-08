using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModel;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogEntryController : ControllerBase
    {
        private readonly ILogger<LogEntryController> _logger;
        private readonly ILogEntryService _logEntryService;

        public LogEntryController(ILogger<LogEntryController> logger, ILogEntryService logEntryService)
        {
            _logger = logger;
            _logEntryService = logEntryService;
        }

        [Authorize(Roles = "Computer Security Officer")]
        [HttpPost("AddLogEntry")]
        public async Task<ServiceReturn<string>> AddLogEntry([FromBody] LogEntryViewModel logEntryViewModel)
        {
            var apiReturn = new ServiceReturn<string>();

            try
            {
                var result = await _logEntryService.AddLogEntry(logEntryViewModel);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    apiReturn.Data = "Log entry successfully added.";
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while adding the log entry: {ex.Message}");
                _logger.LogError($"An error occurred while adding the log entry: {ex.Message}");
            }

            return apiReturn;
        }


        [HttpGet("GetLogEntriesByVisitorId")]
        public async Task<ServiceReturn<List<LogEntryViewModel>>> GetLogEntriesByVisitorId()
        {
            var apiReturn = new ServiceReturn<List<LogEntryViewModel>>();

            try
            {
                var visitorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "VisitorId")?.Value;
                if (string.IsNullOrEmpty(visitorIdClaim) || !int.TryParse(visitorIdClaim, out int visitorId))
                {
                    apiReturn.AddError("Visitor ID is not present in the token.");
                    return apiReturn;
                }

                var result = await _logEntryService.GetLogEntriesByVisitorId(visitorId);
                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    apiReturn.Data = result.Data;
                    apiReturn.Count = result.Count;
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving log entries: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving log entries: {ex.Message}");
            }
            return apiReturn;
        }


        [HttpPost("UpdateExitTimeByLogId")]
        public async Task<ServiceReturn<string>> UpdateExitTimeByLogId([FromQuery] int logEntryId, [FromQuery] int? exitGateId, [FromQuery] int? secExitId)
        {
            var apiReturn = new ServiceReturn<string>();

            try
            {
                if (logEntryId <= 0)
                {
                    apiReturn.AddError("Log entry ID is required and must be a valid number.");
                    return apiReturn;
                }

                var result = await _logEntryService.UpdateExitTimeByLogId(logEntryId, exitGateId, secExitId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Count;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while updating the exit time: {ex.Message}");
                _logger.LogError($"An error occurred while updating the exit time: {ex.Message}");
            }

            return apiReturn;
        }


        [HttpGet("GetLogIdByVisitorId/{visitorId}")]
        public async Task<ServiceReturn<List<LogEntryViewModel>>> GetLogIdByVisitorId(int visitorId)
        {
            var apiReturn = new ServiceReturn<List<LogEntryViewModel>>();

            try
            {
                var result = await _logEntryService.GetLogIdByVisitorId(visitorId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while searching for log entries by visitor ID: {ex.Message}");
                _logger.LogError($"An error occurred while searching for log entries by visitor ID: {ex.Message}");
            }

            return apiReturn;
        }



        [HttpGet("GetLogHistoryByDateRange")]
        public async Task<ServiceReturn<List<LogEntryViewModel>>> GetLogHistoryByDateRange([FromQuery] DateTime fromDate,[FromQuery] DateTime toDate,[FromQuery] int campusId,[FromQuery] int? gateId = null){
            var apiReturn = new ServiceReturn<List<LogEntryViewModel>>();

            try
            {
                var result = await _logEntryService.GetLogHistoryByDateRange(fromDate, toDate, campusId, gateId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Count;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving log history statistics: {ex.Message}");
                _logger.LogError($"Error in GetLogHistoryByDateRange: {ex.Message}");
            }

            return apiReturn;
        }
    }
}

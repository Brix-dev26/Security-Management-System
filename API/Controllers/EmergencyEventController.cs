using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmergencyEventController : ControllerBase
    {
        private readonly ILogger<EmergencyEventController> _logger;
        private readonly IEmergencyEventService _emergencyEventService;

        public EmergencyEventController(ILogger<EmergencyEventController> logger, IEmergencyEventService emergencyEventService)
        {
            _logger = logger;
            _emergencyEventService = emergencyEventService;
        }

        [Authorize(Roles = "Computer Security Officer")]
        [HttpPost("AddEmergencyEvent")]
        public async Task<ServiceReturn<EmergencyEventViewModel>> AddEmergencyEvent(EmergencyEventViewModel emergencyEventViewModel)
        {
            var apiReturn = new ServiceReturn<EmergencyEventViewModel>();

            try
            {
                var result = await _emergencyEventService.AddEmergencyEvent(emergencyEventViewModel);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while adding the emergency event: {ex.Message}");
                _logger.LogError($"An error occurred while adding the emergency event: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetEmergencyEventById/{emergencyEventId}")]
        public async Task<ServiceReturn<EmergencyEventViewModel>> GetEmergencyEventById(int emergencyEventId)
        {
            var apiReturn = new ServiceReturn<EmergencyEventViewModel>();

            try
            {
                if (emergencyEventId <= 0)
                {
                    apiReturn.AddError("Invalid emergency event ID.");
                    return apiReturn;
                }

                var result = await _emergencyEventService.GetEmergencyEventById(emergencyEventId);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving the emergency event data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving the emergency event data: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetEmergencyEventsBySecurityStaffId/{securityStaffId}")]
        public async Task<ServiceReturn<List<EmergencyEventViewModel>>> GetEmergencyEventsBySecurityStaffId(int securityStaffId)
        {
            var apiReturn = new ServiceReturn<List<EmergencyEventViewModel>>();

            try
            {
                if (securityStaffId <= 0)
                {
                    apiReturn.AddError("Invalid security staff ID.");
                    return apiReturn;
                }

                var result = await _emergencyEventService.GetEmergencyEventsBySecurityStaffId(securityStaffId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving emergency events: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving emergency events: {ex.Message}");
            }

            return apiReturn;
        }
    }
}

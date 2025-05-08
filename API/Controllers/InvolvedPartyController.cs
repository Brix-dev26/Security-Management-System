using Domain.IServices;
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
    public class InvolvedPartyController : ControllerBase
    {
        private readonly ILogger<InvolvedPartyController> _logger;
        private readonly IInvolvedPartyService _involvedPartyService;

        public InvolvedPartyController(ILogger<InvolvedPartyController> logger, IInvolvedPartyService involvedPartyService)
        {
            _logger = logger;
            _involvedPartyService = involvedPartyService;
        }

        [HttpPost("AddInvolvedParty")]
        public async Task<ServiceReturn<InvolvedPartyViewModel>> AddInvolvedParty(InvolvedPartyViewModel involvedPartyViewModel)
        {
            var apiReturn = new ServiceReturn<InvolvedPartyViewModel>();

            try
            {
                var result = await _involvedPartyService.AddInvolvedParty(involvedPartyViewModel);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while adding the involved party: {ex.Message}");
                _logger.LogError($"An error occurred while adding the involved party: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetInvolvedPartyById/{involvedPartyId}")]
        public async Task<ServiceReturn<InvolvedPartyViewModel>> GetInvolvedPartyById(int involvedPartyId)
        {
            var apiReturn = new ServiceReturn<InvolvedPartyViewModel>();

            try
            {
                var result = await _involvedPartyService.GetInvolvedPartyById(involvedPartyId);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving involved party data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving involved party data: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetInvolvedPartiesByEmergencyId/{emergencyId}")]
        public async Task<ServiceReturn<List<InvolvedPartyViewModel>>> GetInvolvedPartiesByEmergencyId(int emergencyId)
        {
            var apiReturn = new ServiceReturn<List<InvolvedPartyViewModel>>();

            try
            {
                var result = await _involvedPartyService.GetInvolvedPartiesByEmergencyId(emergencyId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving involved parties data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving involved parties data: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetEmergencyIdsBySecurityStaffId/{secId}")]
        public async Task<ServiceReturn<InvolvedPartyViewModel>> GetEmergencyIdsBySecurityStaffId(int secId)
        {
            var apiReturn = new ServiceReturn<InvolvedPartyViewModel>();

            try
            {
                var result = await _involvedPartyService.GetEmergencyIdsBySecurityStaffId(secId);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving emergency event IDs: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving emergency event IDs: {ex.Message}");
            }

            return apiReturn;
        }
    }
}

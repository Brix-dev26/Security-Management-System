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
    public class GateController : ControllerBase
    {
        private readonly ILogger<GateController> _logger;
        private readonly IGateService _gateService;

        public GateController(ILogger<GateController> logger, IGateService gateService)
        {
            _logger = logger;
            _gateService = gateService;
        }

        [HttpPost("AddGate")]
        public async Task<ServiceReturn<GateViewModel>> AddGate(GateViewModel gateViewModel)
        {
            var apiReturn = new ServiceReturn<GateViewModel>();

            try
            {
                var result = await _gateService.AddGate(gateViewModel);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while adding the gate: {ex.Message}");
                _logger.LogError($"An error occurred while adding the gate: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetGateById/{gateId}")]
        public async Task<ServiceReturn<GateViewModel>> GetGateById(int gateId)
        {
            var apiReturn = new ServiceReturn<GateViewModel>();

            try
            {
                if (gateId <= 0)
                {
                    apiReturn.AddError("Invalid gate ID.");
                    return apiReturn;
                }

                var result = await _gateService.GetGateById(gateId);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving gate data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving gate data: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetGatesByCampusId/{campusId}")]
        public async Task<ServiceReturn<List<GateViewModel>>> GetGatesByCampusId(int campusId)
        {
            var apiReturn = new ServiceReturn<List<GateViewModel>>();

            try
            {
                if (campusId <= 0)
                {
                    apiReturn.AddError("Campus ID is required and must be a valid number.");
                    return apiReturn;
                }

                var result = await _gateService.GetGatesByCampusId(campusId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving gate data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving gate data: {ex.Message}");
            }

            return apiReturn;
        }
    }
}

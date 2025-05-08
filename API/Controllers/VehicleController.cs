using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly IConfiguration _configuration;

        public VehicleController(
            ILogger<VehicleController> logger,
            IVehicleService vehicleService,
            IConfiguration configuration)
        {
            _logger = logger;
            _vehicleService = vehicleService;
            _configuration = configuration;
        }

        [HttpPost("AddVehicle")]
        public async Task<ServiceReturn<string>> AddVehicle([FromBody] VehicleViewModel vehicleViewModel)
        {
            var apiReturn = new ServiceReturn<string>();

            try
            {
                var visitorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "VisitorId")?.Value;
                if (!string.IsNullOrEmpty(visitorIdClaim) && int.TryParse(visitorIdClaim, out int visitorId))
                {
                    vehicleViewModel.VisitorId = visitorId;
                }

                if (vehicleViewModel.VisitorId == 0 && string.IsNullOrEmpty(vehicleViewModel.PhoneNumber))
                {
                    apiReturn.AddError("Either VisitorId or PhoneNumber must be provided.");
                    return apiReturn;
                }

                var result = await _vehicleService.AddVehicle(vehicleViewModel);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    apiReturn.Data = "Vehicle added successfully.";
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error adding vehicle: {ex.Message}");
                _logger.LogError($"Error adding vehicle: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetVehiclesByVisitorId/{visitorId}")]
        public async Task<ServiceReturn<List<VehicleViewModel>>> GetVehiclesByVisitorId(int visitorId)
        {
            var apiReturn = new ServiceReturn<List<VehicleViewModel>>();

            try
            {
                var result = await _vehicleService.GetVehiclesByVisitorId(visitorId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error retrieving vehicle data: {ex.Message}");
                _logger.LogError($"Error retrieving vehicle data: {ex.Message}");
            }

            return apiReturn;
        }
    }
}

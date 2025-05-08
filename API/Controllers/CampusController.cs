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
    public class CampusController : ControllerBase
    {
        private readonly ILogger<CampusController> _logger;
        private readonly ICampusService _campusService;

        public CampusController(ILogger<CampusController> logger, ICampusService campusService)
        {
            _logger = logger;
            _campusService = campusService;
        }

        [HttpPost("AddCampus")]
        public async Task<ServiceReturn<CampusViewModel>> AddCampus(CampusViewModel campusViewModel)
        {
            var apiReturn = new ServiceReturn<CampusViewModel>();

            try
            {
                var result = await _campusService.AddCampus(campusViewModel);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while adding the campus: {ex.Message}");
                _logger.LogError($"Error while adding the campus: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetCampusById/{campusId}")]
        public async Task<ServiceReturn<CampusViewModel>> GetCampusById(int campusId)
        {
            var apiReturn = new ServiceReturn<CampusViewModel>();

            try
            {
                if (campusId <= 0)
                {
                    apiReturn.AddError("Invalid campus ID.");
                    return apiReturn;
                }

                var result = await _campusService.GetCampusById(campusId);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving the campus data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving the campus data: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetAllCampuses")]
        public async Task<ServiceReturn<List<CampusViewModel>>> GetAllCampuses()
        {
            var apiReturn = new ServiceReturn<List<CampusViewModel>>();

            try
            {
                var result = await _campusService.GetAllCampuses();
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while retrieving all campuses: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving all campuses: {ex.Message}");
            }

            return apiReturn;
        }
    }
}

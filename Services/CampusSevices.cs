using Microsoft.Extensions.Logging;
using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using Infrastructure;
using Domain.Entities;
using Domain.IServices;

namespace Services
{
    public class CampusService : ICampusService
    {
        private readonly IRepository<Campus> _campusRepository;
        private readonly ILogger<CampusService> _logger;

        public CampusService(IRepository<Campus> campusRepository, ILogger<CampusService> logger)
        {
            _campusRepository = campusRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<CampusViewModel>> AddCampus(CampusViewModel campusViewModel)
        {
            var serviceReturn = new ServiceReturn<CampusViewModel>();

            try
            {
                var existingCampus = (await _campusRepository.FindBy(c => c.campus_name == campusViewModel.CampusName)).FirstOrDefault();
                if (existingCampus != null)
                {
                    serviceReturn.AddError("Campus name is already registered.");
                    return serviceReturn;
                }

                var campus = new Campus
                {
                    campus_name = campusViewModel.CampusName,
                    CampusLocation = campusViewModel.CampusLocation
                };

                await _campusRepository.Insert(campus);
                serviceReturn.Data = campusViewModel;
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"Error occurred while adding the campus: {ex.Message}");
                _logger.LogError($"Error occurred while adding the campus: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<CampusViewModel>> GetCampusById(int campusId)
        {
            var serviceReturn = new ServiceReturn<CampusViewModel>();

            try
            {
                var campus = (await _campusRepository.FindBy(c => c.CampusId == campusId)).FirstOrDefault();

                if (campus == null)
                {
                    serviceReturn.AddError("Campus not found.");
                    return serviceReturn;
                }

                serviceReturn.Data = new CampusViewModel
                {
                    CampusId = campus.CampusId,
                    CampusName = campus.campus_name,
                    CampusLocation = campus.CampusLocation
                };
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"Error occurred while retrieving the campus data: {ex.Message}");
                _logger.LogError($"Error occurred while retrieving the campus data: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<CampusViewModel>>> GetAllCampuses()
        {
            var serviceReturn = new ServiceReturn<List<CampusViewModel>>();

            try
            {
                var campuses = await _campusRepository.FindBy(c => true);

                if (campuses == null || !campuses.Any())
                {
                    serviceReturn.AddError("No campus data found.");
                    return serviceReturn;
                }

                serviceReturn.Data = campuses.Select(c => new CampusViewModel
                {
                    CampusId = c.CampusId,
                    CampusName = c.campus_name,
                    CampusLocation = c.CampusLocation
                }).ToList();
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"Error occurred while retrieving all campuses: {ex.Message}");
                _logger.LogError($"Error occurred while retrieving all campuses: {ex.Message}");
            }

            return serviceReturn;
        }
    }
}

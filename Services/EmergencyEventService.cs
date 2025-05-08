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
    public class EmergencyEventService : IEmergencyEventService
    {
        private readonly IRepository<EmergencyEvent> _emergencyEventRepository;
        private readonly IRepository<SecurityStaff> _securityStaffRepository;
        private readonly ILogger<EmergencyEventService> _logger;

        public EmergencyEventService(IRepository<EmergencyEvent> emergencyEventRepository, IRepository<SecurityStaff> securityStaffRepository, ILogger<EmergencyEventService> logger)
        {
            _emergencyEventRepository = emergencyEventRepository;
            _securityStaffRepository = securityStaffRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<EmergencyEventViewModel>> AddEmergencyEvent(EmergencyEventViewModel emergencyEventViewModel)
        {
            var serviceReturn = new ServiceReturn<EmergencyEventViewModel>();

            try
            {
                var securityStaffExists = (await _securityStaffRepository.FindBy(s => s.Sec_ID == emergencyEventViewModel.Sec_ID)).FirstOrDefault();
                if (securityStaffExists == null)
                {
                    serviceReturn.AddError("Security staff not found.");
                    return serviceReturn;
                }

                var emergencyEvent = new EmergencyEvent
                {
                    Sec_ID = emergencyEventViewModel.Sec_ID,
                    ReportDate = emergencyEventViewModel.ReportDate,
                    Location = emergencyEventViewModel.Location,
                    ActionTaken = emergencyEventViewModel.ActionTaken,
                    ResolutionStatus = emergencyEventViewModel.ResolutionStatus,
                    CauseDescription = emergencyEventViewModel.CauseDescription,
                    NumberOfInvolvedPeople = emergencyEventViewModel.NumberOfInvolvedPeople,
                    ResolutionMethod = emergencyEventViewModel.ResolutionMethod,
                    Secutity_names = emergencyEventViewModel.Secutity_names,
                    involvedpeople_names = emergencyEventViewModel.involvedpeople_names,
                    CampusId = emergencyEventViewModel.CampusId,
                };

                await _emergencyEventRepository.Insert(emergencyEvent);
                serviceReturn.Data = emergencyEventViewModel;
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"Error occurred while adding the emergency event: {ex.Message}");
                _logger.LogError($"Error occurred while adding the emergency event: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<EmergencyEventViewModel>> GetEmergencyEventById(int emergencyEventId)
        {
            var serviceReturn = new ServiceReturn<EmergencyEventViewModel>();

            try
            {
                var emergencyEvent = (await _emergencyEventRepository.FindBy(e => e.Emerg_ID == emergencyEventId)).FirstOrDefault();

                if (emergencyEvent == null)
                {
                    serviceReturn.AddError("Emergency event not found.");
                    return serviceReturn;
                }

                serviceReturn.Data = new EmergencyEventViewModel
                {
                    Emerg_ID = emergencyEvent.Emerg_ID,
                    Sec_ID = emergencyEvent.Sec_ID,
                    ReportDate = emergencyEvent.ReportDate,
                    Location = emergencyEvent.Location,
                    ActionTaken = emergencyEvent.ActionTaken,
                    ResolutionStatus = emergencyEvent.ResolutionStatus,
                    CauseDescription = emergencyEvent.CauseDescription,
                    NumberOfInvolvedPeople = emergencyEvent.NumberOfInvolvedPeople,
                    ResolutionMethod = emergencyEvent.ResolutionMethod,
                    Secutity_names = emergencyEvent.Secutity_names,
                    involvedpeople_names = emergencyEvent.involvedpeople_names
                };
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"Error occurred while retrieving the emergency event data: {ex.Message}");
                _logger.LogError($"Error occurred while retrieving the emergency event data: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<EmergencyEventViewModel>>> GetEmergencyEventsBySecurityStaffId(int securityStaffId)
        {
            var serviceReturn = new ServiceReturn<List<EmergencyEventViewModel>>();
            try
            {
                var emergencyEvents = await _emergencyEventRepository.FindBy(e => e.Sec_ID == securityStaffId);

                if (emergencyEvents.Any())
                {
                    serviceReturn.Data = emergencyEvents.Select(e => new EmergencyEventViewModel
                    {
                        Emerg_ID = e.Emerg_ID,
                        Sec_ID = e.Sec_ID,
                        ReportDate = e.ReportDate,
                        Location = e.Location,
                        ActionTaken = e.ActionTaken,
                        ResolutionStatus = e.ResolutionStatus,
                        CauseDescription = e.CauseDescription,
                        NumberOfInvolvedPeople = e.NumberOfInvolvedPeople,
                        ResolutionMethod = e.ResolutionMethod,
                        Secutity_names = e.Secutity_names,
                        involvedpeople_names = e.involvedpeople_names
                    }).ToList();
                    serviceReturn.Count = emergencyEvents.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No emergency events found for this security staff.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"Error occurred while retrieving emergency events: {ex.Message}");
                _logger.LogError($"Error occurred while retrieving emergency events: {ex.Message}");
            }
            return serviceReturn;
        }
    }
}

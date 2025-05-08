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
    public class InvolvedPartyService : IInvolvedPartyService
    {
        private readonly IRepository<InvolvedParty> _involvedPartyRepository;
        private readonly IRepository<EmergencyEvent> _emergencyEventRepository;
        private readonly IRepository<SecurityStaff> _securityStaffRepository;
        private readonly ILogger<InvolvedPartyService> _logger;

        public InvolvedPartyService(IRepository<InvolvedParty> involvedPartyRepository, IRepository<EmergencyEvent> emergencyEventRepository, IRepository<SecurityStaff> securityStaffRepository, ILogger<InvolvedPartyService> logger)
        {
            _involvedPartyRepository = involvedPartyRepository;
            _emergencyEventRepository = emergencyEventRepository;
            _securityStaffRepository = securityStaffRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<InvolvedPartyViewModel>> AddInvolvedParty(InvolvedPartyViewModel involvedPartyViewModel)
        {
            var serviceReturn = new ServiceReturn<InvolvedPartyViewModel>();

            try
            {
                var emergencyEventExists = (await _emergencyEventRepository.FindBy(e => e.Emerg_ID == involvedPartyViewModel.Emerg_ID)).FirstOrDefault();
                if (emergencyEventExists == null)
                {
                    serviceReturn.AddError("Emergency event not found.");
                    return serviceReturn;
                }

                var involvedParty = new InvolvedParty
                {
                    PersonalId = involvedPartyViewModel.PersonalId,
                    Emerg_ID = involvedPartyViewModel.Emerg_ID,
                    PersonName = involvedPartyViewModel.PersonName,
                    Role = involvedPartyViewModel.Role,
                    Sec_ID = involvedPartyViewModel.Sec_ID
                };

                await _involvedPartyRepository.Insert(involvedParty);
                serviceReturn.Data = involvedPartyViewModel;
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while adding the involved party: {ex.Message}");
                _logger.LogError($"An error occurred while adding the involved party: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<InvolvedPartyViewModel>> GetInvolvedPartyById(int involvedPartyId)
        {
            var serviceReturn = new ServiceReturn<InvolvedPartyViewModel>();

            try
            {
                var involvedParty = (await _involvedPartyRepository.FindBy(i => i.InvolvedPartyId == involvedPartyId)).FirstOrDefault();

                if (involvedParty == null)
                {
                    serviceReturn.AddError("Involved party not found.");
                    return serviceReturn;
                }

                serviceReturn.Data = new InvolvedPartyViewModel
                {
                    InvolvedPartyId = involvedParty.InvolvedPartyId,
                    PersonalId = involvedParty.PersonalId,
                    Emerg_ID = involvedParty.Emerg_ID,
                    PersonName = involvedParty.PersonName,
                    Role = involvedParty.Role,
                    Sec_ID = involvedParty.Sec_ID
                };
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving the involved party data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving the involved party data: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<InvolvedPartyViewModel>>> GetInvolvedPartiesByEmergencyId(int emergencyId)
        {
            var serviceReturn = new ServiceReturn<List<InvolvedPartyViewModel>>();
            try
            {
                var involvedParties = await _involvedPartyRepository.FindBy(i => i.Emerg_ID == emergencyId);

                if (involvedParties.Any())
                {
                    serviceReturn.Data = involvedParties.Select(i => new InvolvedPartyViewModel
                    {
                        InvolvedPartyId = i.InvolvedPartyId,
                        PersonalId = i.PersonalId,
                        Emerg_ID = i.Emerg_ID,
                        PersonName = i.PersonName,
                        Role = i.Role,
                        Sec_ID = i.Sec_ID
                    }).ToList();
                    serviceReturn.Count = involvedParties.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No involved parties found for this emergency event.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving the involved party data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving the involved party data: {ex.Message}");
            }
            return serviceReturn;
        }

        public async Task<ServiceReturn<InvolvedPartyViewModel>> GetEmergencyIdsBySecurityStaffId(int secId)
        {
            var serviceReturn = new ServiceReturn<InvolvedPartyViewModel>();
            try
            {
                var involvedParty = (await _involvedPartyRepository.FindBy(i => i.Sec_ID == secId)).FirstOrDefault();

                if (involvedParty == null)
                {
                    serviceReturn.AddError("No emergency events found for the specified security staff.");
                    return serviceReturn;
                }

                serviceReturn.Data = new InvolvedPartyViewModel
                {
                    InvolvedPartyId = involvedParty.InvolvedPartyId,
                    PersonalId = involvedParty.PersonalId,
                    Emerg_ID = involvedParty.Emerg_ID,
                    PersonName = involvedParty.PersonName,
                    Role = involvedParty.Role,
                    Sec_ID = involvedParty.Sec_ID
                };
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving emergency event IDs: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving emergency event IDs: {ex.Message}");
            }
            return serviceReturn;
        }
    }
}

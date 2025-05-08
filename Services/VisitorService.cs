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
    public class VisitorService : IVisitorService
    {
        private readonly IRepository<Visitor> _visitorRepository;
        private readonly ILogger<VisitorService> _logger;

        public VisitorService(IRepository<Visitor> visitorRepository, ILogger<VisitorService> logger)
        {
            _visitorRepository = visitorRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<VisitorViewModel>> AddVisitor(VisitorViewModel visitorViewModel)
        {
            var serviceReturn = new ServiceReturn<VisitorViewModel>();

            try
            {
                if (string.IsNullOrEmpty(visitorViewModel.passport_no) && !visitorViewModel.NationalIdCard.HasValue)
                {
                    serviceReturn.AddError("You must provide either the national ID or the passport number.");
                    return serviceReturn;
                }

                if (!string.IsNullOrEmpty(visitorViewModel.passport_no))
                {
                    // Validation for passport number can be re-enabled if required
                    var existingPassportVisitor = (await _visitorRepository.FindBy(v => v.passport_no == visitorViewModel.passport_no)).FirstOrDefault();
                    if (existingPassportVisitor != null)
                    {
                        serviceReturn.AddError("Passport number is already registered for another visitor.");
                        return serviceReturn;
                    }
                }

                if (visitorViewModel.NationalIdCard.HasValue)
                {
                    string nationalIdString = visitorViewModel.NationalIdCard.Value.ToString();
                    if (nationalIdString.Length != 14)
                    {
                        serviceReturn.AddError("National ID must be exactly 14 digits.");
                        return serviceReturn;
                    }

                    var existingNationalIdVisitor = (await _visitorRepository.FindBy(v => v.NationalIdCard == visitorViewModel.NationalIdCard.Value)).FirstOrDefault();
                    if (existingNationalIdVisitor != null)
                    {
                        serviceReturn.AddError("National ID is already registered for another visitor.");
                        return serviceReturn;
                    }
                }

                if (!string.IsNullOrEmpty(visitorViewModel.phone_no))
                {
                    if (!visitorViewModel.phone_no.All(char.IsDigit) || visitorViewModel.phone_no.Length != 11)
                    {
                        serviceReturn.AddError("Phone number must be exactly 11 digits.");
                        return serviceReturn;
                    }

                    var existingPhoneVisitor = (await _visitorRepository.FindBy(v => v.phone_no == visitorViewModel.phone_no)).FirstOrDefault();
                    if (existingPhoneVisitor != null)
                    {
                        serviceReturn.AddError("Phone number is already registered for another visitor.");
                        return serviceReturn;
                    }
                }

                if (visitorViewModel.NationalIdCard.HasValue && string.IsNullOrEmpty(visitorViewModel.nationality))
                {
                    visitorViewModel.nationality = "Egyptian";
                }

                var maxVisitorId = (int)(await _visitorRepository.Max(v => v.VisitorId, 0) ?? 0);
                visitorViewModel.VisitorId = maxVisitorId + 1;

                var visitor = new Visitor
                {
                    phone_no = visitorViewModel.phone_no,
                    name = visitorViewModel.name,
                    E_mail = visitorViewModel.E_mail,
                    nationality = visitorViewModel.nationality,
                    passport_no = visitorViewModel.passport_no,
                    NationalIdCard = visitorViewModel.NationalIdCard,
                    IsBlacklisted = false,
                    notes = visitorViewModel.notes,
                };

                await _visitorRepository.Insert(visitor);
                serviceReturn.Data = visitorViewModel;
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while adding the visitor: {ex.Message}");
                _logger.LogError($"Error while adding visitor: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByPhoneNumber(string contactNumber)
        {
            var serviceReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                var visitors = await _visitorRepository.FindBy(v => v.phone_no == contactNumber);

                if (visitors.Any())
                {
                    serviceReturn.Data = visitors.Select(v => new VisitorViewModel
                    {
                        VisitorId = v.VisitorId,
                        name = v.name,
                        E_mail = v.E_mail,
                        phone_no = v.phone_no,
                        passport_no = v.passport_no,
                        nationality = v.nationality,
                        NationalIdCard = v.NationalIdCard,
                        IsBlacklisted = v.IsBlacklisted,
                        notes = v.notes
                    }).ToList();
                    serviceReturn.Count = visitors.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No visitor found with the specified phone number.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while searching for visitors by phone number: {ex.Message}");
                _logger.LogError($"Error retrieving visitors by phone number: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByNationalIdCard(string nationalIdCard)
        {
            var serviceReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                if (!long.TryParse(nationalIdCard, out long parsedNationalId))
                {
                    serviceReturn.AddError("National ID must be a valid number.");
                    return serviceReturn;
                }

                var visitors = await _visitorRepository.FindBy(v => v.NationalIdCard == parsedNationalId);

                if (visitors.Any())
                {
                    serviceReturn.Data = visitors.Select(v => new VisitorViewModel
                    {
                        VisitorId = v.VisitorId,
                        name = v.name,
                        E_mail = v.E_mail,
                        phone_no = v.phone_no,
                        passport_no = v.passport_no,
                        nationality = v.nationality,
                        NationalIdCard = v.NationalIdCard,
                        IsBlacklisted = v.IsBlacklisted,
                        notes = v.notes
                    }).ToList();
                    serviceReturn.Count = visitors.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No visitor found with the specified national ID.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while searching for visitors by national ID: {ex.Message}");
                _logger.LogError($"Error retrieving visitors by national ID: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByPassportId(string passportId)
        {
            var serviceReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                var visitors = await _visitorRepository.FindBy(v => v.passport_no == passportId);

                if (visitors.Any())
                {
                    serviceReturn.Data = visitors.Select(v => new VisitorViewModel
                    {
                        VisitorId = v.VisitorId,
                        name = v.name,
                        E_mail = v.E_mail,
                        phone_no = v.phone_no,
                        passport_no = v.passport_no,
                        nationality = v.nationality,
                        NationalIdCard = v.NationalIdCard,
                        IsBlacklisted = v.IsBlacklisted,
                        notes = v.notes
                    }).ToList();
                    serviceReturn.Count = visitors.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No visitor found with the specified passport number.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while searching for visitors by passport number: {ex.Message}");
                _logger.LogError($"Error retrieving visitors by passport number: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<string>> UpdateVisitorById(VisitorViewModel visitorViewModel)
        {
            var serviceReturn = new ServiceReturn<string>();

            try
            {
                var existingVisitor = (await _visitorRepository.FindBy(v => v.VisitorId == visitorViewModel.VisitorId)).FirstOrDefault();

                if (existingVisitor == null)
                {
                    serviceReturn.AddError("Visitor not found.");
                    return serviceReturn;
                }

                existingVisitor.name = visitorViewModel.name;
                existingVisitor.phone_no = visitorViewModel.phone_no;
                existingVisitor.NationalIdCard = visitorViewModel.NationalIdCard;
                existingVisitor.passport_no = visitorViewModel.passport_no;
                existingVisitor.E_mail = visitorViewModel.E_mail;
                existingVisitor.nationality = visitorViewModel.nationality;

                if (visitorViewModel.IsBlacklisted.HasValue)
                    existingVisitor.IsBlacklisted = visitorViewModel.IsBlacklisted.Value;

                if (visitorViewModel.notes != null)
                    existingVisitor.notes = visitorViewModel.notes;

                await _visitorRepository.Update(existingVisitor);

                _logger.LogInformation($"Visitor {existingVisitor.VisitorId} updated successfully.");

                serviceReturn.Data = "Visitor updated successfully.";
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while updating the visitor: {ex.Message}");
                _logger.LogError($"Error while updating visitor: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<VisitorViewModel>> GetVisitorById(int visitorId)
        {
            var serviceReturn = new ServiceReturn<VisitorViewModel>();

            try
            {
                var Visitor = (await _visitorRepository.FindBy(v => v.VisitorId == visitorId)).FirstOrDefault();

                if (Visitor != null)
                {
                    serviceReturn.Data = new VisitorViewModel
                    {
                        VisitorId = Visitor.VisitorId,
                        name = Visitor.name,
                        E_mail = Visitor.E_mail,
                        phone_no = Visitor.phone_no,
                        passport_no = Visitor.passport_no,
                        nationality = Visitor.nationality,
                        NationalIdCard = Visitor.NationalIdCard,
                        IsBlacklisted = Visitor.IsBlacklisted,
                        notes = Visitor.notes
                    };
                }
                else
                {
                    serviceReturn.AddError("Visitor not found.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving visitor information: {ex.Message}");
                _logger.LogError($"Error retrieving visitor information: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<VisitorViewModel>>> GetBlacklistedVisitors()
        {
            var serviceReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                var blacklistedVisitors = await _visitorRepository.FindBy(v => v.IsBlacklisted == true);

                serviceReturn.Data = blacklistedVisitors.Select(v => new VisitorViewModel
                {
                    VisitorId = v.VisitorId,
                    name = v.name,
                    E_mail = v.E_mail,
                    phone_no = v.phone_no,
                    passport_no = v.passport_no,
                    nationality = v.nationality,
                    NationalIdCard = v.NationalIdCard,
                    IsBlacklisted = v.IsBlacklisted,
                    notes = v.notes
                }).ToList();
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving blacklisted visitors: {ex.Message}");
                _logger.LogError($"Error retrieving blacklisted visitors: {ex.Message}");
            }

            return serviceReturn;
        }
    }
}

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
    public class GateService : IGateService
    {
        private readonly IRepository<Gate> _gateRepository;
        private readonly IRepository<Campus> _campusRepository;
        private readonly ILogger<GateService> _logger;

        public GateService(IRepository<Gate> gateRepository, IRepository<Campus> campusRepository, ILogger<GateService> logger)
        {
            _gateRepository = gateRepository;
            _campusRepository = campusRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<GateViewModel>> AddGate(GateViewModel gateViewModel)
        {
            var serviceReturn = new ServiceReturn<GateViewModel>();

            try
            {
                var existingGate = (await _gateRepository.FindBy(g => g.gate_name == gateViewModel.GateName)).FirstOrDefault();
                if (existingGate != null)
                {
                    serviceReturn.AddError("Gate name is already registered.");
                    return serviceReturn;
                }

                var campusExists = (await _campusRepository.FindBy(c => c.CampusId == gateViewModel.CampusId)).FirstOrDefault();
                if (campusExists == null)
                {
                    serviceReturn.AddError("Campus not found.");
                    return serviceReturn;
                }

                var gate = new Gate
                {
                    gate_name = gateViewModel.GateName,
                    CampusId = gateViewModel.CampusId
                };

                await _gateRepository.Insert(gate);
                serviceReturn.Data = gateViewModel;
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while adding the gate: {ex.Message}");
                _logger.LogError($"An error occurred while adding the gate: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<GateViewModel>> GetGateById(int gateId)
        {
            var serviceReturn = new ServiceReturn<GateViewModel>();

            try
            {
                var gate = (await _gateRepository.FindBy(g => g.GateId == gateId)).FirstOrDefault();

                if (gate == null)
                {
                    serviceReturn.AddError("Gate not found.");
                    return serviceReturn;
                }

                var campus = (await _campusRepository.FindBy(c => c.CampusId == gate.CampusId)).FirstOrDefault();

                serviceReturn.Data = new GateViewModel
                {
                    GateId = gate.GateId,
                    GateName = gate.gate_name,
                    CampusId = gate.CampusId,
                };
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving the gate data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving the gate data: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<GateViewModel>>> GetGatesByCampusId(int campusId)
        {
            var serviceReturn = new ServiceReturn<List<GateViewModel>>();
            try
            {
                var gates = await _gateRepository.FindBy(g => g.CampusId == campusId);

                if (gates.Any())
                {
                    serviceReturn.Data = gates.Select(g => new GateViewModel
                    {
                        GateId = g.GateId,
                        GateName = g.gate_name,
                        CampusId = g.CampusId
                    }).ToList();
                    serviceReturn.Count = gates.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No gates found for this campus.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving gate data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving gate data: {ex.Message}");
            }
            return serviceReturn;
        }
    }
}

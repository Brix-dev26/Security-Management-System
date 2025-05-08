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
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<Visitor> _visitorRepository;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(
            IRepository<Vehicle> vehicleRepository,
            IRepository<Visitor> visitorRepository,
            ILogger<VehicleService> logger)
        {
            _vehicleRepository = vehicleRepository;
            _visitorRepository = visitorRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<VehicleViewModel>> AddVehicle(VehicleViewModel vehicleViewModel)
        {
            var serviceReturn = new ServiceReturn<VehicleViewModel>();

            try
            {
                var existingVisitor = (await _visitorRepository.FindBy(v => v.VisitorId == vehicleViewModel.VisitorId)).FirstOrDefault();
                if (existingVisitor == null)
                {
                    serviceReturn.AddError("The visitor does not exist.");
                    return serviceReturn;
                }

                var existingPlate = (await _vehicleRepository.FindBy(v => v.Plate == vehicleViewModel.Plate)).FirstOrDefault();
                if (existingPlate != null)
                {
                    serviceReturn.AddError("The plate number is already registered.");
                    return serviceReturn;
                }

                var vehicle = new Vehicle
                {
                    VisitorId = vehicleViewModel.VisitorId,
                    Plate = vehicleViewModel.Plate,
                    Type = vehicleViewModel.Type
                };

                await _vehicleRepository.Insert(vehicle);
                serviceReturn.Data = vehicleViewModel;
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while adding the vehicle: {ex.Message}");
                _logger.LogError($"An error occurred while adding the vehicle: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<VehicleViewModel>>> GetVehiclesByVisitorId(int visitorId)
        {
            var serviceReturn = new ServiceReturn<List<VehicleViewModel>>();
            try
            {
                var vehicles = await _vehicleRepository.FindBy(v => v.VisitorId == visitorId);

                if (vehicles.Any())
                {
                    serviceReturn.Data = vehicles.Select(v => new VehicleViewModel
                    {
                        VehicleId = v.VehicleId,
                        VisitorId = v.VisitorId,
                        Type = v.Type,
                        Plate = v.Plate,
                    }).ToList();
                    serviceReturn.Count = vehicles.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No vehicles were found for this visitor.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving vehicle data: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving vehicle data: {ex.Message}");
            }
            return serviceReturn;
        }
    }
}

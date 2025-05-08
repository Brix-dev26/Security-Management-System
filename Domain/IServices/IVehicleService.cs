using Shared.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface IVehicleService
    {
        public Task<ServiceReturn<VehicleViewModel>> AddVehicle(VehicleViewModel vehicleViewModel); 
        public Task<ServiceReturn<List<VehicleViewModel>>> GetVehiclesByVisitorId(int VisitorId);
        
    }
}

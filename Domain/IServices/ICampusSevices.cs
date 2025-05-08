using Shared.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface ICampusService
    {
        public Task<ServiceReturn<CampusViewModel>> AddCampus(CampusViewModel campusViewModel);
        public Task<ServiceReturn<CampusViewModel>> GetCampusById(int campusId);
        public Task<ServiceReturn<List<CampusViewModel>>> GetAllCampuses();
    }
}

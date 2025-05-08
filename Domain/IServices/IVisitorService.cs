using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface IVisitorService
    {
        public Task<ServiceReturn<VisitorViewModel>> AddVisitor(VisitorViewModel visitorViewModel);
        public Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByPhoneNumber(string contactNumber);
        public Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByNationalIdCard(string nationalIdCard);
        public Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByPassportId(string passportId);
        public Task<ServiceReturn<string>> UpdateVisitorById(VisitorViewModel visitorViewModel);
        public Task<ServiceReturn<VisitorViewModel>> GetVisitorById(int visitorId);
        public Task<ServiceReturn<List<VisitorViewModel>>> GetBlacklistedVisitors();


    }
}
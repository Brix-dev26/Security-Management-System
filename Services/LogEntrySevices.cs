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
    public class LogEntryService : ILogEntryService
    {
        private readonly IRepository<LogEntry> _logEntryRepository;
        private readonly IRepository<Visitor> _visitorRepository;
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<SecurityStaff> _securityStaffRepository;
        private readonly IRepository<Gate> _gateRepository;
        private readonly IRepository<Campus> _campusRepository;
        private readonly ILogger<LogEntryService> _logger;

        public LogEntryService(
            IRepository<LogEntry> logEntryRepository,
            IRepository<Visitor> visitorRepository,
            IRepository<Vehicle> vehicleRepository,
            IRepository<SecurityStaff> securityStaffRepository,
            IRepository<Gate> gateRepository,
            IRepository<Campus> campusRepository,
            ILogger<LogEntryService> logger)
        {
            _logEntryRepository = logEntryRepository;
            _visitorRepository = visitorRepository;
            _vehicleRepository = vehicleRepository;
            _securityStaffRepository = securityStaffRepository;
            _gateRepository = gateRepository;
            _campusRepository = campusRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<LogEntryViewModel>> AddLogEntry(LogEntryViewModel logEntryViewModel)
        {
            var serviceReturn = new ServiceReturn<LogEntryViewModel>();

            try
            {
                var visitors = await _visitorRepository.FindBy(v => v.VisitorId == logEntryViewModel.VisitorId);
                var visitor = visitors.FirstOrDefault();

                if (visitor == null)
                {
                    serviceReturn.AddError("The visitor does not exist.");
                    return serviceReturn;
                }

                if (visitor.IsBlacklisted)
                {
                    serviceReturn.AddError("This visitor is not allowed to enter.");
                    return serviceReturn;
                }

                var logEntry = new LogEntry
                {
                    VisitorId = logEntryViewModel.VisitorId,
                    VehicleId = logEntryViewModel.VehicleId,
                    Sec_ID = logEntryViewModel.Sec_ID,
                    EntryTime = logEntryViewModel.EntryTime,
                    ExitTime = logEntryViewModel.ExitTime,
                    Visit_reason = logEntryViewModel.Visit_reason,
                    GateId = logEntryViewModel.GateId,
                    CampusId = logEntryViewModel.CampusId,
                    GateId_exit = logEntryViewModel.GateId_exit ?? null,
                    Sec_ID_Exit = logEntryViewModel.Sec_ID_Exit ?? null
                };

                await _logEntryRepository.Insert(logEntry);

                serviceReturn.Data = logEntryViewModel;
                serviceReturn.SuccessMessage = "Log entry added successfully.";
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while adding the log entry: {ex.Message}");
                _logger.LogError($"An error occurred while adding the log entry: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<LogEntryViewModel>>> GetLogEntriesByVisitorId(int visitorId)
        {
            var serviceReturn = new ServiceReturn<List<LogEntryViewModel>>();

            try
            {
                var logEntries = (await _logEntryRepository.FindBy(l => l.VisitorId == visitorId)).ToList();

                if (!logEntries.Any())
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No log entries found for this visitor.");
                    return serviceReturn;
                }

                var logEntryViewModels = new List<LogEntryViewModel>();

                foreach (var log in logEntries)
                {
                    var campus = (await _campusRepository.FindBy(c => c.CampusId == log.CampusId)).FirstOrDefault();
                    var entryGate = (await _gateRepository.FindBy(g => g.GateId == log.GateId)).FirstOrDefault();
                    var exitGate = (await _gateRepository.FindBy(g => g.GateId == log.GateId_exit)).FirstOrDefault();

                    if (campus == null || entryGate == null)
                    {
                        continue;
                    }

                    logEntryViewModels.Add(new LogEntryViewModel
                    {
                        LogId = log.LogId,
                        VisitorId = log.VisitorId,
                        VehicleId = log.VehicleId ?? null,
                        Sec_ID = log.Sec_ID,
                        Sec_ID_Exit = log.Sec_ID_Exit,
                        EntryTime = log.EntryTime,
                        ExitTime = log.ExitTime,
                        Visit_reason = log.Visit_reason,
                        GateId = entryGate.GateId,
                        CampusId = campus.CampusId,
                        GateId_exit = exitGate?.GateId ?? 0
                    });
                }

                serviceReturn.Data = logEntryViewModels;
                serviceReturn.Count = logEntryViewModels.Count;
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving the log entries: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving the log entries: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<string>> UpdateExitTimeByLogId(int logEntryId, int? exitGateId, int? secExitId)
        {
            var serviceReturn = new ServiceReturn<string>();

            try
            {
                var logEntry = (await _logEntryRepository.FindBy(l => l.LogId == logEntryId)).FirstOrDefault();

                if (logEntry == null)
                {
                    serviceReturn.AddError("Log entry not found.");
                    return serviceReturn;
                }

                var latestLog = (await _logEntryRepository.FindBy(l => l.VisitorId == logEntry.VisitorId))
                                  .OrderByDescending(l => l.EntryTime)
                                  .FirstOrDefault();

                if (latestLog == null || latestLog.LogId != logEntryId)
                {
                    serviceReturn.AddError("You can only log or update exit for the most recent entry.");
                    return serviceReturn;
                }

                logEntry.ExitTime = DateTime.UtcNow;
                logEntry.GateId_exit = exitGateId;
                logEntry.Sec_ID_Exit = secExitId;

                _logger.LogInformation($"Exit time updated for log entry {logEntryId} at {logEntry.ExitTime} via gate {exitGateId} by security staff {secExitId}");

                await _logEntryRepository.Update(logEntry);

                serviceReturn.Data = "Exit time, gate, and security staff updated successfully.";
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while updating the exit time: {ex.Message}");
                _logger.LogError($"An error occurred while updating the exit time: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<LogEntryViewModel>>> GetLogIdByVisitorId(int visitorId)
        {
            var serviceReturn = new ServiceReturn<List<LogEntryViewModel>>();

            try
            {
                var logEntries = await _logEntryRepository.FindBy(l => l.VisitorId == visitorId);

                if (logEntries.Any())
                {
                    serviceReturn.Data = logEntries.Select(l => new LogEntryViewModel
                    {
                        LogId = l.LogId,
                        VisitorId = l.VisitorId,
                        VehicleId = l.VehicleId,
                        EntryTime = l.EntryTime,
                        ExitTime = l.ExitTime,
                        Visit_reason = l.Visit_reason,
                        CampusId = l.CampusId,
                        GateId = l.GateId,
                        GateId_exit = l.GateId_exit,
                        Sec_ID_Exit = l.Sec_ID_Exit,
                        Sec_ID = l.Sec_ID,
                    }).ToList();
                    serviceReturn.Count = logEntries.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No log entries found for this visitor.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while retrieving the log entries: {ex.Message}");
                _logger.LogError($"An error occurred while retrieving the log entries: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<List<LogEntryViewModel>>> GetLogHistoryByDateRange(DateTime fromDate, DateTime toDate, int campusId, int? gateId = null)
        {
            var serviceReturn = new ServiceReturn<List<LogEntryViewModel>>();

            try
            {
                toDate = toDate.Date.AddDays(1).AddTicks(-1);

                var logEntries = await _logEntryRepository.FindBy(
                    l => l.EntryTime >= fromDate
                         && l.EntryTime <= toDate
                         && l.CampusId == campusId
                         && (gateId == null || l.GateId == gateId)
                );

                if (logEntries.Any())
                {
                    serviceReturn.Data = logEntries.Select(l => new LogEntryViewModel
                    {
                        LogId = l.LogId,
                        VisitorId = l.VisitorId,
                        VehicleId = l.VehicleId,
                        EntryTime = l.EntryTime,
                        ExitTime = l.ExitTime,
                        Visit_reason = l.Visit_reason,
                        CampusId = l.CampusId,
                        GateId = l.GateId,
                        GateId_exit = l.GateId_exit,
                        Sec_ID_Exit = l.Sec_ID_Exit,
                        Sec_ID = l.Sec_ID,
                    }).ToList();

                    serviceReturn.Count = logEntries.Count();
                }
                else
                {
                    serviceReturn.Count = 0;
                    serviceReturn.AddError("No records found in the specified date range.");
                }
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"An error occurred while fetching the data: {ex.Message}");
                _logger.LogError($"Error in GetLogHistoryByDateRange: {ex.Message}");
            }

            return serviceReturn;
        }
    }
}

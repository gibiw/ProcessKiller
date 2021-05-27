using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Infrastructure.Interfaces;

namespace UserCases
{
    public class ProcessManages
    {
        private readonly ILogger _logger;
        private readonly IProcessService _processService;
        private readonly ProcessManagerConfig _processManagerConfig;

        public ProcessManages(ILogger logger, IProcessService processService, ProcessManagerConfig processManagerConfig)
        {
            _logger = logger;
            _processService = processService;
            _processManagerConfig = processManagerConfig;
        }

        public void Execute()
        {
            _logger.LogInfo("Start program");

            try
            {
                while (true)
                {
                    var processes = GetProcesses();

                    if (processes.Count == 0)
                    {
                        _logger.LogInfo($"Processes with name {_processManagerConfig.ProcessName} not found");
                    }
                    
                    foreach (var process in processes)
                    {
                        _logger.LogInfo($"Checking process {process.Name}:{process.Id}");

                        if (NeedToKillProcess(process))
                        {
                            KillProcess(process);
                        }
                    }

                    Task.Delay(TimeSpan.FromMinutes(_processManagerConfig.CheckInterval)).Wait();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
            }
        }

        private ICollection<Process> GetProcesses()
        {
            _logger.LogInfo($"Getting process with name {_processManagerConfig.ProcessName}");

            var processDto = _processService.GetProcessByName(_processManagerConfig.ProcessName);

            var processes = processDto
                .Select(p => p.ConvertToModel())
                .ToList();

            return processes;
        }

        private bool NeedToKillProcess(Process process)
        {
            return DateTime.Now - process.StartTime > TimeSpan.FromMinutes(_processManagerConfig.TimeToLive);
        }

        private void KillProcess(Process process)
        {
            _logger.LogInfo($"Killing process {process.Name}:{process.Id}");

            _processService.KillProcess(process.ConvertToDto());
        }
    }
}
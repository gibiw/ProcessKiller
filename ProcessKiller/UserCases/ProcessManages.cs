using System;
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
                var process = GetProcess();

                do
                {
                    _logger.LogInfo("Checking process");

                    if (!CheckProcessExist(process)) return;

                    if (NeedToKillProcess(process))
                    {
                        KillProcess(process);
                    }

                    Task.Delay(TimeSpan.FromMinutes(_processManagerConfig.CheckInterval)).Wait();
                } while (CheckProcessExist(process));
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
            }
        }

        private bool CheckProcessExist(Process process)
        {
            if (_processService.IsProcessExist(process.ConvertToDto()))
            {
                return true;
            }

            _logger.LogInfo("Process not exists");
            
            return false;
        }

        private Process GetProcess()
        {
            _logger.LogInfo($"Getting process with name {_processManagerConfig.ProcessName}");

            var processDto = _processService.GetProcessByName(_processManagerConfig.ProcessName);

            return processDto.ConvertToModel();
        }

        private bool NeedToKillProcess(Process process)
        {
            return DateTime.Now - process.StartTimeOfMonitoring > TimeSpan.FromMinutes(_processManagerConfig.TimeToLive);
        }

        private void KillProcess(Process process)
        {
            _logger.LogInfo($"Killing process {process.Name}:{process.Id}");

            var proc = new ProcessDto(process.Name, process.Id);

            _processService.KillProcess(proc);
        }
    }
}
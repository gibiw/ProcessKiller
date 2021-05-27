using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Infrastructure.Interfaces;

namespace Infrastructure.Implementation
{
    public class WindowsProcessService : IProcessService
    {
        public ICollection<ProcessDto> GetProcessByName(string name)
        {
            var processes = Process.GetProcessesByName(name);

            var processesDto = processes
                .Select(p => new ProcessDto(p.ProcessName, p.Id, p.StartTime))
                .ToList();

            return processesDto;
        }

        public void KillProcess(ProcessDto process)
        {
            var processForKill = Process.GetProcessesByName(process.Name)
                .FirstOrDefault(p => p.Id == process.Id);

            if (processForKill == null)
            {
                throw new ApplicationException("Process not found!");
            }

            processForKill.Kill();
        }
    }
}
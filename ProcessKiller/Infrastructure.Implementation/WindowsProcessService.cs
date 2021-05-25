using System;
using System.Diagnostics;
using System.Linq;
using Infrastructure.Interfaces;

namespace Infrastructure.Implementation
{
    public class WindowsProcessService : IProcessService
    {
        public ProcessDto GetProcessByName(string name)
        {
            var process = Process.GetProcessesByName(name)
                .FirstOrDefault();

            if (process == null)
            {
                throw new ApplicationException("Process not found!");
            }

            return new ProcessDto(process.ProcessName, process.Id);
        }

        public bool IsProcessExist(ProcessDto process)
        {
            var proc = Process.GetProcessesByName(process.Name)
                .FirstOrDefault(p => p.Id == process.Id);

            return proc != null;
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
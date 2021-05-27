using System.Collections.Generic;
using Infrastructure.Interfaces;

namespace Infrastructure.Implementation
{
    public class LinuxProcessService : IProcessService
    {
        public ICollection<ProcessDto> GetProcessByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void KillProcess(ProcessDto process)
        {
            throw new System.NotImplementedException();
        }
    }
}
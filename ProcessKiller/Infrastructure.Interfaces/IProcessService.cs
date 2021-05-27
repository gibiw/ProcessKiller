using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IProcessService
    {
        ICollection<ProcessDto> GetProcessByName(string name);
        void KillProcess(ProcessDto process);
    }
}
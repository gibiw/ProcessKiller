namespace Infrastructure.Interfaces
{
    public interface IProcessService
    {
        ProcessDto GetProcessByName(string name);
        bool IsProcessExist(ProcessDto process);
        void KillProcess(ProcessDto process);
    }
}
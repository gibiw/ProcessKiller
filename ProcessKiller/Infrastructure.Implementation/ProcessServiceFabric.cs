using System.Runtime.InteropServices;
using Infrastructure.Interfaces;

namespace Infrastructure.Implementation
{
    public static class ProcessServiceFabric
    {
        public static IProcessService GetProcessService()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                return new WindowsProcessService();
            }

            return new LinuxProcessService();
        }
    }
}
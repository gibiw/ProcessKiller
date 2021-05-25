using System;
using Infrastructure.Implementation;
using UserCases;

namespace ProcessKiller
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ParseArgument(args);
            var logger = new Logger();
            var processService = ProcessServiceFabric.GetProcessService();
            var processManager = new ProcessManages(logger, processService, config);

            processManager.Execute();
        }

        static ProcessManagerConfig ParseArgument(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ApplicationException("You need to enter 3 parameters: process name, time to live and check interval");
            }

            if (!int.TryParse(args[1], out var timeToLive))
            {
                throw new ApplicationException("Argument 2 must be a number");
            }

            if (!int.TryParse(args[2], out var checkInterval))
            {
                throw new ApplicationException("Argument 3 must be a number");
            }

            return new ProcessManagerConfig
            {
                ProcessName = args[0],
                TimeToLive = timeToLive,
                CheckInterval = checkInterval
            };
        }
    }
}
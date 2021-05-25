using System;
using Infrastructure.Interfaces;

namespace Infrastructure.Implementation
{
    public class Logger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"[ERR] {DateTime.Now}: {message}");
        }
    }
}
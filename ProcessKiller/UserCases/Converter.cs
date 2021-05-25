using System;
using Entities;
using Infrastructure.Interfaces;

namespace UserCases
{
    public static class Converter
    {
        public static Process ConvertToModel(this ProcessDto processDto)
        {
            return new Process()
            {
                Name = processDto.Name,
                Id = processDto.Id,
                StartTimeOfMonitoring = DateTime.Now
            };
        } 
        
        public static ProcessDto ConvertToDto(this Process process)
        {
            return new ProcessDto(process.Name, process.Id);
        } 
    }
}
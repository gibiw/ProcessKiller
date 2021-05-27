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
                StartTime = processDto.StartTime
            };
        }

        public static ProcessDto ConvertToDto(this Process process)
        {
            return new ProcessDto(process.Name, process.Id, process.StartTime);
        }
    }
}
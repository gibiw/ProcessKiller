using System;

namespace Infrastructure.Interfaces
{
    public record ProcessDto(string Name, int Id, DateTime StartTime);
}
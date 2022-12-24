using ApplicationTier.DTO;
using DataAccessTier.Models;

namespace ApplicationTier.Mapping;

public static class EmployeeMapping
{
    public static EmployeeDto AsDto(this Employee employee)
        => new EmployeeDto(
            employee.Id,
            employee.Name,
            employee.LedEmployees.Select(x => x.AsDto()).ToArray(),
            employee.Messages.Select(x => x.AsDto()).ToArray());
}
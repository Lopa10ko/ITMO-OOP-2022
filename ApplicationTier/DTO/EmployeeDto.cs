using DataAccessTier.Models;

namespace ApplicationTier.DTO;

public record EmployeeDto(Guid Id, string Name, IReadOnlyCollection<EmployeeDto> LedEmployees, IReadOnlyCollection<MessageDto> Messages);
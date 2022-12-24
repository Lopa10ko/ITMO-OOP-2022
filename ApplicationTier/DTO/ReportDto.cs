using DataAccessTier.Models;

namespace ApplicationTier.DTO;

public record ReportDto(Guid Id, Guid EmployeeId, DateTime FormedDate, string FullText);
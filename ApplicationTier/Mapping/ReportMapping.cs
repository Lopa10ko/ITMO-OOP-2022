using ApplicationTier.DTO;
using DataAccessTier.Models;

namespace ApplicationTier.Mapping;

public static class ReportMapping
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(
            report.Id,
            report.Employee.Id,
            report.FormedDate,
            report.FullText);
}
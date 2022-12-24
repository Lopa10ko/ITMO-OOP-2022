using ApplicationTier.DTO;

namespace ApplicationTier.Services;

public interface IReport
{
    Task<ReportDto> GetReportAsync(Guid employeeId, DateTime spanStart, DateTime spanEnd, CancellationToken cancellationToken);
    Task<ICollection<ReportDto>> GetAllReportsAsync(Guid employeeId, CancellationToken cancellationToken);
}
using ApplicationTier.DTO;
using ApplicationTier.Extensions;
using ApplicationTier.Mapping;
using DataAccessTier;
using DataAccessTier.Models;

namespace ApplicationTier.Services.Implementations;

internal class ReportService : IReport
{
    private readonly DatabaseContext _context;

    public ReportService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> GetReportAsync(Guid employeeId, DateTime spanStart, DateTime spanEnd, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetEntityAsync(employeeId, cancellationToken);
        string reportString = $"Report (from {spanStart} to {spanEnd}) for {employee.Id}\n" +
                              $"Quantity of subordinate employees: {employee.LedEmployees.Count}\n" +
                              $"Total handled messages: {employee.LedEmployees.Sum(e => e.Messages.Count(m => IsInRange(m.HandledTime, spanStart, spanEnd) && m.State.Equals(MessageState.Checked)))}\n" +
                              $"Total messages: {employee.LedEmployees.Sum(e => e.Messages.Count)}";
        var report = new Report(Guid.NewGuid(), DateTime.Now, employee, reportString);
        _context.Reports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);
        return report.AsDto();
    }

    public async Task<ICollection<ReportDto>> GetAllReportsAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetEntityAsync(employeeId, cancellationToken);
        return _context.Reports
            .ToArray()
            .Select(r => r.AsDto())
            .Where(dto => dto.EmployeeId.Equals(employee.Id))
            .ToArray();
    }

    private static bool IsInRange(DateTime observedTime, DateTime spanStart, DateTime spanEnd)
        => observedTime >= spanStart && observedTime <= spanEnd;
}
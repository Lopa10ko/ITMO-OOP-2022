using ApplicationTier.DTO;
using ApplicationTier.Extensions;
using ApplicationTier.Mapping;
using DataAccessTier;
using DataAccessTier.Models;

namespace ApplicationTier.Services.Implementations;

internal class EmployeeService : IEmployee
{
    private readonly DatabaseContext _context;

    public EmployeeService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(string name, CancellationToken cancellationToken)
    {
        var employee = new Employee(Guid.NewGuid(), name);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return employee.AsDto();
    }

    public async Task CheckMessageAsync(Guid employeeId, Guid messageId, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetEntityAsync(employeeId, cancellationToken);
        var message = await _context.Messages.GetEntityAsync(messageId, cancellationToken);
        if (!employee.Messages.Contains(message))
            throw new Exception("this message is not subscribed to person");
        message.State = MessageState.Checked;
        message.HandledTime = DateTime.Now;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddLedEmployeeAsync(Guid id, Guid bossId, CancellationToken cancellationToken)
    {
        var boss = await _context.Employees.GetEntityAsync(bossId, cancellationToken);
        var employee = await _context.Employees.GetEntityAsync(id, cancellationToken);
        boss.LedEmployees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> GetReportAsync(Guid employeeId, DateTime spanStart, DateTime spanEnd, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetEntityAsync(employeeId, cancellationToken);
        return $"Report (from {spanStart} to {spanEnd}) for {employee.Id}\n" +
               $"Quantity of subordinate employees: {employee.LedEmployees.Count}\n" +
               $"Total handled messages: {employee.LedEmployees.Sum(e => e.Messages.Count(m => IsInRange(m.HandledTime, spanStart, spanEnd) && m.State.Equals(MessageState.Checked)))}\n" +
               $"Total messages: {employee.LedEmployees.Sum(e => e.Messages.Count(m => IsInRange(m.HandledTime, spanStart, spanEnd)))}";
    }

    private static bool IsInRange(DateTime observedTime, DateTime spanStart, DateTime spanEnd)
        => observedTime >= spanStart && observedTime <= spanEnd;
}
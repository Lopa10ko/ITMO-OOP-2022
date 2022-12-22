using ApplicationTier.DTO;
using ApplicationTier.Extensions;
using ApplicationTier.Mapping;
using DataAccessTier;
using DataAccessTier.Models;

namespace ApplicationTier.Services.Implementations;

internal class MessageService : IMessage
{
    private readonly DatabaseContext _context;

    public MessageService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> CreateMessageAsync(string text, Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetEntityAsync(employeeId, cancellationToken);
        var message = new Message(Guid.NewGuid(), employee, text);
        employee.Messages.Add(message);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.AsDto();
    }
}
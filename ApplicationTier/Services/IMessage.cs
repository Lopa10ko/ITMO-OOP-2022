using ApplicationTier.DTO;
using DataAccessTier.Models;

namespace ApplicationTier.Services;

public interface IMessage
{
    Task<MessageDto> CreateMessageAsync(string text, Guid employeeId, CancellationToken cancellationToken);
}
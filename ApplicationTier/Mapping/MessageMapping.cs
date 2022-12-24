using ApplicationTier.DTO;
using DataAccessTier.Models;

namespace ApplicationTier.Mapping;

public static class MessageMapping
{
    public static MessageDto AsDto(this Message message)
        => new MessageDto(
            message.Id,
            message.Employee.Id,
            message.Text,
            message.State,
            message.HandledTime);
}
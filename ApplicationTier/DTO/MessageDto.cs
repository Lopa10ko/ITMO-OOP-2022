using DataAccessTier.Models;

namespace ApplicationTier.DTO;

public record MessageDto(Guid Id, Guid EmployeeId, string Text, MessageState State, DateTime HandledTime);
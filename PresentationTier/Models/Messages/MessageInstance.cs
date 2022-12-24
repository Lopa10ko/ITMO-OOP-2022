using DataAccessTier.Models;

namespace PresentationTier.Models.Messages;

public record MessageInstance(Guid Id, Guid EmployeeId, MessageState State, DateTime HandledTime);
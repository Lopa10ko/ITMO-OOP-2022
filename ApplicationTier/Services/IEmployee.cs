﻿using ApplicationTier.DTO;

namespace ApplicationTier.Services;

public interface IEmployee
{
    Task<EmployeeDto> CreateEmployeeAsync(string name, CancellationToken cancellationToken);
    Task CheckMessageAsync(Guid employeeId, Guid messageId, CancellationToken cancellationToken);
    Task AddLedEmployeeAsync(Guid id, Guid bossId, CancellationToken cancellationToken);
    Task<ICollection<KeyValuePair<string, Guid>>> GetAllEmployeeGuids();
}
using Banks.Accounts;

namespace Banks.Models;

public interface IClient
{
    bool IsVerified { get; }
    Guid Id { get; }
    string Name { get; }
    string LastName { get; }
    string Address { get; }
    string Passport { get; }
    void AddAccount(IAccount account);
}
using Backups.Visitors;

namespace Backups.RepositoryItems;

public interface IRepositoryItem
{
    void Accept(IRepositoryVisitor visitor);
}
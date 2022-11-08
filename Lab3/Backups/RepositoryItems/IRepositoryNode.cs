using Backups.Visitors;

namespace Backups.RepositoryItems;

public interface IRepositoryNode : IRepositoryItem
{
    void Accept(IRepositoryVisitor visitor);
}

public class FileSystemDirectory : IRepositoryNode
{
    public void Accept(IRepositoryVisitor visitor)
    {
        visitor.Visit(this);
    }
}
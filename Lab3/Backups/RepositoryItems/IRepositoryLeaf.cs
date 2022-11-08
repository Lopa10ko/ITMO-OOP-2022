using Backups.Visitors;

namespace Backups.RepositoryItems;

public interface IRepositoryLeaf : IRepositoryItem
{
    void Accept(IRepositoryVisitor visitor);
}

public class FileSystemItem : IRepositoryLeaf
{
    public void Accept(IRepositoryVisitor visitor)
    {
        visitor.Visit(this);
    }
}
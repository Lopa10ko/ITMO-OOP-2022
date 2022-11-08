using Backups.RepositoryItems;
using Backups.Services;

namespace Backups.Visitors;

public interface IRepositoryVisitor
{
    void Visit(IRepositoryNode node);
    void Visit(IRepositoryLeaf leaf);
}

public class FileSystemVisitor : IRepositoryVisitor
{
    public void Visit(IRepositoryNode node)
    {
        throw new NotImplementedException();
    }

    public void Visit(IRepositoryLeaf leaf)
    {
        throw new NotImplementedException();
    }
}
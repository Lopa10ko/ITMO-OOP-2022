using Backups.Visitors;

namespace Backups.RepositoryItems;

public class FileSystemNode : IRepositoryNode
{
    private readonly Func<IReadOnlyCollection<IRepositoryItem>> _fileCollection;
    private readonly string _relativePath;
    private readonly string _fullPath;

    public FileSystemNode(string relativePath, string fullPath, Func<IReadOnlyCollection<IRepositoryItem>> fileCollection)
    {
        _relativePath = relativePath;
        _fullPath = fullPath;
        _fileCollection = fileCollection;
    }

    public void Accept(IRepositoryVisitor visitor)
    {
        visitor.Visit(this);
    }

    public string GetNodeId()
        => $"{_relativePath}.zip";

    public string GetNodeRelativePath()
        => _relativePath;

    public string GetSourceId()
        => _fullPath;

    public IEnumerable<IRepositoryItem> GetItems()
        => _fileCollection();
}
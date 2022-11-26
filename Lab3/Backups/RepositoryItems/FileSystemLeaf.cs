using Backups.Visitors;

namespace Backups.RepositoryItems;

public class FileSystemLeaf : IRepositoryLeaf
{
    private readonly Func<Stream> _stream;
    private readonly string _fileName;
    private readonly string _fullPath;

    public FileSystemLeaf(string fileName, string fullPath, Func<Stream> stream)
    {
        _fileName = fileName;
        _fullPath = fullPath;
        _stream = stream;
    }

    public Stream GetCurrentStream() => _stream.Invoke();
    public void Accept(IRepositoryVisitor visitor)
    {
        visitor.Visit(this);
    }

    public string GetLeafId() => _fileName;

    public string GetSourceId() => _fullPath;
}
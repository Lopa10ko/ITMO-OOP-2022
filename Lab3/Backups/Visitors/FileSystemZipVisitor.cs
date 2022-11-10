using System.IO.Compression;
using Backups.RepositoryItems;
using Backups.ZipArchivedItems;

namespace Backups.Visitors;

public class FileSystemZipVisitor : IRepositoryVisitor
{
    private readonly Stack<List<IZipArchivedItem>> _zipArchivedItems;
    private readonly Stack<ZipArchive> _archives;

    public FileSystemZipVisitor(ZipArchive archive)
    {
        _archives = new Stack<ZipArchive>();
        _archives.Push(archive);
        _zipArchivedItems = new Stack<List<IZipArchivedItem>>();
        _zipArchivedItems.Push(new List<IZipArchivedItem>());
    }

    public void Visit(IRepositoryNode node)
    {
        using Stream stream = _archives.Peek().CreateEntry(node.GetNodeId()).Open();
        using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create);
        _archives.Push(zipArchive);
        _zipArchivedItems.Push(new List<IZipArchivedItem>());
        foreach (IRepositoryItem repositoryItem in node.GetItems())
        {
            repositoryItem.Accept(this);
        }

        _zipArchivedItems.Peek().Add(new ZipArchivedNode(node.GetNodeRelativePath(), _zipArchivedItems.Pop()));
        _archives.Pop();
    }

    public void Visit(IRepositoryLeaf leaf)
    {
        ZipArchiveEntry leafEntry = _archives.Peek().CreateEntry(leaf.GetLeafId());
        using Stream leafStream = leafEntry.Open();
        leaf.GetCurrentStream().CopyTo(leafStream);
        _zipArchivedItems.Peek().Add(new ZipArchivedFile(leaf.GetLeafId(), leafEntry));
    }

    public IEnumerable<IZipArchivedItem> GetLastOnStack()
        => _zipArchivedItems.Peek();
}
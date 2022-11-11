using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.ZipArchivedItems;

public class ZipArchivedNode : IZipArchivedItem
{
    private readonly List<IZipArchivedItem> _items;

    public ZipArchivedNode(string name, string fullPath, List<IZipArchivedItem> items)
    {
        Name = name;
        _items = items;
        Source = fullPath;
    }

    public string Name { get; }
    public string Source { get; }

    public string GetArchivedItemId() => Name;

    public IRepositoryItem GetRepositoryItem(ZipArchiveEntry zipArchiveEntry)
    {
        throw new NotImplementedException();
    }
}
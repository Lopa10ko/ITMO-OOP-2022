using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.ZipArchivedItems;

public class ZipArchivedNode : IZipArchivedItem
{
    private readonly List<IZipArchivedItem> _items;

    public ZipArchivedNode(string name, List<IZipArchivedItem> items)
    {
        Name = name;
        _items = items;
    }

    public string Name { get; }

    public IRepositoryItem GetRepositoryItem(ZipArchiveEntry zipArchiveEntry)
    {
        throw new NotImplementedException();
    }
}
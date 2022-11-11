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
        => new FileSystemNode(Name, Source, () => GetFunc(zipArchiveEntry));

    private List<IRepositoryItem> GetFunc(ZipArchiveEntry zipArchiveEntry)
    {
        using Stream stream = zipArchiveEntry.Open();
        var archive = new ZipArchive(stream, ZipArchiveMode.Read);
        return _items
            .Select(item => item.GetRepositoryItem(GetEntry(item.GetArchivedItemId(), archive)))
            .ToList();
    }

    private ZipArchiveEntry GetEntry(string archivedItemId, ZipArchive archive)
        => archive.GetEntry(archivedItemId) ?? throw new ArgumentNullException();
}
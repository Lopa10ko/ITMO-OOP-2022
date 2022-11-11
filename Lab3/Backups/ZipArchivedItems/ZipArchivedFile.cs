using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.ZipArchivedItems;

public class ZipArchivedFile : IZipArchivedItem
{
    public ZipArchivedFile(string name, string fullPath, ZipArchiveEntry fileEntry)
    {
        Name = name;
        Entry = fileEntry;
        Source = fullPath;
    }

    public ZipArchiveEntry Entry { get; }
    public string Name { get; }
    public string Source { get; }

    public string GetArchivedItemId() => Name;

    public IRepositoryItem GetRepositoryItem(ZipArchiveEntry zipArchiveEntry)
        => new FileSystemLeaf(Name, Source, zipArchiveEntry.Open);
}
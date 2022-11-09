using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.ZipArchivedItems;

public class ZipArchivedFile : IZipArchivedItem
{
    public ZipArchivedFile(string name, ZipArchiveEntry fileEntry)
    {
        Name = name;
        Entry = fileEntry;
    }

    public ZipArchiveEntry Entry { get; }
    public string Name { get; }

    public IRepositoryItem GetRepositoryItem(ZipArchiveEntry zipArchiveEntry)
    {
        throw new NotImplementedException();
    }
}
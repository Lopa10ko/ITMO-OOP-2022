using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.ZipArchivedItems;

public interface IZipArchivedItem
{
    string GetArchivedItemId();
    IRepositoryItem GetRepositoryItem(ZipArchiveEntry zipArchiveEntry);
}
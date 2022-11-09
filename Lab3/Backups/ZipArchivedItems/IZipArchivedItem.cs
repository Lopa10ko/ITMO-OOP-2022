using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.ZipArchivedItems;

public interface IZipArchivedItem
{
    IRepositoryItem GetRepositoryItem(ZipArchiveEntry zipArchiveEntry);
}
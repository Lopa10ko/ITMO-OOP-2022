using System.IO.Compression;
using Backups.Entities;
using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Archiver;

public interface IArchiver
{
    void Archive(RestorePoint restorePoint, IRepository repository);
}

public class ZipArchiver : IArchiver
{
    public void Archive(RestorePoint restorePoint, IRepository repository)
    {
        Stream repoStream = repository.OpenStream($"{restorePoint.Id.ToString()}.zip");
        using var archive = new ZipArchive(repoStream, ZipArchiveMode.Create);
    }
}
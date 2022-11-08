using System.IO.Compression;
using Backups.Archiver;
using Backups.Entities;
using Backups.Repositories;
using Backups.Services;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : ICreationAlgorithm
{
    public void Archive(RestorePoint restorePoint, IRepository repository, IArchiver archiver)
    {
        var storage = new Storage(repository);
        Stream repoStream = repository.OpenStream($"{restorePoint.Id.ToString()}.zip");
        using var archive = new ZipArchive(repoStream, ZipArchiveMode.Create);
        foreach (IBackupItem backupItem in restorePoint.Items)
        {
            /*backupItem.GetRepository();*/
            storage.SaveBackupItem(backupItem, archive);
        }

        restorePoint.AddStorage(storage);
        /*repository.Load(items);
        repository.AddStorages(items);
        restorePoint.AddRepository(repository);
        repository.Save(items);*/
    }
}
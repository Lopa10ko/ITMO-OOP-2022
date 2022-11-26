using Backups.Archiver;
using Backups.Models;
using Backups.Repositories;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : ICreationAlgorithm
{
    public IStorage Operate(IEnumerable<IBackupItem> trackingItems, IRepository repository, IArchiver archiver)
    {
        var repositoryItems = trackingItems
            .Select(backupItem => backupItem.GetRepository().GenerateRepositoryItem(backupItem.GetPath()));

        return archiver.Archive(repositoryItems, repository);
    }
}
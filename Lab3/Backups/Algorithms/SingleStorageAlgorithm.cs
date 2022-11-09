using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Services;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : ICreationAlgorithm
{
    public IStorage Operate(IEnumerable<IBackupItem> trackingItems, IRepository repository, IArchiver archiver)
    {
        var repositoryItems = trackingItems.
            Select(backupItem => backupItem.GetRepository().GenerateRepositoryItem(backupItem))
            .ToList();

        return archiver.Archive(repositoryItems, repository);
    }
}
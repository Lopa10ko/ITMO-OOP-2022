using System.Collections.Generic;
using System.Linq;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Services;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : ICreationAlgorithm
{
    public IStorage Operate(IEnumerable<IBackupItem> trackingItems, IRepository repository, IArchiver archiver)
        => new SplitStorage(trackingItems
            .Select(backupItem => backupItem.GetRepository().GenerateRepositoryItem(backupItem.GetPath()))
            .Select(repositoryItem => new List<IRepositoryItem> { repositoryItem })
            .Select(repoTempList => archiver.Archive(repoTempList, repository))
            .ToList());
}
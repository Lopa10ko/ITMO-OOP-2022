using System.Collections.Generic;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Repositories;
using Backups.Services;
using Backups.Storages;

namespace Backups.Algorithms;

public interface ICreationAlgorithm
{
    IStorage Operate(IEnumerable<IBackupItem> trackingItems, IRepository repository, IArchiver archiver);
}
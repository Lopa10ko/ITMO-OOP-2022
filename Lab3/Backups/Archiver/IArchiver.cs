using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Services;
using Backups.Storages;

namespace Backups.Archiver;

public interface IArchiver
{
    IStorage Archive(IEnumerable<IRepositoryItem> repositoryItems, IRepository repository);
}
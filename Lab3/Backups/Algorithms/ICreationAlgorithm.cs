using Backups.Archiver;
using Backups.Entities;
using Backups.Repositories;
using Backups.Services;

namespace Backups.Algorithms;

public interface ICreationAlgorithm
{
    void Archive(RestorePoint restorePoint, IRepository repository, IArchiver archiver);
}
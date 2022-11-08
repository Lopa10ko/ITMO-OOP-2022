using Backups.Archiver;
using Backups.Entities;
using Backups.Repositories;
using Backups.Services;

namespace Backups.Algorithms;

public class SplitStorage : ICreationAlgorithm
{
    public void Archive(RestorePoint restorePoint, IRepository repository, IArchiver archiver)
    {
        throw new NotImplementedException();
    }
}
using Backups.Algorithms;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Repositories;

namespace Backups.Services;

public class BackupConfigurationFacade : IBackupConfigurationFacade
{
    private readonly List<BackupTask> _backups;

    public BackupConfigurationFacade(ICreationAlgorithm creationAlgorithm, IRepository repository, IArchiver archiver)
    {
        CreationAlgorithm = creationAlgorithm;
        Repository = repository;
        Archiver = archiver;
        _backups = new List<BackupTask>();
    }

    public IReadOnlyList<BackupTask> Backups => _backups.AsReadOnly();
    public ICreationAlgorithm CreationAlgorithm { get; }
    public IRepository Repository { get; }
    public IArchiver Archiver { get; }

    public BackupTask CreateBackupTask()
    {
        BackupTask backupTask = BackupTask.Builder.
            WithAlgorithm(CreationAlgorithm).
            WithRepository(Repository).
            WithArchiver(Archiver).Build();
        _backups.Add(backupTask);
        return backupTask;
    }
}
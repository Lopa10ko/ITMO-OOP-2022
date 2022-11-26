using Backups.Algorithms;
using Backups.Archiver;
using Backups.Models;
using Backups.Repositories;
using Backups.Services;
using Backups.Tools;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<IBackupItem> _trackingItems;
    private Backup _backup;
    private ICreationAlgorithm _creationAlgorithm;
    private IRepository _repository;
    private IArchiver _archiver;

    private BackupTask(ICreationAlgorithm creationAlgorithm, IRepository repository, IArchiver archiver)
    {
        _trackingItems = new List<IBackupItem>();
        _backup = new Backup();
        _archiver = archiver;
        _creationAlgorithm = creationAlgorithm;
        _repository = repository;
        Id = Guid.NewGuid();
    }

    public static ICreationAlgorithmBuilder Builder => new BackupTaskBuilder();
    public Backup Backup => _backup;
    public IReadOnlyList<IBackupItem> TrackingItems => _trackingItems.AsReadOnly();
    public Guid Id { get; }

    public void AddBackupItem(IBackupItem backupItem)
    {
        if (_trackingItems.Contains(backupItem))
            throw BackupTaskException.ExistingItem(backupItem);
        _trackingItems.Add(backupItem);
    }

    public void DeleteBackupItem(IBackupItem backupItem)
    {
        if (!_trackingItems.Contains(backupItem))
            throw BackupTaskException.NonExistingItem(backupItem);
        _trackingItems.Remove(backupItem);
    }

    public RestorePoint CreateRestorePoint()
    {
        var restorePoint = new RestorePoint(_creationAlgorithm.Operate(_trackingItems, _repository, _archiver), _trackingItems);
        Backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }

    public class BackupTaskBuilder : ICreationAlgorithmBuilder, IRepositoryBuilder, IArchiverBuilder, IChainBuilder
    {
        private ICreationAlgorithm? Algorithm { get; set; }
        private IRepository? Repository { get; set; }
        private IArchiver? Archiver { get; set; }

        public IRepositoryBuilder WithAlgorithm(ICreationAlgorithm algorithm)
        {
            Algorithm = algorithm;
            return this;
        }

        public IArchiverBuilder WithRepository(IRepository repository)
        {
            Repository = repository;
            return this;
        }

        public IChainBuilder WithArchiver(IArchiver archiver)
        {
            Archiver = archiver;
            return this;
        }

        public IChainBuilder PlugBuilder() => this;

        public BackupTask Build()
        {
            ArgumentNullException.ThrowIfNull(Algorithm);
            ArgumentNullException.ThrowIfNull(Repository);
            ArgumentNullException.ThrowIfNull(Archiver);
            return new BackupTask(Algorithm, Repository, Archiver);
        }
    }
}

public interface ICreationAlgorithmBuilder
{
    IRepositoryBuilder WithAlgorithm(ICreationAlgorithm algorithm);
}

public interface IRepositoryBuilder
{
    IArchiverBuilder WithRepository(IRepository repository);
}

public interface IArchiverBuilder
{
    IChainBuilder WithArchiver(IArchiver archiver);
}

public interface IChainBuilder
{
    IChainBuilder PlugBuilder();
    BackupTask Build();
}
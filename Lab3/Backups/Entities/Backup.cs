using Backups.Services;
using Backups.Tools;

namespace Backups.Entities;

public class Backup : IBackup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
        Id = Guid.NewGuid();
    }

    public IReadOnlyList<RestorePoint> RestorePoints
        => _restorePoints
            .AsReadOnly();

    public Guid Id { get; }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
            throw BackupLogicException.ExistingRestorePoint(restorePoint);
        _restorePoints.Add(restorePoint);
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (!_restorePoints.Contains(restorePoint))
            throw BackupLogicException.NonExistingRestorePoint(restorePoint);
        _restorePoints.Remove(restorePoint);
    }
}
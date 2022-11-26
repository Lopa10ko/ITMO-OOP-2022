using Backups.Entities;
using Backups.Models;

namespace Backups.Tools;

public class BackupLogicException : BackupException
{
    private BackupLogicException(string errorMessage)
        : base(errorMessage) { }

    public static BackupLogicException ExistingRestorePoint(RestorePoint restorePoint)
        => new BackupLogicException($"Invalid addition in Backup: RestorePoint - ({restorePoint.Id}) is already tracking");
    public static BackupLogicException NonExistingRestorePoint(RestorePoint restorePoint)
        => new BackupLogicException($"Invalid deletion in Backup: RestorePoint - ({restorePoint.Id}) is not tracking");
}
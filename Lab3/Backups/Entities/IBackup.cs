namespace Backups.Entities;

public interface IBackup
{
    void AddRestorePoint(RestorePoint restorePoint);
}
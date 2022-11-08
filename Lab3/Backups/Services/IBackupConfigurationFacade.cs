using Backups.Archiver;
using Backups.Entities;

namespace Backups.Services;

public interface IBackupConfigurationFacade
{
    BackupTask CreateBackupTask();
    /*RestorePoint CreateRestorePoint();
    void AddBackupItem(string filePath, BackupTask backup);
    void DeleteBackupItem(string filePath, BackupTask backup);*/
}
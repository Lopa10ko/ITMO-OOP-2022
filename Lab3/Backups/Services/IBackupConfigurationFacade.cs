using Backups.Archiver;
using Backups.Entities;

namespace Backups.Services;

public interface IBackupConfigurationFacade
{
    BackupTask CreateBackupTask();
}
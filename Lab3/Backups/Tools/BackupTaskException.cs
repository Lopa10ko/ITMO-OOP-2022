using Backups.Models;

namespace Backups.Tools;

public class BackupTaskException : BackupException
{
    private BackupTaskException(string errorMessage)
        : base(errorMessage) { }

    public static BackupTaskException ExistingItem(IBackupItem item)
        => new BackupTaskException($"Invalid addition in BackupTask: object - ({item.GetPath()}) is already tracking");
    public static BackupTaskException NonExistingItem(IBackupItem item)
        => new BackupTaskException($"Invalid deletion in BackupTask: object - ({item.GetPath()}) is not tracking");
}
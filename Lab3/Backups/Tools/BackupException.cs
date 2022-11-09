namespace Backups.Tools;

public class BackupException : Exception
{
    public BackupException(string errorMessage)
        : base(errorMessage) { }
}
using Backups.Models;

namespace Backups.Tools;

public class FileSystemRepositoryException : BackupException
{
    private FileSystemRepositoryException(string errorMessage)
        : base(errorMessage) { }

    public static FileSystemRepositoryException IncorrectDirectoryStringFormat(string sourcePath)
        => new FileSystemRepositoryException($"Invalid DirectoryId {sourcePath} - should be ...\\");
    public static FileSystemRepositoryException InvalidBackupItem(string backupItemId)
        => new FileSystemRepositoryException($"Invalid file: {backupItemId}");
}
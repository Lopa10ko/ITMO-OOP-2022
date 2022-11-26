using Backups.Algorithms;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Repositories;
using Backups.Services;
using Backups.Test.Repositories;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTests
{
    [Fact]
    public void CreateBackupTaskAndOperateObjects_RestorePointAndStorageCreated()
    {
        const string repoPath = "/mnt/c/Temp/Test";
        const string fileA = "stalin.txt";
        string fileB = $"folderA{UPath.DirectorySeparator}Lab_1_05.pdf";
        const string folderB = "folderB";
        IFileSystem fileSystem = CreateFileSystemInstance(repoPath, fileA, fileB, folderB);
        IRepository sourceRepository = new InMemoryRepository($"/mnt/c/Temp/Test{UPath.DirectorySeparator}", fileSystem);
        IRepository testRepositorySplit = new InMemoryRepository($"/mnt/c/Temp/Saved{UPath.DirectorySeparator}", fileSystem);
        var configSplitLocalZip =
            new BackupConfigurationFacade(new SplitStorageAlgorithm(), testRepositorySplit, new ZipArchiver());
        BackupTask backupTaskSplit = configSplitLocalZip.CreateBackupTask();
        backupTaskSplit.AddBackupItem(new FileBackupItem(fileA, sourceRepository));
        backupTaskSplit.AddBackupItem(new FileBackupItem(fileB, sourceRepository));
        var tempBackupObject = new FileBackupItem(folderB, sourceRepository);
        backupTaskSplit.AddBackupItem(tempBackupObject);
        backupTaskSplit.CreateRestorePoint();
        backupTaskSplit.DeleteBackupItem(tempBackupObject);
        backupTaskSplit.CreateRestorePoint();
        Assert.Equal(2, backupTaskSplit.Backup.RestorePoints.Count);
        Assert.Equal(2, backupTaskSplit.Backup.RestorePoints.Count);
    }

    private static IFileSystem CreateFileSystemInstance(string repoPath, string fileA, string fileB, string folderB)
    {
        var fs = new MemoryFileSystem();
        fs.CreateDirectory("/mnt/c/Temp/Test");
        fs.CreateDirectory("/mnt/c/Temp/Saved");
        fs.CreateDirectory("/mnt/c/Temp/Test/folderA");
        fs.CreateDirectory("/mnt/c/Temp/Test/folderB");
        Stream fileAStream = fs.OpenFile($"{repoPath}{UPath.DirectorySeparator}{fileA}", FileMode.OpenOrCreate, FileAccess.Write);
        Stream fileBStream = fs.OpenFile($"{repoPath}{UPath.DirectorySeparator}{fileB}", FileMode.OpenOrCreate, FileAccess.Write);
        fileAStream.Close();
        fileBStream.Close();
        return fs;
    }
}
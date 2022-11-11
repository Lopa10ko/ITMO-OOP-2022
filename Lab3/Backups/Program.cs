using System.IO.Compression;
using Backups.Algorithms;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Repositories;
using Backups.Services;

namespace Backups;

public static class Program
{
    public static void Main()
    {
        TestSingle();

        // TestSplit();
    }

    private static void TestSingle()
    {
        const string fileA = "stalin.png";
        const string fileB = "folderA\\Lab_1_05.pdf";
        const string folderB = "folderB";
        IRepository sourceRepository = new FileSystemRepository(@"C:\Users\George\Desktop\test\");
        IRepository testRepository = new FileSystemRepository(@"C:\Users\George\Desktop\saved\");
        var configSingleLocalZip =
            new BackupConfigurationFacade(new SingleStorageAlgorithm(), testRepository, new ZipArchiver());
        BackupTask backupTask = configSingleLocalZip.CreateBackupTask();
        backupTask.AddBackupItem(new FileBackupItem(fileA, sourceRepository));
        backupTask.AddBackupItem(new FileBackupItem(fileB, sourceRepository));
        backupTask.AddBackupItem(new FileBackupItem(folderB, sourceRepository));
        backupTask.CreateRestorePoint();
    }

    private static void TestSplit()
    {
        const string fileA = "stalin.png";
        const string fileB = "folderA\\Lab_1_05.pdf";
        const string folderB = "folderB";
        IRepository sourceRepository = new FileSystemRepository(@"C:\Users\George\Desktop\test\");
        IRepository testRepositorySplit = new FileSystemRepository(@"C:\Users\George\Desktop\saved\split\");
        var configSplitLocalZip =
            new BackupConfigurationFacade(new SplitStorageAlgorithm(), testRepositorySplit, new ZipArchiver());
        BackupTask backupTaskSplit = configSplitLocalZip.CreateBackupTask();
        backupTaskSplit.AddBackupItem(new FileBackupItem(fileA, sourceRepository));
        backupTaskSplit.AddBackupItem(new FileBackupItem(fileB, sourceRepository));
        backupTaskSplit.AddBackupItem(new FileBackupItem(folderB, sourceRepository));
        backupTaskSplit.CreateRestorePoint();
    }
}
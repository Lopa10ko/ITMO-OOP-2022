using System.IO.Compression;
using Backups.Models;
using Backups.RepositoryItems;
using Backups.Tools;

namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string sourcePath)
    {
        Validate(sourcePath);
        Source = sourcePath;
    }

    private string Source { get; }

    public Stream OpenStream(string archiveName)
        => new FileStream($"{Source}\\{archiveName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);

    public void Save(List<ZipArchive> archivedItems)
    {
        throw new NotImplementedException();
    }

    public string GetSource() => Source;

    public IRepositoryItem GenerateRepositoryItem(IBackupItem backupItem)
    {
        string backupItemRelativeId = backupItem.GetPath();
        string backupItemFullPath = $"{Source}{backupItemRelativeId}";
        if (File.Exists(backupItemFullPath))
            return new FileSystemLeaf(backupItemRelativeId, backupItemFullPath, () => File.Open(backupItemFullPath, FileMode.Open, FileAccess.Read));
        if (Directory.Exists(backupItemFullPath))
            return new FileSystemNode(backupItemRelativeId, backupItemFullPath, () => GetItemsList(backupItemFullPath));
        throw new Exception("not existing item in directory");
    }

    private static void Validate(string sourcePath)
    {
        if (!sourcePath.EndsWith(Path.DirectorySeparatorChar))
            throw FileSystemRepositoryException.IncorrectDirectoryStringFormat(sourcePath);
    }

    private static string GetDirName(string fullPath)
        => fullPath.Split(Path.DirectorySeparatorChar).Last();
    private IReadOnlyCollection<IRepositoryItem> GetItemsList(string directoryId)
        => new List<IRepositoryItem>(Directory.EnumerateFiles(directoryId, "*", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName)
                .Select(fn => new FileSystemLeaf(
                    fn!,
                    $"{directoryId}{Path.DirectorySeparatorChar}{fn}",
                    () => File.Open($"{directoryId}\\{fn}", FileMode.Open, FileAccess.Read))))
            .Concat(new List<IRepositoryItem>(Directory.EnumerateDirectories(directoryId, "*", SearchOption.TopDirectoryOnly)
                .Select(dn => new FileSystemNode(
                    GetDirName(dn),
                    $"{directoryId}{Path.DirectorySeparatorChar}{GetDirName(dn)}",
                    () => GetItemsList($"{directoryId}{Path.DirectorySeparatorChar}{GetDirName(dn)}"))))).ToList();
}
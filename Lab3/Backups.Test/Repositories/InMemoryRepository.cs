using Backups.Models;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Tools;
using Microsoft.VisualBasic;
using Zio;
using Zio.FileSystems;

namespace Backups.Test.Repositories;

public class InMemoryRepository : IRepository
{
    public InMemoryRepository(string sourcePath, IFileSystem fileSystem)
    {
        Validate(sourcePath);
        Source = sourcePath;
        FileSystem = fileSystem;
    }

    private string Source { get; }
    private IFileSystem FileSystem { get; }

    public Stream OpenStream(string archiveName)
         => FileSystem.OpenFile($"{Source}\\{archiveName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);

    public IRepositoryItem GenerateRepositoryItem(string backupItemRelativeId)
    {
        string backupItemFullPath = $"{Source}{backupItemRelativeId}";
        if (FileSystem.FileExists(backupItemFullPath))
            return new FileSystemLeaf(backupItemRelativeId, backupItemFullPath, () => FileSystem.OpenFile(backupItemFullPath, FileMode.Open, FileAccess.Read));
        if (FileSystem.DirectoryExists(backupItemFullPath))
            return new FileSystemNode(backupItemRelativeId, backupItemFullPath, () => GetItemsList(backupItemFullPath));
        throw FileSystemRepositoryException.InvalidBackupItem(backupItemRelativeId);
    }

    public string GetSource() => Source;
    private static string GetDirName(string fullPath)
        => fullPath.Split(UPath.DirectorySeparator).Last();
    private static void Validate(string sourcePath)
    {
        if (!sourcePath.EndsWith(UPath.DirectorySeparator))
            throw FileSystemRepositoryException.IncorrectDirectoryStringFormat(sourcePath);
    }

    private IReadOnlyCollection<IRepositoryItem> GetItemsList(string directoryId)
        => new List<IRepositoryItem>(FileSystem.EnumerateFiles(directoryId, "*", SearchOption.TopDirectoryOnly)
                .Select(upath => GetDirName(upath.FullName))
                .Select(fn => new FileSystemLeaf(
                    fn,
                    $"{directoryId}{UPath.DirectorySeparator}{fn}",
                    () => FileSystem.OpenFile($"{directoryId}{UPath.DirectorySeparator}{fn}", FileMode.Open, FileAccess.Read))))
            .Concat(new List<IRepositoryItem>(FileSystem.EnumerateDirectories(directoryId, "*", SearchOption.TopDirectoryOnly)
                .Select(upath => upath.FullName)
                .Select(dn => new FileSystemNode(
                    GetDirName(dn),
                    $"{directoryId}{Path.DirectorySeparatorChar}{GetDirName(dn)}",
                    () => GetItemsList($"{directoryId}{Path.DirectorySeparatorChar}{GetDirName(dn)}"))))).ToList();
}
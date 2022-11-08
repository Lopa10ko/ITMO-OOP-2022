using System.IO.Compression;
using Backups.RepositoryItems;
using Backups.Services;

namespace Backups.Repositories;

public interface IRepository
{
    Stream OpenStream(string archiveName);
    void Save(List<ZipArchive> archivedItems);
    IRepositoryItem GenerateRepositoryItem(IBackupItem backupItem);
    string GetSource();
}

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string source)
    {
        Source = source;
    }

    public string Source { get; }

    public Stream OpenStream(string archiveName)
        => new FileStream($"{Source}\\{archiveName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);

    public void Save(List<ZipArchive> archivedItems)
    {
        throw new NotImplementedException();
    }

    public IRepositoryItem GenerateRepositoryItem(IBackupItem backupItem)
    {
        throw new NotImplementedException();
    }

    public string GetSource()
    {
        return Source;
    }
}
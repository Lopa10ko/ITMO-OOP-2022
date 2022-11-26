using System.IO.Compression;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;
using Backups.Visitors;

namespace Backups.Archiver;

public class ZipArchiver : IArchiver
{
    public IStorage Archive(IEnumerable<IRepositoryItem> repositoryItems, IRepository repository)
    {
        string streamName = Guid.NewGuid().ToString();
        Stream repoStream = repository.OpenStream($"{streamName}.zip");
        using var archive = new ZipArchive(repoStream, ZipArchiveMode.Create);
        var visitor = new FileSystemZipVisitor(archive);
        foreach (IRepositoryItem repositoryItem in repositoryItems)
        {
            repositoryItem.Accept(visitor);
        }

        return new ZipArchivedStorage($"{streamName}.zip", repository, visitor.GetLastOnStack());
    }
}
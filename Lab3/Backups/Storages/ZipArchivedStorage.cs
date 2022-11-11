using System.IO.Compression;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.ZipArchivedItems;

namespace Backups.Storages;

public class ZipArchivedStorage : IStorage
{
    public ZipArchivedStorage(string relativePath, IRepository repository, IEnumerable<IZipArchivedItem> items)
    {
        Repository = repository;
        Items = items;
        RelativeId = relativePath;
    }

    public IEnumerable<IZipArchivedItem> Items { get; }
    public string RelativeId { get; }
    public IRepository Repository { get; }

    public IEnumerable<IRepositoryItem> GetRepositoryItems()
    {
        using Stream stream = ((IRepositoryLeaf)Repository.GenerateRepositoryItem(RelativeId)).GetCurrentStream();
        var archive = new ZipArchive(stream, ZipArchiveMode.Read);
        return Items
            .Select(zipArchivedItem => zipArchivedItem.GetRepositoryItem(GetEntry(zipArchivedItem.GetArchivedItemId(), archive)))
            .ToList();
    }

    private ZipArchiveEntry GetEntry(string archivedItemId, ZipArchive archive)
        => archive.GetEntry(archivedItemId) ?? throw new ArgumentNullException();
}
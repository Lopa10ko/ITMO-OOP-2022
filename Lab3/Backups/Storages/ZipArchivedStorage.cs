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
        /*=> Items.Select(zi => zi.GetRepositoryItem(zi.Entry)).ToList();*/
        throw new NotImplementedException();
    }
}
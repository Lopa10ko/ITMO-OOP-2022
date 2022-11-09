using Backups.RepositoryItems;

namespace Backups.Storages;

public class SplitStorage : IStorage
{
    private readonly IEnumerable<IStorage> _archivedStorages;

    public SplitStorage(IEnumerable<IStorage> archivedStorages)
    {
        _archivedStorages = archivedStorages;
    }

    public IEnumerable<IRepositoryItem> GetRepositoryItems()
        => _archivedStorages.SelectMany(storage => storage.GetRepositoryItems());
}
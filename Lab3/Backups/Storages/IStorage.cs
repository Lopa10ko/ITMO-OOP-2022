using Backups.RepositoryItems;

namespace Backups.Storages;

public interface IStorage
{
    IEnumerable<IRepositoryItem> GetRepositoryItems();
}
namespace Backups.RepositoryItems;

public interface IRepositoryNode : IRepositoryItem
{
    string GetNodeId();
    string GetNodeRelativePath();
    IEnumerable<IRepositoryItem> GetItems();
}
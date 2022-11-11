namespace Backups.RepositoryItems;

public interface IRepositoryNode : IRepositoryItem
{
    string GetNodeId();
    string GetNodeRelativePath();
    string GetSourceId();
    IEnumerable<IRepositoryItem> GetItems();
}
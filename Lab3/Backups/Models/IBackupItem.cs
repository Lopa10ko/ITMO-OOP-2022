using Backups.Repositories;

namespace Backups.Models;

public interface IBackupItem
{
    IRepository GetRepository();
    string GetPath();
}
using Backups.Repositories;

namespace Backups.Services;

public interface IBackupItem
{
    IRepository GetRepository();
    string GetPath();
}
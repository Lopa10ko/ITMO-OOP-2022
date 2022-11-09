using System.IO.Compression;
using System.Linq;
using Backups.Models;
using Backups.RepositoryItems;
using Backups.Services;

namespace Backups.Repositories;

public interface IRepository
{
    Stream OpenStream(string archiveName);
    IRepositoryItem GenerateRepositoryItem(IBackupItem backupItem);
    string GetSource();
}
using System.IO.Compression;
using Backups.Repositories;
using Backups.Services;

namespace Backups.Models;

public class FileBackupItem : IBackupItem
{
    public FileBackupItem(string path, IRepository repository)
    {
        FilePath = path;
        Repository = repository;
    }

    public IRepository Repository { get; }
    public string FilePath { get; }

    public string GetPath() => FilePath;
    public IRepository GetRepository() => Repository;
}
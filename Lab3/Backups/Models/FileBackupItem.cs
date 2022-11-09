using System.IO.Compression;
using Backups.Repositories;
using Backups.Services;

namespace Backups.Models;

public class FileBackupItem : IBackupItem
{
    public FileBackupItem(string relativePath, IRepository repository)
    {
        RelativePath = relativePath;
        Repository = repository;
    }

    public IRepository Repository { get; }
    public string RelativePath { get; }

    public string GetPath() => RelativePath;
    public IRepository GetRepository() => Repository;
}
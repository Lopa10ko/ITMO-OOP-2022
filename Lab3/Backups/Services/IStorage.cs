using System.IO.Compression;
using Backups.Entities;
using Backups.Repositories;

namespace Backups.Services;

public class Storage
{
    public Storage(IRepository repository)
    {
        Repository = repository;
    }

    public IRepository Repository { get; }

    public void SaveBackupItem(IBackupItem backupItem, ZipArchive archive)
    {
        ZipArchiveEntry zipArchiveEntry = archive.CreateEntry($"{backupItem.GetPath()}");
        using Stream entryStream = zipArchiveEntry.Open();
        using FileStream sourceStream = File.Open($"{backupItem.GetRepository().GetSource()}{backupItem.GetPath()}", FileMode.Open, FileAccess.Read);
        sourceStream.CopyTo(entryStream);
        /*GetArchive(backupItem);*/
        /*Repository.Save(backupItem);*/
    }

    private static ZipArchive GetArchive(IBackupItem backupItem)
    {
        /* read all data from backupItem
         repository.LoadData(...)
         and do archiving --> Stream -> ZipArchive */
        throw new NotImplementedException();
    }
}
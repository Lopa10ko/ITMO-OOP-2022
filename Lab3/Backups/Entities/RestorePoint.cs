using Backups.Models;
using Backups.Services;
using Backups.Storages;

namespace Backups.Entities;

public class RestorePoint : IEquatable<RestorePoint>
{
    public RestorePoint(IStorage storage, IEnumerable<IBackupItem> items)
    {
        Storage = storage;
        CreationTime = DateTime.Now;
        Id = Guid.NewGuid();
        Items = items;
    }

    public Guid Id { get; }
    public DateTime CreationTime { get; }
    public IStorage Storage { get; }
    public IEnumerable<IBackupItem> Items { get; }

    public bool Equals(RestorePoint? other)
    => other is not null && Id.Equals(other.Id) && CreationTime.Equals(other.CreationTime);

    public override bool Equals(object? obj)
        => Equals(obj as RestorePoint);

    public override int GetHashCode()
        => HashCode.Combine(Id, CreationTime);
}
using Backups.Services;

namespace Backups.Entities;

public class RestorePoint : IEquatable<RestorePoint>
{
    private readonly List<Storage> _storages;
    public RestorePoint(IReadOnlyList<IBackupItem> trackingItems)
    {
        Items = trackingItems;
        /* TODO: trackingItems or List<ZipArchive> archived */
        _storages = new List<Storage>();
        CreationTime = DateTime.Now;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public DateTime CreationTime { get; }
    public IReadOnlyList<IBackupItem> Items { get; }
    public IReadOnlyList<Storage> Storages => _storages.AsReadOnly();

    public void AddStorage(Storage storage)
    {
        _storages.Add(storage);
    }

    public bool Equals(RestorePoint? other)
    => other is not null && Id.Equals(other.Id) && CreationTime.Equals(other.CreationTime);

    public override bool Equals(object? obj)
        => Equals(obj as RestorePoint);

    public override int GetHashCode()
        => HashCode.Combine(Id, CreationTime);
}
namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
        Id = Guid.NewGuid();
    }

    public IReadOnlyList<RestorePoint> RestorePoints
        => _restorePoints
            .AsReadOnly();

    public Guid Id { get; }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
    }
}
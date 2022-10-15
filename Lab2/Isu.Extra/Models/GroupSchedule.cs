using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class GroupSchedule<T> : IEquatable<GroupSchedule<T>>
{
    public GroupSchedule(T group, Schedule schedule)
    {
        Group = group;
        Schedule = schedule;
    }

    internal T Group { get; }
    internal Schedule Schedule { get; }

    public bool Equals(GroupSchedule<T>? other)
        => other is not null && EqualityComparer<T>.Default.Equals(Group, other.Group);

    public override bool Equals(object? obj)
        => Equals(obj as GroupSchedule<T>);

    public override int GetHashCode()
        => EqualityComparer<T>.Default.GetHashCode(Group!);
}
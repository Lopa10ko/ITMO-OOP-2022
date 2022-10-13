using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public record Lesson
{
    public Lesson(string name, Professor professor, int room, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime)
    {
        Name = name;
        Professor = professor;
        DayOfWeek = dayOfWeek;
        Room = ValidateRoom(room);
        (StartTime, EndTime) = ValidateTime(startTime, endTime);
    }

    public string Name { get; }
    public Professor Professor { get; }
    public int Room { get; }
    public DayOfWeek DayOfWeek { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public bool IsOverlapping(Lesson other)
        => DayOfWeek.Equals(other.DayOfWeek)
           && !(EndTime < other.StartTime || other.EndTime < StartTime);

    private static (TimeOnly, TimeOnly) ValidateTime(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime > endTime)
            throw LessonException.InvalidTime(startTime, endTime);
        return (startTime, endTime);
    }

    private static int ValidateRoom(int room)
    {
        if (room < 0)
            throw LessonException.InvalidRoom(room);
        return room;
    }
}
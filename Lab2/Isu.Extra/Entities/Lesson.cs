using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public record Lesson
{
    public Lesson(string name, string professor, int room, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime)
    {
        Name = name;
        Professor = professor;
        DayOfWeek = dayOfWeek;
        ValidateRoom(room);
        Room = room;
        ValidateTime(startTime, endTime);
        StartTime = startTime;
        EndTime = endTime;
    }

    public string Name { get; }
    public string Professor { get; }
    public int Room { get; }
    public DayOfWeek DayOfWeek { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public bool IsOverlapping(Lesson other)
        => DayOfWeek.Equals(other.DayOfWeek)
           && !(EndTime < other.StartTime || other.EndTime < StartTime);

    private static void ValidateTime(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime > endTime)
            throw LessonException.InvalidTime(startTime, endTime);
    }

    private static void ValidateRoom(int room)
    {
        if (room < 0)
            throw LessonException.InvalidRoom(room);
    }
}
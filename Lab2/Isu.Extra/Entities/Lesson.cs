namespace Isu.Extra.Entities;

public record Lesson
{
    public Lesson(string lessonName, string lessonProfessor, int lessonRoom, DayOfWeek lessonDayOfWeek, TimeOnly lessonStartTime, TimeOnly lessonEndTime)
    {
        LessonName = lessonName;
        LessonProfessor = lessonProfessor;
        LessonDayOfWeek = lessonDayOfWeek;
        ValidateLessonRoom(lessonRoom);
        LessonRoom = lessonRoom;
        ValidateLessonTime(lessonStartTime, lessonEndTime);
        LessonStartTime = lessonStartTime;
        LessonEndTime = lessonEndTime;
    }

    public string LessonName { get; }
    public string LessonProfessor { get; }
    public int LessonRoom { get; }
    public DayOfWeek LessonDayOfWeek { get; }
    public TimeOnly LessonStartTime { get; }
    public TimeOnly LessonEndTime { get; }

    public bool IsOverlapping(Lesson otherLesson)
        => LessonDayOfWeek.Equals(otherLesson.LessonDayOfWeek)
           && !(LessonEndTime < otherLesson.LessonStartTime || otherLesson.LessonEndTime < LessonStartTime);

    private static void ValidateLessonTime(TimeOnly lessonStartTime, TimeOnly lessonEndTime)
    {
        if (lessonStartTime > lessonEndTime)
            throw new Exception();
    }

    private static void ValidateLessonRoom(int lessonRoom)
    {
        if (lessonRoom < 0)
            throw new Exception();
    }
}
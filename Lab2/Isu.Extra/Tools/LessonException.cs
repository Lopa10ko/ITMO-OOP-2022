namespace Isu.Extra.Tools;

public class LessonException : IsuExtraException
{
    private LessonException(string errorMessage)
        : base(errorMessage) { }

    public static LessonException InvalidLessonTime(TimeOnly lessonStartTime, TimeOnly lessonEndTime)
        => new LessonException($"LessonTime is invalid: {lessonStartTime} should be less than {lessonEndTime}");

    public static LessonException InvalidLessonRoom(int lessonRoom)
        => new LessonException($"LessonRoom is invalid: {lessonRoom} should be positive integer");
}
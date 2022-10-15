using Isu.Extra.Entities;

namespace Isu.Extra.Tools;

public class ScheduleBuilderException : IsuExtraException
{
    private ScheduleBuilderException(string errorMessage)
        : base(errorMessage) { }

    public static ScheduleBuilderException OverlappingLessons(Lesson lesson, Lesson otherLesson)
        => new ScheduleBuilderException($"Lesson {FormatLesson(otherLesson)} addition failed: overlapping with Lesson {FormatLesson(lesson)}");

    private static string FormatLesson(Lesson lesson)
        => $"{lesson.Name} : {lesson.Professor.Name} : {lesson.Room} : {lesson.DayOfWeek} : {lesson.StartTime}-{lesson.EndTime}";
}
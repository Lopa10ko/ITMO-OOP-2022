namespace Isu.Extra.Tools;

public class ScheduleServiceException : IsuExtraException
{
    private ScheduleServiceException(string errorMessage)
        : base(errorMessage) { }

    public static ScheduleServiceException OgnpOverlappingRoom()
        => new ScheduleServiceException($"OgnpGroup schedules is overlapping by the LessonRoom");

    public static ScheduleServiceException OverlappingRoom()
        => new ScheduleServiceException($"Group schedules is overlapping by the LessonRoom");

    public static ScheduleServiceException OgnpOverlappingProfessor()
        => new ScheduleServiceException($"OgnpGroup schedules is overlapping by the LessonProfessor");

    public static ScheduleServiceException OverlappingProfessor()
        => new ScheduleServiceException($"Group schedules is overlapping by the LessonProfessor");

    public static ScheduleServiceException OverlappingRoomAmongAll()
        => new ScheduleServiceException($"Group and OgnpGroup schedules is overlapping by the LessonRoom");

    public static ScheduleServiceException OverlappingProfessorAmongAll()
        => new ScheduleServiceException($"Group and OgnpGroup schedules is overlapping by the LessonProfessor");
}
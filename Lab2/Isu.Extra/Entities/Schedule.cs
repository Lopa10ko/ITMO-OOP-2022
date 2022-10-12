using Isu.Extra.Tools;
using Isu.Models;

namespace Isu.Extra.Entities;

public class Schedule
{
    private List<Lesson> _lessons;

    private Schedule(List<Lesson> lessons)
    {
        _lessons = lessons;
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public bool IsOverlapping(Schedule otherSchedule)
        => _lessons.All(lesson => otherSchedule._lessons.Any(lesson.IsOverlapping));

    public bool IsOverlappingProfessor(Schedule otherSchedule)
        => _lessons.Any(lesson => otherSchedule._lessons.Any(otherLesson =>
            lesson.IsOverlapping(otherLesson) && lesson.LessonProfessor.Equals(otherLesson.LessonProfessor)));

    public bool IsOverlappingRoom(Schedule otherSchedule)
        => _lessons.Any(lesson => otherSchedule._lessons.Any(otherLesson =>
            lesson.IsOverlapping(otherLesson) && lesson.LessonRoom.Equals(otherLesson.LessonRoom)));

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons = new List<Lesson>();

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }

        public ScheduleBuilder AddLesson(Lesson otherLesson)
        {
            bool overlapping = _lessons.Any(lesson => lesson.IsOverlapping(otherLesson));
            if (overlapping)
            {
                Lesson overlappingLesson = _lessons.Single(lesson => lesson.IsOverlapping(otherLesson));
                throw ScheduleBuilderException.OverlappingLessons(overlappingLesson, otherLesson);
            }

            _lessons.Add(otherLesson);
            return this;
        }
    }
}
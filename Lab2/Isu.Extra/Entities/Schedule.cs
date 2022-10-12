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
        => _lessons.Any(lesson => otherSchedule._lessons.Any(lesson.IsOverlapping));

    public bool IsOverlappingProfessor(Schedule otherSchedule)
        => _lessons.Any(lesson => otherSchedule._lessons.Any(otherLesson =>
            lesson.IsOverlapping(otherLesson) && lesson.Professor.Equals(otherLesson.Professor)));

    public bool IsOverlappingRoom(Schedule otherSchedule)
        => _lessons.Any(lesson => otherSchedule._lessons.Any(otherLesson =>
            lesson.IsOverlapping(otherLesson) && lesson.Room.Equals(otherLesson.Room)));

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons = new List<Lesson>();

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }

        public ScheduleBuilder AddLesson(Lesson other)
        {
            bool overlapping = _lessons.Any(lesson => lesson.IsOverlapping(other));
            if (overlapping)
            {
                Lesson overlappingLesson = _lessons.Single(lesson => lesson.IsOverlapping(other));
                throw ScheduleBuilderException.OverlappingLessons(overlappingLesson, other);
            }

            _lessons.Add(other);
            return this;
        }
    }
}
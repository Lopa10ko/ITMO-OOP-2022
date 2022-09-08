using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int GroupCapacity = 20;
    private List<Student> _students = new List<Student>();
    public Group(GroupName groupName)
    {
        GroupName = groupName;
        CourseNumber = new CourseNumber(groupName.GetCourseNumber());
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }
}
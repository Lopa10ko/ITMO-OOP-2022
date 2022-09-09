using Isu.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups;
    private readonly List<Student> _students;
    private int _idIsu;

    public IsuService()
    {
        _groups = new List<Group>();
        _students = new List<Student>();
        _idIsu = 1;
    }

    public Group AddGroup(GroupName name)
    {
        var group = new Group(name);
        if (_groups.Contains(group))
        {
            throw new GroupException("Group is already created");
        }

        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (!_groups.Contains(group))
        {
            throw new GroupException("Group is not initialized");
        }

        var student = new Student(_idIsu, name);
        _idIsu++;
        group.AddStudent(student);
        _students.Add(student);
        return student;
    }

    public Student GetStudent(int id)
        => _students.Single(student => student.IsuNumber == id);

    public Student? FindStudent(int id)
        => _students.SingleOrDefault(student => student.IsuNumber == id);

    public List<Student> FindStudents(GroupName groupName)
        => _groups.Where(group => group.GroupName.Equals(groupName))
            .SelectMany(group => group.GetStudents()).ToList();

    public List<Student> FindStudents(CourseNumber courseNumber)
        => _groups.Where(group => group.CourseNumber.Equals(courseNumber))
            .SelectMany(group => group.GetStudents()).ToList();

    public Group? FindGroup(GroupName groupName)
        => _groups.FirstOrDefault(group => group.GroupName.Equals(groupName));

    public List<Group> FindGroups(CourseNumber courseNumber)
        => _groups.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student.Group == null)
        {
            throw new GroupException("Student must have oldGroup to change groups");
        }

        student.Group.RemoveStudent(student);
        newGroup.AddStudent(student);
    }
}
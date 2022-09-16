using Isu.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups;
    private readonly List<Student> _students;
    private IdIsuGenerator _idIsu;

    public IsuService()
    {
        _groups = new List<Group>();
        _students = new List<Student>();
        _idIsu = new IdIsuGenerator();
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

        var student = new Student(_idIsu.IdIsu, name);
        _idIsu.IncrementIdIsu();
        group.AddStudent(student);
        student.AddGroup(group);
        _students.Add(student);
        return student;
    }

    public Student GetStudent(int id)
        => _students.Single(student => student.IsuNumber == id);

    public Student? FindStudent(int id)
        => _students.SingleOrDefault(student => student.IsuNumber == id);

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return _groups.Where(group => group.GroupName.Equals(groupName))
            .SelectMany(group => group.GetStudents).ToList();
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.CourseNumber.Equals(courseNumber))
            .SelectMany(group => group.GetStudents).ToList();
    }

    public Group? FindGroup(GroupName groupName)
        => _groups.FirstOrDefault(group => group.GroupName.Equals(groupName));

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
        => _groups.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student.Group == null)
        {
            throw new GroupException($"{student.Name} - {student.IsuNumber} must have oldGroup to change groups");
        }

        student.Group.RemoveStudent(student);
        newGroup.AddStudent(student);
        student.AddGroup(newGroup);
    }
}
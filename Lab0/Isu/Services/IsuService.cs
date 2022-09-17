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
            throw GroupLogicException.CreatedGroupException(group);
        }

        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (!_groups.Contains(group))
        {
            throw GroupLogicException.UnknownObjectException(group);
        }

        var student = new Student(_idIsu.IdIsu, name, group);
        _idIsu.NextId();
        _students.Add(student);
        return student;
    }

    public Student GetStudent(int id)
        => _students
            .Single(student => student.IsuNumber == id);

    public Student? FindStudent(int id)
        => _students
            .SingleOrDefault(student => student.IsuNumber == id);

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return _groups
            .Where(group => group.GroupName.Equals(groupName))
            .SelectMany(group => group.GetStudents).ToList();
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return _groups
            .Where(group => group.GroupName.CourseNumber.Equals(courseNumber))
            .SelectMany(group => group.GetStudents).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups
            .FirstOrDefault(group => group.GroupName.Equals(groupName));
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups
            .Where(group => group.GroupName.CourseNumber.Equals(courseNumber)).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (!_students.Contains(student))
            throw GroupLogicException.UnknownObjectException(student);

        if (!_groups.Contains(newGroup))
            throw GroupLogicException.UnknownObjectException(newGroup);

        if (student.Group.Equals(null))
            throw GroupLogicException.ChangeGroupException(student);

        student.ChangeGroup(newGroup);
    }
}
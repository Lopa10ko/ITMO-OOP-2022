using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    public Group AddGroup(GroupName name)
    {
        throw new NotImplementedException();
    }

    public Student AddStudent(Group group, string name)
    {
        /*try group.Addstudent(name)
         why not [Student AddStudent(Group group, Student student)]
         or [Student AddStudent(string group, string student)]*/
        throw new NotImplementedException();
    }

    public Student GetStudent(int id)
    {
        throw new NotImplementedException();
    }

    public Student? FindStudent(int id)
    {
        throw new NotImplementedException();
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        throw new NotImplementedException();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        throw new NotImplementedException();
    }

    public Group? FindGroup(GroupName groupName)
    {
        throw new NotImplementedException();
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        throw new NotImplementedException();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        throw new NotImplementedException();
    }
}
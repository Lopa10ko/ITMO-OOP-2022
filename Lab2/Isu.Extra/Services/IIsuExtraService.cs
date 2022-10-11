using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    OgnpGroup AddOgnpGroup(OgnpGroupName name);
    void AddOgnpGroupSchedule(OgnpGroup ognpGroup, Schedule schedule);
    void AddGroupSchedule(Group group, Schedule schedule);
    void AddStudent(OgnpGroup ognpGroup, Student student);
    void RemoveStudent(OgnpGroup ognpGroup, Student student);
    IReadOnlyList<Student> GetStudents(FacultyLetter facultyLetter);
    IReadOnlyList<OgnpGroup> GetOgnpGroups(FacultyLetter facultyLetter);
    IReadOnlyList<Student> FindStudents(OgnpGroupName ognpGroupName);
    IReadOnlyList<Student> FindNotSignedUpStudents(Group group);
}
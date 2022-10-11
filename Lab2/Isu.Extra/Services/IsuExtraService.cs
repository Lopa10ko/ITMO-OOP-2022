using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuExtraService
{
    private readonly List<OgnpGroup> _ognpGroups;
    private readonly List<Student> _signedStudents;
    private IScheduleService _scheduleService;

    public IsuExtraService(IScheduleService scheduleService)
    {
        _ognpGroups = new List<OgnpGroup>();
        _signedStudents = new List<Student>();
        _scheduleService = scheduleService;
    }

    public OgnpGroup AddOgnpGroup(OgnpGroupName name)
    {
        var ognpGroup = new OgnpGroup(name);
        if (_ognpGroups.Contains(ognpGroup))
            throw new Exception();

        // throw GroupLogicException.CreatedGroupException(ognpGroup);
        _ognpGroups.Add(ognpGroup);
        return ognpGroup;
    }

    public void AddOgnpGroupSchedule(OgnpGroup ognpGroup, Schedule schedule)
    {
        if (!_ognpGroups.Contains(ognpGroup))
            throw new Exception();
        _scheduleService.AddSchedule(ognpGroup, schedule);
    }

    public void AddGroupSchedule(Group group, Schedule schedule)
    {
        _scheduleService.AddSchedule(group, schedule);
    }

    public void AddStudent(OgnpGroup ognpGroup, Student student)
    {
        if (!_ognpGroups.Contains(ognpGroup))
            throw new Exception();
        if (_signedStudents.Contains(student))
            throw new Exception();
        ognpGroup.AddStudent(student);
        _signedStudents.Add(student);
    }

    public void RemoveStudent(OgnpGroup ognpGroup, Student student)
    {
        if (!_ognpGroups.Contains(ognpGroup))
            throw new Exception();
        ognpGroup.RemoveStudent(student);
        _signedStudents.Remove(student);
    }

    public IReadOnlyList<Student> GetStudents(FacultyLetter facultyLetter)
    {
        var tempStudents = new List<Student>();
        foreach (OgnpGroup ognpGroup in _ognpGroups
                     .Where(ognpGroup => ognpGroup.OgnpGroupName.FacultyLetter.Equals(facultyLetter)))
        {
            tempStudents.AddRange(ognpGroup.GetStudents);
        }

        return tempStudents.AsReadOnly();
    }

    public IReadOnlyList<OgnpGroup> GetOgnpGroups(FacultyLetter facultyLetter)
        => _ognpGroups.
            Where(g => g.OgnpGroupName.FacultyLetter.Equals(facultyLetter)).
            ToList();

    public IReadOnlyList<Student> FindStudents(OgnpGroupName ognpGroupName)
        => _ognpGroups.
            Single(g => g.OgnpGroupName.Equals(ognpGroupName)).
            GetStudents;

    public IReadOnlyList<Student> FindNotSignedUpStudents(Group group)
    {
        // foreach (Student student in group.GetStudents)
        // {
        //     foreach (OgnpGroup ognpGroup in _ognpGroups)
        //     {
        //         if (ognpGroup.GetStudents.Contains(student))
        //     }
        // }
        throw new NotImplementedException();
    }
}
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;
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
        ValidateOgnpGroupPresence(ognpGroup);
        ValidateStudentRegisterState(ognpGroup, student);
        if (ognpGroup.OgnpGroupName.FacultyLetter.Equals(student.Group.GroupName.FacultyLetter))
            throw LogicException.InvalidOgnpAddition(ognpGroup, student);
        ValidateSchedulesPresence(ognpGroup, student);
        ValidateOverlappingSchedules(ognpGroup, student);
        ognpGroup.AddStudent(student);
        _signedStudents.Add(student);
    }

    public void RemoveStudent(OgnpGroup ognpGroup, Student student)
    {
        ValidateOgnpGroupPresence(ognpGroup);
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
        => _ognpGroups.Where(g => g.OgnpGroupName.FacultyLetter.Equals(facultyLetter)).ToList();

    public IReadOnlyList<Student> FindStudents(OgnpGroupName ognpGroupName)
        => _ognpGroups.Single(g => g.OgnpGroupName.Equals(ognpGroupName)).GetStudents;

    public IReadOnlyList<Student> FindNotSignedUpStudents(Group group)
        => (IReadOnlyList<Student>)group.GetStudents.Where(s => !_signedStudents.Contains(s));

    private void ValidateOgnpGroupPresence(OgnpGroup ognpGroup)
    {
        if (!_ognpGroups.Contains(ognpGroup))
            throw AlienEntityException.AlienOgnpGroup(ognpGroup);
    }

    private void ValidateOgnpGroupRegisterState(OgnpGroup ognpGroup)
    {
        if (_ognpGroups.Contains(ognpGroup))
            throw OgnpGroupException.GroupAlreadyExists(ognpGroup);
    }

    private void ValidateStudentRegisterState(OgnpGroup ognpGroup, Student student)
    {
        if (_signedStudents.Contains(student))
            throw LogicException.InvalidRegisteredStudentState(ognpGroup, student);
    }

    private void ValidateOverlappingSchedules(OgnpGroup ognpGroup, Student student)
    {
        Schedule ognpSchedule = _scheduleService
            .GetOgnpGroupSchedules()
            .Single(gs => gs.Group.Equals(ognpGroup))
            .Schedule;
        Schedule studentSchedule = _scheduleService
            .GetGroupSchedules()
            .Single(gs => gs.Group.Equals(student.Group))
            .Schedule;
        if (ognpSchedule.IsOverlapping(studentSchedule))
            throw LogicException.OverlappingSchedules(ognpGroup, student);
    }

    private void ValidateSchedulesPresence(OgnpGroup ognpGroup, Student student)
    {
        if (!_scheduleService.GetOgnpGroupSchedules().Any(gs => gs.Group.Equals(ognpGroup)))
            throw LogicException.InvalidSchedulesState(ognpGroup, student, "OgnpGroup");
        if (!_scheduleService.GetGroupSchedules().Any(gs => gs.Group.Equals(student.Group)))
            throw LogicException.InvalidSchedulesState(ognpGroup, student, "Group");
    }
}
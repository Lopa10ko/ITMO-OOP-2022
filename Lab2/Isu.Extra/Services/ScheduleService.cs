using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Services;

public class ScheduleService : IScheduleService
{
    private readonly List<GroupSchedule<Group>> _groupSchedules;
    private readonly List<GroupSchedule<OgnpGroup>> _ognpGroupSchedules;

    public ScheduleService()
    {
        _groupSchedules = new List<GroupSchedule<Group>>();
        _ognpGroupSchedules = new List<GroupSchedule<OgnpGroup>>();
    }

    public void AddSchedule(Group group, Schedule schedule)
    {
        var groupSchedule = new GroupSchedule<Group>(group, schedule);
        bool isInSchedulesList = _groupSchedules.Any(gS => gS.Equals(groupSchedule));
        if (isInSchedulesList)
        {
            GroupSchedule<Group> tempGroupSchedule = _groupSchedules.Single(gS => gS.Equals(groupSchedule));
            ValidateGroupScheduleIntersections(groupSchedule);
            _groupSchedules.Remove(tempGroupSchedule);
        }
        else
        {
            ValidateGroupScheduleIntersections(groupSchedule);
        }

        _groupSchedules.Add(groupSchedule);
    }

    public void AddSchedule(OgnpGroup ognpGroup, Schedule schedule)
    {
        var ognpGroupSchedule = new GroupSchedule<OgnpGroup>(ognpGroup, schedule);
        bool isInSchedulesList = _ognpGroupSchedules.Any(gS => gS.Equals(ognpGroupSchedule));
        if (isInSchedulesList)
        {
            GroupSchedule<OgnpGroup> tempOgnpGroupSchedule = _ognpGroupSchedules.Single(gS => gS.Equals(ognpGroupSchedule));
            ValidateOgnpGroupScheduleIntersections(ognpGroupSchedule);
            _ognpGroupSchedules.Remove(tempOgnpGroupSchedule);
        }
        else
        {
            ValidateOgnpGroupScheduleIntersections(ognpGroupSchedule);
        }

        _ognpGroupSchedules.Add(ognpGroupSchedule);
    }

    public IReadOnlyList<GroupSchedule<Group>> GetGroupSchedules()
        => _groupSchedules
            .AsReadOnly();

    public IReadOnlyList<GroupSchedule<OgnpGroup>> GetOgnpGroupSchedules()
        => _ognpGroupSchedules
            .AsReadOnly();

    private void ValidateGroupScheduleIntersections(GroupSchedule<Group> groupSchedule)
    {
        if (_groupSchedules.Any(gS =>
                groupSchedule.Schedule.IsOverlappingRoom(gS.Schedule) && !groupSchedule.Equals(gS)))
        {
            throw ScheduleServiceException.OverlappingRoom();
        }

        if (_groupSchedules.Any(gS =>
                groupSchedule.Schedule.IsOverlappingProfessor(gS.Schedule) && !groupSchedule.Equals(gS)))
        {
            throw ScheduleServiceException.OverlappingProfessor();
        }

        if (_ognpGroupSchedules.Any(gS => groupSchedule.Schedule.IsOverlappingRoom(gS.Schedule)))
        {
            throw ScheduleServiceException.OverlappingRoomAmongAll();
        }

        if (_ognpGroupSchedules.Any(gS => groupSchedule.Schedule.IsOverlappingProfessor(gS.Schedule)))
        {
            throw ScheduleServiceException.OverlappingProfessorAmongAll();
        }
    }

    private void ValidateOgnpGroupScheduleIntersections(GroupSchedule<OgnpGroup> ognpGroupSchedule)
    {
        if (_ognpGroupSchedules.Any(gS =>
                ognpGroupSchedule.Schedule.IsOverlappingRoom(gS.Schedule) && !ognpGroupSchedule.Equals(gS)))
        {
            throw ScheduleServiceException.OgnpOverlappingRoom();
        }

        if (_ognpGroupSchedules.Any(gS =>
                ognpGroupSchedule.Schedule.IsOverlappingProfessor(gS.Schedule) && !ognpGroupSchedule.Equals(gS)))
        {
            throw ScheduleServiceException.OgnpOverlappingProfessor();
        }

        if (_groupSchedules.Any(gS => ognpGroupSchedule.Schedule.IsOverlappingRoom(gS.Schedule)))
        {
            throw ScheduleServiceException.OverlappingRoomAmongAll();
        }

        if (_groupSchedules.Any(gS => ognpGroupSchedule.Schedule.IsOverlappingProfessor(gS.Schedule)))
        {
            throw ScheduleServiceException.OverlappingProfessorAmongAll();
        }
    }
}
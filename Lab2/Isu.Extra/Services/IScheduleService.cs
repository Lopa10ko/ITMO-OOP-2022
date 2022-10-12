using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public interface IScheduleService
{
    void AddSchedule(Group group, Schedule schedule);
    void AddSchedule(OgnpGroup ognpGroup, Schedule schedule);

    IReadOnlyList<GroupSchedule<Group>> GetGroupSchedules();
    IReadOnlyList<GroupSchedule<OgnpGroup>> GetOgnpGroupSchedules();
}
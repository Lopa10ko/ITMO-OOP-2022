using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Services;

public interface IScheduleService
{
    void AddSchedule(Group group, Schedule schedule);
    void AddSchedule(OgnpGroup ognpGroup, Schedule schedule);

    IReadOnlyList<KeyValuePair<Group, Schedule>> GetGroupSchedules();
    IReadOnlyList<KeyValuePair<OgnpGroup, Schedule>> GetOgnpGroupSchedules();
}
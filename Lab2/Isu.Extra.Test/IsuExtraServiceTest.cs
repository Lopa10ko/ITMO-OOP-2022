using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    private readonly IsuService _isuService;
    private readonly IsuExtraService _isuExtraService;
    private readonly ScheduleService _scheduleService;

    public IsuExtraServiceTest()
    {
        _isuService = new IsuService();
        _scheduleService = new ScheduleService();
        _isuExtraService = new IsuExtraService(_scheduleService);
    }

    [Theory]
    [InlineData("M32021", "КИБ3.1", "Lopatenko Gosha")]
    [InlineData("M3421", "ФОТ2.0", "Gopatenko Losha")]
    [InlineData("K320212", "ФИЗ4.2", "Kruglov Gosha")]
    public void AddStudentToGroupAndOgnp_StudentHasGroupAndOgnp(string groupName, string ognpGroupName, string name)
    {
        Group tGroup = _isuService.AddGroup(new GroupName(groupName));
        OgnpGroup tOgnpGroup = _isuExtraService.AddOgnpGroup(new OgnpGroupName(ognpGroupName));
        Assert.NotEqual(tGroup.GroupName, tOgnpGroup.OgnpGroupName);
        Student tStudent = _isuService.AddStudent(tGroup, name);
        Assert.Equal(tGroup, tStudent.Group);
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroup()
    {
        var tLessonEng1 = new Lesson("English", "Фредерикс Энди", 100, DayOfWeek.Monday, new TimeOnly(8, 20), new TimeOnly(9, 50));
        var tLessonEng2 = new Lesson("English", "Фредерикс Энди", 100, DayOfWeek.Monday, new TimeOnly(10, 0), new TimeOnly(11, 30));
        var tLessonPhys1 = new Lesson("Physics", "Музыченко", 100, DayOfWeek.Monday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        var tLessonPhys2 = new Lesson("Physics", "Тимофеева", 100, DayOfWeek.Monday, new TimeOnly(17, 0), new TimeOnly(18, 30));

        Group tGroup = _isuService.AddGroup(new GroupName("N3231"));
        OgnpGroup tOgnpGroup = _isuExtraService.AddOgnpGroup(new OgnpGroupName("КИБ3.1"));
        Schedule tGroupSchedule = new Schedule.ScheduleBuilder()
            .AddLesson(tLessonEng1)
            .AddLesson(tLessonEng2)
            .AddLesson(tLessonPhys1)
            .AddLesson(tLessonPhys2)
            .Build();
        _isuExtraService.AddGroupSchedule(tGroup, tGroupSchedule);

        Student tStudent = _isuService.AddStudent(tGroup, "Lopatenko Gosha");
        Assert.Equal(tGroup, tStudent.Group);
    }
}
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
        Schedule groupSchedule = new Schedule.ScheduleBuilder()
            .AddLesson(new Lesson("OPP", new Professor("Fredi Bats"), 3401, DayOfWeek.Monday, new TimeOnly(8, 20), new TimeOnly(9, 50)))
            .AddLesson(new Lesson("OPO", new Professor("Fredi Cats"), 3401, DayOfWeek.Monday, new TimeOnly(10, 0), new TimeOnly(11, 40)))
            .AddLesson(new Lesson("OOP", new Professor("Ronimizy"), 100, DayOfWeek.Monday, new TimeOnly(13, 30), new TimeOnly(15, 0)))
            .AddLesson(new Lesson("PPO", new Professor("O-l-e-g"), 100, DayOfWeek.Monday, new TimeOnly(15, 10), new TimeOnly(16, 40)))
            .AddLesson(new Lesson("ML", new Professor("Fredi Dogs"), 3401, DayOfWeek.Tuesday, new TimeOnly(8, 20), new TimeOnly(9, 50)))
            .AddLesson(new Lesson("Git", new Professor("Daddy Dogs"), 3401, DayOfWeek.Tuesday, new TimeOnly(10, 0), new TimeOnly(11, 40)))
            .AddLesson(new Lesson("Windows", new Professor("I-SER-I"), 100, DayOfWeek.Tuesday, new TimeOnly(13, 30), new TimeOnly(15, 0)))
            .AddLesson(new Lesson("БЖД", new Professor("menemi"), 100, DayOfWeek.Tuesday, new TimeOnly(15, 10), new TimeOnly(16, 40)))
            .Build();
        Schedule ognpSchedule = new Schedule.ScheduleBuilder()
            .AddLesson(new Lesson("OPP", new Professor("Fredi Bats"), 3401, DayOfWeek.Wednesday, new TimeOnly(8, 20), new TimeOnly(9, 50)))
            .AddLesson(new Lesson("OPO", new Professor("Fredi Cats"), 3401, DayOfWeek.Wednesday, new TimeOnly(10, 0), new TimeOnly(11, 40)))
            .AddLesson(new Lesson("OOP", new Professor("Ronimizy"), 100, DayOfWeek.Wednesday, new TimeOnly(13, 30), new TimeOnly(15, 0)))
            .AddLesson(new Lesson("PPO", new Professor("O-l-e-g"), 100, DayOfWeek.Wednesday, new TimeOnly(15, 10), new TimeOnly(16, 40)))
            .AddLesson(new Lesson("PhysEd", new Professor("Ermolay"), 3440, DayOfWeek.Tuesday, new TimeOnly(17, 00), new TimeOnly(18, 30)))
            .AddLesson(new Lesson("C/C++", new Professor("Zhuikov"), 3440, DayOfWeek.Tuesday, new TimeOnly(7, 21), new TimeOnly(7, 50)))
            .Build();
        Group tGroup = _isuService.AddGroup(new GroupName(groupName));
        _isuExtraService.AddGroupSchedule(tGroup, groupSchedule);
        OgnpGroup tOgnpGroup = _isuExtraService.AddOgnpGroup(new OgnpGroupName(ognpGroupName));
        _isuExtraService.AddOgnpGroupSchedule(tOgnpGroup, ognpSchedule);
        Assert.NotEqual(tGroup.GroupName, tOgnpGroup.GroupName);
        Student tStudent = _isuService.AddStudent(tGroup, name);
        Assert.Equal(tGroup, tStudent.Group);
        _isuExtraService.AddStudent(tOgnpGroup, tStudent);
        Assert.Contains(tStudent, tOgnpGroup.GetStudents);
    }

    [Theory]
    [InlineData("M32021", "КИБ3.1", "Lopatenko Gosha")]
    [InlineData("M3421", "ФОТ2.0", "Gopatenko Losha")]
    [InlineData("K320212", "ФИЗ4.2", "Kruglov Gosha")]
    public void AddStudentToOgnpRemoveFromOgnp_StudentHasGroup(string groupName, string ognpGroupName, string name)
    {
        Schedule groupSchedule = new Schedule.ScheduleBuilder()
            .AddLesson(new Lesson("PPO", new Professor("O-l-e-g"), 100, DayOfWeek.Wednesday, new TimeOnly(15, 10), new TimeOnly(16, 40)))
            .AddLesson(new Lesson("ML", new Professor("Fredi Dogs"), 3401, DayOfWeek.Tuesday, new TimeOnly(8, 20), new TimeOnly(9, 50)))
            .AddLesson(new Lesson("Git", new Professor("Daddy Dogs"), 3401, DayOfWeek.Wednesday, new TimeOnly(10, 0), new TimeOnly(11, 30)))
            .Build();
        Schedule ognpSchedule = new Schedule.ScheduleBuilder()
            .AddLesson(new Lesson("OPP", new Professor("Fredi Bats"), 3401, DayOfWeek.Wednesday, new TimeOnly(8, 20), new TimeOnly(9, 50)))
            .AddLesson(new Lesson("OPO", new Professor("Fredi Cats"), 3401, DayOfWeek.Wednesday, new TimeOnly(11, 40), new TimeOnly(13, 10)))
            .AddLesson(new Lesson("OOP", new Professor("Ronimizy"), 100, DayOfWeek.Wednesday, new TimeOnly(13, 30), new TimeOnly(15, 0)))
            .Build();
        Group tGroup = _isuService.AddGroup(new GroupName(groupName));
        _isuExtraService.AddGroupSchedule(tGroup, groupSchedule);
        var tOgnpGroupName = new OgnpGroupName(ognpGroupName);
        OgnpGroup tOgnpGroup = _isuExtraService.AddOgnpGroup(tOgnpGroupName);
        _isuExtraService.AddOgnpGroupSchedule(tOgnpGroup, ognpSchedule);
        Student tStudent = _isuService.AddStudent(tGroup, name);
        _isuExtraService.AddStudent(tOgnpGroup, tStudent);
        _isuExtraService.RemoveStudent(tOgnpGroup, tStudent);
        Assert.DoesNotContain(tStudent, _isuExtraService.FindStudents(tOgnpGroupName));
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroup()
    {
        var tLessonEng1 = new Lesson("English", new Professor("Frederiks A."), 100, DayOfWeek.Monday, new TimeOnly(8, 20), new TimeOnly(9, 50));
        var tLessonEng2 = new Lesson("English", new Professor("Frederiks A."), 100, DayOfWeek.Monday, new TimeOnly(10, 0), new TimeOnly(11, 30));
        var tLessonPhys1 = new Lesson("Physics", new Professor("Muzichenko"), 100, DayOfWeek.Monday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        var tLessonPhys2 = new Lesson("Physics", new Professor("Timofeeva"), 100, DayOfWeek.Monday, new TimeOnly(17, 0), new TimeOnly(18, 30));
        var tLessonEng12 = new Lesson("English", new Professor("Sara Danieli"), 101, DayOfWeek.Monday, new TimeOnly(8, 20), new TimeOnly(9, 50));
        var tLessonEng22 = new Lesson("English", new Professor("Sara Danieli"), 101, DayOfWeek.Monday, new TimeOnly(10, 0), new TimeOnly(11, 30));
        var tLessonPhys12 = new Lesson("Physics", new Professor("Zinchik"), 101, DayOfWeek.Monday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        var tLessonPhys22 = new Lesson("Physics", new Professor("Ronimizy"), 101, DayOfWeek.Monday, new TimeOnly(17, 0), new TimeOnly(18, 30));
        var tNewLessonEng12 = new Lesson("English", new Professor("Frederiks A."), 102, DayOfWeek.Monday, new TimeOnly(8, 20), new TimeOnly(9, 50));
        var tNewLessonEng22 = new Lesson("English", new Professor("Frederiks A."), 102, DayOfWeek.Monday, new TimeOnly(10, 0), new TimeOnly(11, 30));
        var tNewLessonPhys12 = new Lesson("Physics", new Professor("Timofeeva"), 102, DayOfWeek.Monday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        var tNewLessonPhys22 = new Lesson("Physics", new Professor("Muzichenko"), 102, DayOfWeek.Monday, new TimeOnly(17, 0), new TimeOnly(18, 30));

        Group tGroup1 = _isuService.AddGroup(new GroupName("N3231"));
        Group tGroup2 = _isuService.AddGroup(new GroupName("N3232"));
        OgnpGroup tOgnpGroup = _isuExtraService.AddOgnpGroup(new OgnpGroupName("КИБ3.1"));
        Schedule tGroupSchedule1 = new Schedule.ScheduleBuilder()
            .AddLesson(tLessonEng1)
            .AddLesson(tLessonEng2)
            .AddLesson(tLessonPhys1)
            .AddLesson(tLessonPhys2)
            .Build();
        Schedule tGroupSchedule2 = new Schedule.ScheduleBuilder()
            .AddLesson(tLessonEng12)
            .AddLesson(tLessonEng22)
            .AddLesson(tLessonPhys12)
            .AddLesson(tLessonPhys22)
            .Build();
        Schedule tNewGroupSchedule1 = new Schedule.ScheduleBuilder()
            .AddLesson(tNewLessonEng12)
            .AddLesson(tNewLessonEng22)
            .AddLesson(tNewLessonPhys12)
            .AddLesson(tNewLessonPhys22)
            .Build();
        _isuExtraService.AddGroupSchedule(tGroup1, tGroupSchedule1);
        _isuExtraService.AddGroupSchedule(tGroup2, tGroupSchedule2);
        _isuExtraService.AddGroupSchedule(tGroup1, tNewGroupSchedule1);
        Student tStudent = _isuService.AddStudent(tGroup1, "Lopatenko Gosha");
    }
}
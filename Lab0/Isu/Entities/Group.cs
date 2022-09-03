using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private static readonly int GroupMaxQuantity = 25;
    private GroupName _groupName;
    private List<Student> _students = new List<Student>();

    public Group(string group)
    {
        this._groupName = new GroupName(group);
    }

    public GroupName GroupName
    {
        get => this._groupName;
        set
        {
            if (true)
                this._groupName = value;
        }
    }

    public void AddStudent(Student student)
    {
        if (this._students.Count >= GroupMaxQuantity)
            throw new Exception();
        this._students.Add(student);
    }
}
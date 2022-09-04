using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private static int groupMaxQuantity = 25;
    private GroupName _groupName;
    private List<Student> _students = new List<Student>();

    public Group(GroupName groupName)
    {
        this._groupName = groupName;
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
        if (this._students.Count >= groupMaxQuantity)
            throw new Exception();
        this._students.Add(student);

        // student.ChangeGroup(this.GroupName)
    }
}
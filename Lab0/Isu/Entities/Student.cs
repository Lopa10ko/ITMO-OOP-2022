using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Student
{
    private static ulong _idCurrent = 0;
    private Group _group;
    private string _name;
    private ulong _idIsu;

    public Student()
        : this("Undefined", new Group("Z0000"))
    { }

    public Student(string name, string group)
    {
        this._idIsu = ++_idCurrent;
        this._name = name;
        this._group = group;
    }

    public void Print()
    {
        Console.WriteLine($"name: {_name}  ISU: {_idIsu} Group: {_groupName}");
    }
}
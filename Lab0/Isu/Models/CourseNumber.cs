using Isu.Tools;

namespace Isu.Models;

public class CourseNumber
{
    private int _courseNumber;

    public CourseNumber(int courseNumber)
        => this.CourseNum = courseNumber;

    public int CourseNum
    {
        get => this._courseNumber;
        private set
        {
            if (!(value >= 1 && value <= 4))
                throw new CourseNumberOutOfRangeException();
            this._courseNumber = value;
        }
    }
}
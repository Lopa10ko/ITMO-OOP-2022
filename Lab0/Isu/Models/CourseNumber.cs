namespace Isu.Models;

public class CourseNumber
{
    private int _courseNumber;

    public CourseNumber(int courseNumber)
    {
        if (!(courseNumber >= 1 && courseNumber <= 4))
            throw new OverflowException();
        this._courseNumber = courseNumber;
    }
}
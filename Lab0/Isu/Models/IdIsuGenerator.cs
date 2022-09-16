using Isu.Tools;

namespace Isu.Models;

public class IdIsuGenerator
{
    private const int MinIdIsuNumber = 100000;
    private const int MaxIdIsuNumber = 999999;
    private int _idIsu;

    public IdIsuGenerator()
    {
        _idIsu = MinIdIsuNumber;
    }

    public int IdIsu
    {
        get => _idIsu;
        private set
        {
            ValidateIdIsu(value);
            _idIsu = value;
        }
    }

    public void IncrementIdIsu()
        => ++IdIsu;

    private static void ValidateIdIsu(int value)
    {
        if (value is < MinIdIsuNumber or > MaxIdIsuNumber)
        {
            throw IsuException.OutOfRangeException(value, "IdIsu");
        }
    }
}
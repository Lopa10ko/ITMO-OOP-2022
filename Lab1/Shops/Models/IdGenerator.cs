﻿namespace Shops.Models;

public class IdGenerator
{
    private const int MinIdIsuNumber = 100000;
    private const int MaxIdIsuNumber = 999999;
    private int _idIsu;

    public IdGenerator()
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

    public void NextId()
        => ++IdIsu;

    private static void ValidateIdIsu(int value)
    {
        if (value is < MinIdIsuNumber or > MaxIdIsuNumber)
        {
            throw GroupNameException.OutOfRangeException(value, "IdIsu");
        }
    }
}
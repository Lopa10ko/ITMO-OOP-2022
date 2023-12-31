﻿namespace Shops.Tools;

public class NamingException : ShopsException
{
    private NamingException(string errorMessage)
        : base(errorMessage) { }

    public static NamingException InvalidName(string address)
        => new ($"Address {address} is not formatted correctly");
}
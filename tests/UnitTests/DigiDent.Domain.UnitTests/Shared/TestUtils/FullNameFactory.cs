﻿using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.UnitTests.Shared.TestUtils;

public class FullNameFactory
{
    public static HashSet<(string, string)> ValidNames =>
    [
        ("John", "Doe"),
        ("Emma", "Johnson"),
        ("Christopher", "Smith"),
        ("Olivia", "Williams"),
        ("William", "Brown"),
        ("Ava", "Jones")
    ];

    public static HashSet<(string, string)> InvalidNames() =>
    [
        ("John", ""),
        ("", "Doe"),
        ("J", "D"),
        ("John", "".PadLeft(51, 'D')),
    ];
    
    public static FullName GetValidFullName()
    {
        var (firstName, lastName) = ValidNames.First();
        return FullName.Create(firstName, lastName).Value!;
    }
}
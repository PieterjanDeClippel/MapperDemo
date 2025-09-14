// See https://aka.ms/new-console-template for more information
using MapperDemo;
using MintPlayer.Mapper.Attributes;
using System.Diagnostics;

var person = new Person
{
    FirstName = "John",
    LastName = "Doe",
    MainAddress = new Address
    {
        Street = "123 Main St",
        City = "Anytown",
        Country = "USA",
        Extras = ["Near the park", "Blue house"],
    },
    ContactInfos =
    [
        new ContactInfo
        {
            ContactType = EContactType.Email,
            Value = "info@example.com",
        },
        new ContactInfo
        {
            ContactType = EContactType.Phone,
            Value = "+1 23/45.67.89",
        },
    ],
    Notes =
    [
        "Note 1",
        "Note 2",
    ],
    Weight = 70.5,
};

var dto = person.MapToPersonDto();
Debugger.Break();

[GenerateMapper(typeof(PersonDto))]
public class Person
{
    [MapperAlias(nameof(PersonDto.Voornaam))]
    public string FirstName { get; set; }

    [MapperAlias(nameof(PersonDto.Achternaam))]
    public string LastName { get; set; }

    [MapperAlias(nameof(PersonDto.HoofdAdres))]
    public Address MainAddress { get; set; }

    [MapperAlias(nameof(PersonDto.Notities))]
    public List<string> Notes { get; set; }

    [MapperAlias(nameof(PersonDto.ContactGegevens))]
    public ContactInfo[] ContactInfos { get; set; }

    [MapperAlias(nameof(PersonDto.Gewicht))]
    public double Weight { get; set; }
}

public class PersonDto
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public AddressDto HoofdAdres { get; set; }
    public List<string> Notities { get; set; }
    public ContactInfoDto[] ContactGegevens { get; set; }

    public string Gewicht { get; set; }
}

[GenerateMapper(typeof(AddressDto))]
public class Address
{
    [MapperAlias(nameof(AddressDto.Straat))]
    public string Street { get; set; }

    [MapperAlias(nameof(AddressDto.Stad))]
    public string City { get; set; }

    [MapperAlias(nameof(AddressDto.Land))]
    public string Country { get; set; }

    public List<string> Extras { get; set; }
}

public class AddressDto
{
    public string Straat { get; set; }
    public string Stad { get; set; }
    public string Land { get; set; }
    public List<string> Extras { get; set; }
}

public enum EContactType
{
    None,
    Email,
    Phone,
    Fax
}

[GenerateMapper(typeof(ContactInfoDto))]
public class ContactInfo
{
    [MapperAlias(nameof(ContactInfoDto.Type))]
    public EContactType ContactType { get; set; }

    [MapperAlias(nameof(ContactInfoDto.Waarde))]
    public string Value { get; set; }
}

public class ContactInfoDto
{
    public EContactType Type { get; set; }
    public string Waarde { get; set; }
}


public static class Conversions
{
    [MapperConversion]
    public static int? StringToNullableInt(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;
        if (int.TryParse(input, out int result))
            return result;
        return null;
    }

    [MapperConversion]
    public static string? NullableIntToString(int? input)
    {
        return input?.ToString();
    }

    [MapperConversion]
    public static double StringToDouble(string input)
    {
        if (double.TryParse(input, out double result))
            return result;
        return 0;
    }

    [MapperConversion]
    public static string DoubleToString(double input)
    {
        return input.ToString();
    }
}

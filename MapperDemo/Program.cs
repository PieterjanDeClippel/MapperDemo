// See https://aka.ms/new-console-template for more information
using MintPlayer.Mapper.Attributes;

Console.WriteLine("Hello, World!");

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
}

public class PersonDto
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public AddressDto HoofdAdres { get; set; }
    public List<string> Notities { get; set; }
    public ContactInfoDto[] ContactGegevens { get; set; }
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
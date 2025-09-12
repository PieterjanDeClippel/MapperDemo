# Mapper demo
This project demonstrates a .NET Source Generator that generates mappers for you.

Input

```cs
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
```

Generated code

```cs
namespace MapperDemo
{
    public static class MapperExtensions
    {
        public static global::Person MapToPerson(this global::PersonDto input)
        {
            return new global::Person()
            {
                FirstName = input.Voornaam,
                LastName = input.Achternaam,
                MainAddress = input.HoofdAdres.MapToAddress(),
                Notes = input.Notities == null ? null : input.Notities.ToList(),
                ContactInfos = input.ContactGegevens == null ? null : input.ContactGegevens.Select(x => x.MapToContactInfo()).ToArray(),
            };
        }
        public static global::PersonDto MapToPersonDto(this global::Person input)
        {
            return new global::PersonDto()
            {
                Voornaam = input.FirstName,
                Achternaam = input.LastName,
                HoofdAdres = input.MainAddress.MapToAddressDto(),
                Notities = input.Notes == null ? null : input.Notes.ToList(),
                ContactGegevens = input.ContactInfos == null ? null : input.ContactInfos.Select(x => x.MapToContactInfoDto()).ToArray(),
            };
        }
        public static global::System.Collections.Generic.IEnumerable<global::Person> MapToPerson(this global::System.Collections.Generic.IEnumerable<global::PersonDto> input)
        {
            return input.Select(x => x.MapToPerson());
        }
        public static global::System.Collections.Generic.IEnumerable<global::PersonDto> MapToPersonDto(this global::System.Collections.Generic.IEnumerable<global::Person> input)
        {
            return input.Select(x => x.MapToPersonDto());
        }
        public static global::Address MapToAddress(this global::AddressDto input)
        {
            return new global::Address()
            {
                Street = input.Straat,
                City = input.Stad,
                Country = input.Land,
                Extras = input.Extras == null ? null : input.Extras.ToList(),
            };
        }
        public static global::AddressDto MapToAddressDto(this global::Address input)
        {
            return new global::AddressDto()
            {
                Straat = input.Street,
                Stad = input.City,
                Land = input.Country,
                Extras = input.Extras == null ? null : input.Extras.ToList(),
            };
        }
        public static global::System.Collections.Generic.IEnumerable<global::Address> MapToAddress(this global::System.Collections.Generic.IEnumerable<global::AddressDto> input)
        {
            return input.Select(x => x.MapToAddress());
        }
        public static global::System.Collections.Generic.IEnumerable<global::AddressDto> MapToAddressDto(this global::System.Collections.Generic.IEnumerable<global::Address> input)
        {
            return input.Select(x => x.MapToAddressDto());
        }
        public static global::ContactInfo MapToContactInfo(this global::ContactInfoDto input)
        {
            return new global::ContactInfo()
            {
                ContactType = input.Type,
                Value = input.Waarde,
            };
        }
        public static global::ContactInfoDto MapToContactInfoDto(this global::ContactInfo input)
        {
            return new global::ContactInfoDto()
            {
                Type = input.ContactType,
                Waarde = input.Value,
            };
        }
        public static global::System.Collections.Generic.IEnumerable<global::ContactInfo> MapToContactInfo(this global::System.Collections.Generic.IEnumerable<global::ContactInfoDto> input)
        {
            return input.Select(x => x.MapToContactInfo());
        }
        public static global::System.Collections.Generic.IEnumerable<global::ContactInfoDto> MapToContactInfoDto(this global::System.Collections.Generic.IEnumerable<global::ContactInfo> input)
        {
            return input.Select(x => x.MapToContactInfoDto());
        }
    }
}
```

# Mapper demo
This project demonstrates a .NET Source Generator that generates mappers for you.

Input

```cs
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
```

Generated code

```cs
namespace MapperDemo
{
    public static class MapperExtensions
    {
        public static TDest? ConvertProperty<TSource, TDest>(TSource? source)
        {
            if (source is null)
                return default;
            
            object? result;
            
            switch ((typeof(TSource), typeof(TDest)))
            {
                case (global::System.Type sourceType, global::System.Type destType) when sourceType == typeof(string) && destType == typeof(int?):
                    result = global::Conversions.StringToNullableInt((string)(object)source);
                    break;
                case (global::System.Type sourceType, global::System.Type destType) when sourceType == typeof(int?) && destType == typeof(string):
                    result = global::Conversions.NullableIntToString((int?)(object)source);
                    break;
                case (global::System.Type sourceType, global::System.Type destType) when sourceType == typeof(string) && destType == typeof(double):
                    result = global::Conversions.StringToDouble((string)(object)source);
                    break;
                case (global::System.Type sourceType, global::System.Type destType) when sourceType == typeof(double) && destType == typeof(string):
                    result = global::Conversions.DoubleToString((double)(object)source);
                    break;
                default:
                    throw new NotSupportedException($"Conversion from {typeof(TSource)} to {typeof(TDest)} is not supported.");
            }
            
            return (TDest?)(object?)result;
        }
        public static global::Person MapToPerson(this global::PersonDto input)
        {
            return new global::Person()
            {
                Name = input.Naam,
                Age = input.Leeftijd,
                Address = input.Adres.MapToAddress(),
                ContactInfos = input.Contactgegevens == null ? null : input.Contactgegevens.Select(x => x.MapToContactInfo()).ToList(),
                Notes = input.Notities == null ? null : input.Notities.ToList(),
                Weight = ConvertProperty<string, double>(input.Gewicht),
            };
        }
        public static global::PersonDto MapToPersonDto(this global::Person input)
        {
            return new global::PersonDto()
            {
                Naam = input.Name,
                Leeftijd = input.Age,
                Adres = input.Address.MapToAddressDto(),
                Contactgegevens = input.ContactInfos == null ? null : input.ContactInfos.Select(x => x.MapToContactInfoDto()).ToList(),
                Notities = input.Notes == null ? null : input.Notes.ToList(),
                Gewicht = ConvertProperty<double, string>(input.Weight),
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
                Street = input.Straatnaam,
                City = input.Stad,
            };
        }
        public static global::AddressDto MapToAddressDto(this global::Address input)
        {
            return new global::AddressDto()
            {
                Straatnaam = input.Street,
                Stad = input.City,
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
        public static global::ContactInfoDto MapToContactInfoDto(this global::ContactInfo input)
        {
            return new global::ContactInfoDto()
            {
                Soort = input.Type,
                Waarde = input.Value,
            };
        }
        public static global::ContactInfo MapToContactInfo(this global::ContactInfoDto input)
        {
            return new global::ContactInfo()
            {
                Type = input.Soort,
                Value = input.Waarde,
            };
        }
        public static global::System.Collections.Generic.IEnumerable<global::ContactInfoDto> MapToContactInfoDto(this global::System.Collections.Generic.IEnumerable<global::ContactInfo> input)
        {
            return input.Select(x => x.MapToContactInfoDto());
        }
        public static global::System.Collections.Generic.IEnumerable<global::ContactInfo> MapToContactInfo(this global::System.Collections.Generic.IEnumerable<global::ContactInfoDto> input)
        {
            return input.Select(x => x.MapToContactInfo());
        }
    }
}

```

## Result
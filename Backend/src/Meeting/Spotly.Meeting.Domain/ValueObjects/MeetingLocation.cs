using System.Text;

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Объект-значение, которая представляет место проведения встречи.
/// </summary>
public sealed class MeetingLocation : IEquatable<MeetingLocation>, IComparable<MeetingLocation>
{
    /// <summary>
    /// Готовая строка для UI (полностью отформатированный адрес).
    /// </summary>
    public string Formatted { get; }

    /// <summary>
    /// Код страны в формате ISO2.
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// Регион (например, область или штат).
    /// </summary>
    public string? Region { get; }

    /// <summary>
    /// Город.
    /// </summary>
    public string? City { get; }

    /// <summary>
    /// Улица.
    /// </summary>
    public string? Street { get; }

    /// <summary>
    /// Номер дома.
    /// </summary>
    public string? HouseNumber { get; }

    /// <summary>
    /// Номер квартиры или офиса (если есть).
    /// </summary>
    public string? Apartment { get; }

    /// <summary>
    /// Географическая широта.
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Географическая долгота.
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// Идентификатор провайдера геокодинга (например, PlaceId).
    /// </summary>
    public string? ProviderId { get; }

    private MeetingLocation(
        string formatted,
        string country,
        string? region,
        string? city,
        string? street,
        string? houseNumber,
        string? apartment,
        double latitude,
        double longitude,
        string? providerId)
    {
        Formatted = formatted;
        Country = country;
        Region = region;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        Apartment = apartment;
        Latitude = latitude;
        Longitude = longitude;
        ProviderId = providerId;
    }

    /// <summary>
    /// Фабричный метод для создания нового экземпляра <see cref="MeetingLocation"/>.
    /// </summary>
    public static MeetingLocation From(
        string country,
        string? region,
        string? city,
        string? street,
        string? houseNumber,
        string? apartment,
        double latitude,
        double longitude,
        string? providerId)
    {
        var formatted = ToFormatted(country, region, city, street, houseNumber, apartment);
        return new MeetingLocation(formatted,
            country,
            region,
            city,
            street,
            houseNumber,
            apartment,
            latitude,
            longitude,
            providerId);
    }

    /// <summary>
    /// Формирует строковое представление адреса.
    /// </summary>
    public static string ToFormatted(
        string country,
        string? region,
        string? city,
        string? street,
        string? houseNumber,
        string? apartment)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(country))
        {
            sb.Append(country).Append(", ");
        }

        if (!string.IsNullOrWhiteSpace(region))
        {
            sb.Append(region).Append(", ");
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            sb.Append(city).Append(", ");
        }

        if (!string.IsNullOrWhiteSpace(street) || !string.IsNullOrWhiteSpace(houseNumber))
        {
            sb.Append(street).Append(' ').Append(houseNumber).Append(", ");
        }

        if (!string.IsNullOrWhiteSpace(apartment))
        {
            sb.Append("кв. ").Append(apartment);
        }

        return sb.ToString().TrimEnd(',', ' ');
    }

    /// <inheritdoc/>
    public bool Equals(MeetingLocation? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Country == other.Country
               && Region == other.Region
               && City == other.City
               && Street == other.Street
               && HouseNumber == other.HouseNumber
               && Apartment == other.Apartment
               && Latitude.Equals(other.Latitude)
               && Longitude.Equals(other.Longitude)
               && ProviderId == other.ProviderId;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as MeetingLocation);

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Country,
            Region,
            City,
            Street,
            HouseNumber,
            Apartment,
            ProviderId);
    }


    /// <inheritdoc/>
    public int CompareTo(MeetingLocation? other)
    {
        if (other is null) return 1;
        return string.Compare(Formatted, other.Formatted, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override string ToString() => Formatted;

    public static bool operator ==(MeetingLocation? left, MeetingLocation? right) => Equals(left, right);

    public static bool operator !=(MeetingLocation? left, MeetingLocation? right) => !Equals(left, right);
}

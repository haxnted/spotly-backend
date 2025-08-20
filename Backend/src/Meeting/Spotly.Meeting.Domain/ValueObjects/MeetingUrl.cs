using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.ValueObjects;

/// <summary>
/// Объект-значение, которое представляет ссылку на онлайн-встречу.
/// </summary>
public sealed class MeetingUrl : IEquatable<MeetingUrl>, IComparable<MeetingUrl> 
{
    /// <summary>
    /// Ссылка на онлайн-встречу.
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Приватный конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private MeetingUrl(string value) => Value = value;

    /// <summary>
    /// Фабричный метод для создания экземпляра <see cref="MeetingUrl"/>.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <exception cref="MeetingException">
    /// Если ссылка пустая, либо превышает максимальную длину.
    /// </exception>
    public static MeetingUrl Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
        {
            throw new MeetingException("Meeting url can't be empty."); 
        }

        // if (value.Length > MAX_URL_LENGTH)
        // {
        //     throw new MeetingException($"Meeting url length must be less than {MAX_URL_LENGTH} symbols."); 
        // }
        
        if (Uri.TryCreate(value, UriKind.Absolute, out var uriResult) is not true
            || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            throw new MeetingException("Meeting url must be a valid absolute HTTP/HTTPS link.");
        }
        
        return new MeetingUrl(value);
    }
    
    /// <inheritdoc/>
    public bool Equals(MeetingUrl? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public int CompareTo(MeetingUrl? other)
    {
        if (other is null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}

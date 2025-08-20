using System.Text.RegularExpressions;
using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.ValueObjects;

/// <summary>
/// Объект-значение, который представляет собой заголовок встречи.
/// </summary>
public sealed class MeetingTitle : IEquatable<MeetingTitle>, IComparable<MeetingTitle>
{
    /// <summary>
    /// Регулярное выражение для проверки заголовка встречи на допустимые символы.
    /// </summary>
    private static readonly Regex _regex = new("^[a-zA-Z0-9 ]+$");

    /// <summary>
    /// Минимальная длина заголовка встречи.
    /// </summary>
    private static readonly int MIN_TITLE_LENGTH = 5;

    /// <summary>
    /// Максимальная длина заголовка встречи.
    /// </summary>
    private static readonly int MAX_TITLE_LENGTH = 200;

    /// <summary>
    /// Значение заголовка встречи.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private MeetingTitle(string value) => Value = value;


    /// <summary>
    /// Фабричный метод для создания заголовка встречи.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <returns>Экземпляр <see cref="MeetingTitle"/></returns>
    /// <exception cref="MeetingException">
    /// Если заголовок пустой, 
    /// </exception>
    public static MeetingTitle From(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
        {
            throw new MeetingException("Meeting title can't be empty.");
        }

        if (value.Length < MIN_TITLE_LENGTH || value.Length > MAX_TITLE_LENGTH)
        {
            throw new MeetingException(
                $"Meeting title length must be between {MIN_TITLE_LENGTH} and {MAX_TITLE_LENGTH} symbols.");
        }

        if (_regex.IsMatch(value) is false)
        {
            throw new MeetingException("Meeting title can contain only letters, numbers and spaces."); 
        }
        
        return new MeetingTitle(value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as MeetingTitle);

    /// <inheritdoc/>
    public bool Equals(MeetingTitle? other) =>
        other is not null && Value.Equals(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(MeetingTitle? other)
    {
        if (other is null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}

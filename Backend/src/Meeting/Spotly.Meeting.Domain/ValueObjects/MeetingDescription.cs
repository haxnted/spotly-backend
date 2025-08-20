using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Объект-значение, который представляет собой описание встречи.
/// </summary>
public sealed class MeetingDescription : IEquatable<MeetingDescription>, IComparable<MeetingDescription>
{
    /// <summary>
    /// Минимальная длина описания встречи.
    /// </summary>
    private static readonly int MIN_DESCRIPTION_LENGTH = 5;

    /// <summary>
    /// Максимальная длина описания встречи.
    /// </summary>
    private static readonly int MAX_DESCRIPTION_LENGTH = 700;

    /// <summary>
    /// Значение описания встречи.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private MeetingDescription(string value) => Value = value;


    /// <summary>
    /// Фабричный метод для создания описания встречи.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <returns>Экземпляр <see cref="MeetingDescription"/></returns>
    /// <exception cref="MeetingException">
    /// Если заголовок пустой, 
    /// </exception>
    public static MeetingDescription From(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
        {
            throw new MeetingException("Meeting description can't be empty.");
        }

        if (value.Length < MIN_DESCRIPTION_LENGTH || value.Length > MAX_DESCRIPTION_LENGTH)
        {
            throw new MeetingException(
                $"Meeting description length must be between {MIN_DESCRIPTION_LENGTH} and {MAX_DESCRIPTION_LENGTH} symbols.");
        }
        
        return new MeetingDescription(value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as MeetingDescription);

    /// <inheritdoc/>
    public bool Equals(MeetingDescription? other) =>
        other is not null && Value.Equals(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(MeetingDescription? other)
    {
        if (other is null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}

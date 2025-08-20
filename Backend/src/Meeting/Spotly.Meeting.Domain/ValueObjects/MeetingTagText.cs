using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Объект-значение, который представляет собой описание тега.
/// </summary>
public sealed class MeetingTagText : IEquatable<MeetingTagText>, IComparable<MeetingTagText>
{
    /// <summary>
    /// Минимальная длина описания тега.
    /// </summary>
    private static readonly int MIN_DESCRIPTION_LENGTH = 3;

    /// <summary>
    /// Максимальная длина описания тега.
    /// </summary>
    private static readonly int MAX_DESCRIPTION_LENGTH = 15;

    /// <summary>
    /// Значение описания тега.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private MeetingTagText(string value) => Value = value;


    /// <summary>
    /// Фабричный метод для создания описания тега.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <returns>Экземпляр <see cref="MeetingTagText"/></returns>
    /// <exception cref="MeetingException">
    /// Если тег пустой.
    /// Если тег слишком короткий или слишком длинный.
    /// </exception>
    public static MeetingTagText From(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
        {
            throw new MeetingException("Meeting tag can't be empty.");
        }

        if (value.Length < MIN_DESCRIPTION_LENGTH || value.Length > MAX_DESCRIPTION_LENGTH)
        {
            throw new MeetingException(
                $"Meeting tag length must be between {MIN_DESCRIPTION_LENGTH} and {MAX_DESCRIPTION_LENGTH} symbols.");
        }
        
        return new MeetingTagText(value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as MeetingTagText);

    /// <inheritdoc/>
    public bool Equals(MeetingTagText? other) =>
        other is not null && Value.Equals(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(MeetingTagText? other)
    {
        if (other is null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}

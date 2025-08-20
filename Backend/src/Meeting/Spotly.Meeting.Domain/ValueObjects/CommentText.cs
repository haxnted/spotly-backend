using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Объект-значение, который представляет собой описание комментария.
/// </summary>
public sealed class CommentText : IEquatable<CommentText>, IComparable<CommentText>
{
    /// <summary>
    /// Минимальная длина описания комментария.
    /// </summary>
    private static readonly int MIN_DESCRIPTION_LENGTH = 5;

    /// <summary>
    /// Максимальная длина описания комментария.
    /// </summary>
    private static readonly int MAX_DESCRIPTION_LENGTH = 1000;

    /// <summary>
    /// Значение описания комментария.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private CommentText(string value) => Value = value;


    /// <summary>
    /// Фабричный метод для создания описания комментария.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <returns>Экземпляр <see cref="CommentText"/></returns>
    /// <exception cref="MeetingException">
    /// Если заголовок пустой.
    /// Если заголовок слишком короткий или слишком длинный.
    /// </exception>
    public static CommentText From(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
        {
            throw new MeetingException("Meeting comment can't be empty.");
        }

        if (value.Length < MIN_DESCRIPTION_LENGTH || value.Length > MAX_DESCRIPTION_LENGTH)
        {
            throw new MeetingException(
                $"Meeting comment length must be between {MIN_DESCRIPTION_LENGTH} and {MAX_DESCRIPTION_LENGTH} symbols.");
        }
        
        return new CommentText(value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as CommentText);

    /// <inheritdoc/>
    public bool Equals(CommentText? other) =>
        other is not null && Value.Equals(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(CommentText? other)
    {
        if (other is null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}

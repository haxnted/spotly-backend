using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Объект-значение, который представляет собой идентификатор комментария (обёртка над <see cref="Guid"/>).
/// </summary>
public readonly struct MeetingCommentId : IEquatable<MeetingCommentId>
{
    /// <summary>
    /// Значение идентификатора комментария.
    /// </summary>
    public Guid Value { get; init; }
    
    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private MeetingCommentId(Guid value) => Value = value;

    /// <summary>
    /// Фабричный метод для создания идентификатора комментария.
    /// </summary>
    /// <param name="id">Значение.</param>
    /// <returns>Экземпляр <see cref="MeetingCommentId"/></returns>
    /// <exception cref="MeetingException">
    /// Если идентификатор пустой
    /// </exception>
    public static MeetingCommentId From(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new MeetingException("Meeting comment id can't be empty.");
        }
        return new MeetingCommentId(id); 
    }

    /// <inheritdoc/>
    public bool Equals(MeetingCommentId other)
    {
        return Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is MeetingCommentId other && Equals(other);
    }
    
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    /// <summary>
    /// Оператор сравнения на равенство.
    /// </summary>
    public static bool operator ==(MeetingCommentId left, MeetingCommentId right) => left.Equals(right);

    /// <summary>
    /// Оператор сравнения на неравенство.
    /// </summary>
    public static bool operator !=(MeetingCommentId left, MeetingCommentId right) => !left.Equals(right);
}

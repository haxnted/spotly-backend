using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.ValueObjects;

/// <summary>
/// Объект-значение, который представляет собой идентификатор тега (обёртка над <see cref="Guid"/>).
/// </summary>
public readonly struct MeetingTagId : IEquatable<MeetingTagId>
{
    /// <summary>
    /// Значение идентификатора тега.
    /// </summary>
    public Guid Value { get; init; }
    
    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private MeetingTagId(Guid value) => Value = value;

    /// <summary>
    /// Фабричный метод для создания идентификатора тега.
    /// </summary>
    /// <param name="id">Значение.</param>
    /// <returns>Экземпляр <see cref="MeetingTagId"/></returns>
    /// <exception cref="MeetingException">
    /// Если идентификатор пустой
    /// </exception>
    public static MeetingTagId From(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new MeetingException("Meeting tag id can't be empty.");
        }
        return new MeetingTagId(id); 
    }

    /// <inheritdoc/>
    public bool Equals(MeetingTagId other)
    {
        return Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is MeetingTagId other && Equals(other);
    }
    
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    /// <summary>
    /// Оператор сравнения на равенство.
    /// </summary>
    public static bool operator ==(MeetingTagId left, MeetingTagId right) => left.Equals(right);

    /// <summary>
    /// Оператор сравнения на неравенство.
    /// </summary>
    public static bool operator !=(MeetingTagId left, MeetingTagId right) => !left.Equals(right);
}

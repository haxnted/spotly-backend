using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Объект-значение, который представляет собой идентификатор встречи (обёртка над <see cref="Guid"/>).
/// </summary>
public readonly struct MeetingId : IEquatable<MeetingId>
{
    /// <summary>
    /// Значение идентификатора встречи.
    /// </summary>
    public Guid Value { get; init; }
    
    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private MeetingId(Guid value) => Value = value;

    /// <summary>
    /// Фабричный метод для создания идентификатора встречи.
    /// </summary>
    /// <param name="id">Значение.</param>
    /// <returns>Экземпляр <see cref="MeetingId"/></returns>
    /// <exception cref="MeetingException">
    /// Если идентификатор пустой
    /// </exception>
    public static MeetingId From(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new MeetingException("Meeting id can't be empty.");
        }
        return new MeetingId(id); 
    }

    /// <inheritdoc/>
    public bool Equals(MeetingId other)
    {
        return Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is MeetingId other && Equals(other);
    }
    
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    /// <summary>
    /// Оператор сравнения на равенство.
    /// </summary>
    public static bool operator ==(MeetingId left, MeetingId right) => left.Equals(right);

    /// <summary>
    /// Оператор сравнения на неравенство.
    /// </summary>
    public static bool operator !=(MeetingId left, MeetingId right) => !left.Equals(right);
}

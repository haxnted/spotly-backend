using Spotly.Meeting.Domain.Exceptions;

namespace Spotly.Meeting.Domain.ValueObjects;

/// <summary>
/// Объект-значение, который представляет собой идентификатор встречи (обёртка над <see cref="Guid"/>).
/// </summary>
public readonly struct ParticipantId : IEquatable<ParticipantId>
{
    /// <summary>
    /// Значение идентификатора встречи.
    /// </summary>
    public Guid Value { get; init; }
    
    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private ParticipantId(Guid value) => Value = value;

    /// <summary>
    /// Фабричный метод для создания идентификатора встречи.
    /// </summary>
    /// <param name="id">Значение.</param>
    /// <returns>Экземпляр <see cref="ParticipantId"/></returns>
    /// <exception cref="MeetingException">
    /// Если идентификатор пустой
    /// </exception>
    public static ParticipantId From(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new MeetingException("Meeting id can't be empty.");
        }
        return new ParticipantId(id); 
    }

    /// <inheritdoc/>
    public bool Equals(ParticipantId other)
    {
        return Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ParticipantId other && Equals(other);
    }
    
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    /// <summary>
    /// Оператор сравнения на равенство.
    /// </summary>
    public static bool operator ==(ParticipantId left, ParticipantId right) => left.Equals(right);

    /// <summary>
    /// Оператор сравнения на неравенство.
    /// </summary>
    public static bool operator !=(ParticipantId left, ParticipantId right) => !left.Equals(right);
}

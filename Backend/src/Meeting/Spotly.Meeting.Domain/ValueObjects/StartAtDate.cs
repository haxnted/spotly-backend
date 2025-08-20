namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Объект-значение, которое представляет дату и время начала встречи.
/// </summary>
public sealed class StartAtDate : IEquatable<StartAtDate>, IComparable<StartAtDate>
{
    /// <summary>
    /// Дата и время начала встречи.
    /// </summary>
    public DateTime Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    public StartAtDate(DateTime value) => Value = value;
    
    /// <summary>
    /// Возвращает новый экземпляр <see cref="StartAtDate"/>.
    /// </summary>
    /// <param name="value">Значение.</param>
    public static StartAtDate Create() => new(DateTime.UtcNow);

    /// <summary>
    /// Создает экземпляр <see cref="StartAtDate"/> из даты и времени.
    /// </summary>
    /// <param name="startAt">Дата и время.</param>
    public static StartAtDate From(DateTime startAt) => new(startAt);
   
    /// <inheritdoc/>
    public override string ToString() => Value.ToString("u");
    
    /// <inheritdoc/>
    public bool Equals(StartAtDate? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public int CompareTo(StartAtDate? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }

    
}

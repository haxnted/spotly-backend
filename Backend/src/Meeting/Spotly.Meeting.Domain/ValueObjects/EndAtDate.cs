namespace Spotly.Meeting.Domain.ValueObjects;

/// <summary>
/// Объект-значение, которое представляет дату и время окончания встречи.
/// </summary>
public sealed class EndAtDate : IEquatable<EndAtDate>, IComparable<EndAtDate>
{
    /// <summary>
    /// Дата и время окончания встречи.
    /// </summary>
    public DateTime Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    public EndAtDate(DateTime value) => Value = value;

    /// <summary>
    /// Возвращает экземпляр <see cref="EndAtDate"/>.
    /// </summary>
    public static EndAtDate Create() => new(DateTime.UtcNow);

    /// <summary>
    /// Создает экземпляр <see cref="EndAtDate"/> из даты и времени.
    /// </summary>
    /// <param name="startAt">Дата и время.</param>
    public static EndAtDate From(DateTime startAt) => new(startAt);

    /// <inheritdoc/>
    public override string ToString() => Value.ToString("u");

    /// <inheritdoc/>
    public bool Equals(EndAtDate? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public int CompareTo(EndAtDate? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}

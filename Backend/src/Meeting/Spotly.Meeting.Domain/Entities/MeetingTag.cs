namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Сущность, описывающая тег встречи.
/// </summary>
public class MeetingTag
{
    /// <summary>
    /// Идентификатор тега.
    /// </summary>
    public MeetingTagId Id { get; }
    
    /// <summary>
    /// Идентификатор встречи.
    /// </summary>
    public MeetingId MeetingId { get; }
    
    /// <summary>
    /// Содержимое тега.
    /// </summary>
    public MeetingTagText Text { get; }

    /// <summary>
    /// Приватный конструктор, используется в фабричном методе.
    /// </summary>
    /// <param name="id">Идентификатор тега.</param>
    /// <param name="meetingId">Идентификатор встречи.</param>
    /// <param name="text">Содержимое тега.</param>
    public MeetingTag(MeetingTagId id, MeetingId meetingId, MeetingTagText text)
    {
        Id = id;
        MeetingId = meetingId;
        Text = text;
    }

    /// <summary>
    /// Фабричный метод для создания нового экземпляра <see cref="MeetingTag"/>.
    /// </summary>
    /// <param name="id">Идентификатор тега.</param>
    /// <param name="meetingId">Идентификатор встречи.</param>
    /// <param name="text">Содержимое тега.</param>
    public static MeetingTag Create(Guid id, Guid meetingId, string text)
    {
        return new MeetingTag(
            MeetingTagId.From(id), 
            MeetingId.From(meetingId), 
            MeetingTagText.From(text));
    }
}

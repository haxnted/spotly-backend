using Spotly.Meeting.Domain.ValueObjects;

namespace Spotly.Meeting.Domain.Entities;

/// <summary>
/// Сущность, описывающая комментарий к встрече.
/// </summary>
public sealed class MeetingComment
{
    /// <summary>
    /// Идентификатор комментария.
    /// </summary>
    public MeetingCommentId Id { get; private set; }

    /// <summary>
    /// Идентификатор встречи.
    /// </summary>
    public MeetingId MeetingId { get; private set; }

    /// <summary>
    /// Идентификатор автора комментария.
    /// </summary>
    public ParticipantId AuthorId { get; private set; }

    /// <summary>
    /// Текст комментария.
    /// </summary>
    public CommentText Text { get; private set; }

    /// <summary>
    /// Дата и время создания комментария.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Дата и время обновления комментария.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Приватный конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="meetingId">Идентификатор встречи.</param>
    /// <param name="authorId">Идентификатор автора комментария.</param>
    /// <param name="text">Комментарий.</param>
    private MeetingComment(
        MeetingCommentId id,
        MeetingId meetingId,
        ParticipantId authorId,
        CommentText text)
    {
        Id = id;
        MeetingId = meetingId;
        AuthorId = authorId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Фабричный метод для создания нового экземпляра <see cref="MeetingComment"/>.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="meetingId">Идентификатор встречи.</param>
    /// <param name="authorId">Идентификатор автора комментария.</param>
    /// <param name="text">Комментарий.</param>
    public static MeetingComment Create(
        Guid id,
        Guid meetingId,
        Guid authorId,
        string text)
    {
        return new MeetingComment(MeetingCommentId.From(id),
            MeetingId.From(meetingId),
            ParticipantId.From(authorId),
            CommentText.From(text));
    }
    
    /// <summary>
    /// Обновление комментария.
    /// </summary>
    /// <param name="text">Текст комментария.</param>
    public void Update(string text) => Text = CommentText.From(text);
}

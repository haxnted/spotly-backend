using Spotly.Meeting.Domain.Entities;
using Spotly.Meeting.Domain.Enums;
using Spotly.Meeting.Domain.Exceptions;
using Spotly.Meeting.Domain.ValueObjects;

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Агрегат-корень, который представляет собой встречу.
/// </summary>
public class Meeting
{
    /// <summary>
    /// Идентификатор встречи.
    /// </summary>
    public MeetingId Id { get; }

    /// <summary>
    /// Идентификатор создателя встречи.
    /// </summary>
    public ParticipantId HostId { get; }

    /// <summary>
    /// Заголовок встречи.
    /// </summary>
    public MeetingTitle Title { get; private set; }

    /// <summary>
    /// Описание встречи.
    /// </summary>
    public MeetingDescription Description { get; private set; }

    /// <summary>
    /// Дата и время начала встречи.
    /// </summary>
    public StartAtDate StartAt { get; private set; }

    /// <summary>
    /// Дата и время окончания встречи.
    /// </summary>
    public EndAtDate EndAt { get; private set; }

    /// <summary>
    /// Место проведения встречи.
    /// </summary>
    public MeetingLocation? Location { get; private set; }

    /// <summary>
    /// Максимальное количество участников.
    /// </summary>
    public int MaxParticipants { get; private set; }

    /// <summary>
    /// Является ли встреча приватной.
    /// </summary>
    public bool IsPrivate { get; private set; }

    /// <summary>
    /// Статус встречи.
    /// </summary>
    public MeetingStatus Status { get; private set; }

    private readonly List<ParticipantId> _participants = [];

    /// <summary>
    /// Список участников встречи.
    /// </summary>
    public virtual ICollection<ParticipantId> Participants => _participants;

    private readonly List<MeetingComment> _comments = [];

    /// <summary>
    /// Список комментариев к встрече.
    /// </summary>
    public virtual ICollection<MeetingComment> Comments => _comments;


    private readonly List<MeetingTag> _tags = [];

    /// <summary>
    /// Список тегов встречи.
    /// </summary>
    public virtual ICollection<MeetingTag> Tags => _tags;

    /// <summary>
    /// Приватный конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="id">Идентификатор встречи.</param>
    /// <param name="hostId">Идентификатор создателя встречи.</param>
    /// <param name="title">Заголовок встречи.</param>
    /// <param name="description">Описание встречи.</param>
    /// <param name="startAt">Дата и время начала встречи.</param>
    /// <param name="endAt">Дата и время окончания встречи.</param>
    private Meeting(
        MeetingId id,
        ParticipantId hostId,
        MeetingTitle title,
        MeetingDescription description,
        StartAtDate startAt,
        EndAtDate endAt)
    {
        Id = id;
        HostId = hostId;
        Title = title;
        Description = description;
        StartAt = startAt;
        EndAt = endAt;
        Status = MeetingStatus.Draft;
    }

    /// <summary>
    /// Фабричный метод для создания нового экземпляра <see cref="Meeting"/>.
    /// </summary>
    /// <param name="id">Идентификатор встречи.</param>
    /// <param name="hostId">Идентификатор создателя встречи.</param>
    /// <param name="title">Заголовок встречи.</param>
    /// <param name="description">Описание встречи.</param>
    /// <param name="startAt">Дата и время начала встречи.</param>
    /// <param name="endAt">Дата и время окончания встречи.</param>
    /// <exception cref="MeetingException">
    /// Если дата начала встречи больше даты окончания.
    /// </exception>
    public static Meeting Create(
        Guid id,
        Guid hostId,
        string title,
        string description,
        DateTime startAt,
        DateTime endAt)
    {
        if (startAt > endAt)
        {
            throw new MeetingException("Start date must be less than end date of meeting.");
        }

        return new Meeting(MeetingId.From(id),
            ParticipantId.From(hostId),
            MeetingTitle.From(title),
            MeetingDescription.From(description),
            StartAtDate.From(startAt),
            EndAtDate.From(endAt));
    }

    /// <summary>
    /// Добавляет дополнительную информацию о встрече.
    /// </summary>
    /// <param name="location">Место проведения встречи.</param>
    /// <param name="maxParticipants">Максимальное количество участников.</param>
    /// <param name="isPrivate">Является ли встреча приватной.</param>
    /// <exception cref="MeetingException">
    /// Если максимальное количество участников меньше нуля.
    /// </exception>
    public void AddInformation(MeetingLocation location, int maxParticipants, bool isPrivate)
    {
        if (maxParticipants < 0)
        {
            throw new MeetingException("Max participants must be greater than zero.");
        }

        Location = location;
        MaxParticipants = maxParticipants;
        IsPrivate = isPrivate;
    }

    /// <summary>
    /// Добавляет участника во встречу.
    /// </summary>
    /// <param name="participantId">Идентификатор участника.</param>
    /// <exception cref="MeetingException">
    /// Если участник является создателем встречи.
    /// Если участник уже есть во встрече.
    /// </exception>
    public void AddParticipant(ParticipantId participantId)
    {
        if (participantId == HostId)
        {
            throw new MeetingException("Participant in meeting can't be host.");
        }

        if (_participants.Contains(participantId))
        {
            throw new MeetingException("Participant in meeting already exists.");
        }

        _participants.Add(participantId);
    }

    /// <summary>
    /// Удаляет участника из встречи.
    /// </summary>
    /// <param name="participantId">Идентификатор участника.</param>
    /// <exception cref="MeetingException">
    /// Если участник является создателем встречи.
    /// Если участник отсутствует во встрече.
    /// </exception>
    public void RemoveParticipant(ParticipantId participantId)
    {
        if (participantId == HostId)
        {
            throw new MeetingException("Participant in meeting can't be host.");
        }

        if (_participants.Contains(participantId) is false)
        {
            throw new MeetingException("Participant in meeting already exists.");
        }

        _participants.Remove(participantId);
    }

    /// <summary>
    /// Добавляет комментарий к встрече.
    /// </summary>
    /// <param name="comment">Комментарий.</param>
    /// <exception cref="MeetingException">
    /// Если комментарий уже существует.
    /// </exception>
    public void AddComment(MeetingComment comment)
    {
        if (_comments.Contains(comment))
        {
            throw new MeetingException("Comment in meeting already exists.");
        }

        _comments.Add(comment);
    }

    /// <summary>
    /// Удаляет комментарий к встрече.
    /// </summary>
    /// <param name="comment">Комментарий.</param>
    /// <exception cref="MeetingException">
    /// Если комментарий не существует.
    /// </exception>
    public void RemoveComment(MeetingComment comment)
    {
        if (_comments.Contains(comment) is false)
        {
            throw new MeetingException("Comment in meeting doesn't exist.");
        }

        _comments.Remove(comment);
    }

    /// <summary>
    /// Добавляет тег во встречу.
    /// </summary>
    /// <param name="tag">Тег.</param>
    /// <exception cref="MeetingException">
    /// Если тег уже существует.
    /// </exception>
    public void AddTag(MeetingTag tag)
    {
        if (_tags.Contains(tag))
        {
            throw new MeetingException("Tag in meeting already exists.");
        }

        _tags.Add(tag);
    }

    /// <summary>
    /// Удаляет тег из встречи.
    /// </summary>
    /// <param name="tag">Тег.</param>
    /// <exception cref="MeetingException">
    /// Если тег не существует.
    /// </exception>
    public void RemoveTag(MeetingTag tag)
    {
        if (_tags.Contains(tag) is false)
        {
            throw new MeetingException("Tag in meeting doesn't exist.");
        }

        _tags.Remove(tag);
    }

    /// <summary>
    /// Изменяет заголовок встречи.
    /// </summary>
    /// <param name="title">Новый заголовок встречи.</param>
    public void ChangeTitle(MeetingTitle title) => Title = title;

    /// <summary>
    /// Изменяет описание встречи.
    /// </summary>
    /// <param name="description">Новое описание встречи.</param>
    public void ChangeDescription(MeetingDescription description) => Description = description;

    /// <summary>
    /// Планирует встречу.
    /// </summary>
    /// <exception cref="MeetingException">
    /// Если статус встречи уже планируется.
    /// Если статус встречи уже в процессе.
    /// Если встреча завершена.
    /// Если встреча отменена.
    /// </exception>
    public void Schedule()
    {
        Status = Status switch
        {
            MeetingStatus.Scheduled => throw new MeetingException("Meeting is already scheduled."),
            MeetingStatus.Ongoing => throw new MeetingException("Meeting is already ongoing."),
            MeetingStatus.Finished => throw new MeetingException("Meeting is finished."),
            MeetingStatus.Cancelled => throw new MeetingException("Meeting is cancelled."),
            _ => MeetingStatus.Scheduled
        };
    }

    /// <summary>
    /// Начинает встречу.
    /// </summary>
    /// <exception cref="MeetingException">
    /// Если статус встречи не планируется.
    /// Если статус встречи уже в процессе.
    /// Если встреча завершена.
    /// Если встреча отменена.
    /// </exception>
    public void OnGoing()
    {
        Status = Status switch
        {
            MeetingStatus.Draft => throw new MeetingException("Meeting is draft."),
            MeetingStatus.Ongoing => throw new MeetingException("Meeting is already ongoing."),
            MeetingStatus.Finished => throw new MeetingException("Meeting is finished."),
            MeetingStatus.Cancelled => throw new MeetingException("Meeting is cancelled."),
            _ => MeetingStatus.Ongoing
        };
    }

    /// <summary>
    /// Завершает встречу.
    /// </summary>
    /// <exception cref="MeetingException">
    /// Если статус встречи не в процессе.
    /// Если встреча завершена.
    /// Если встреча отменена.
    /// </exception>
    public void Finish()
    {
        Status = Status switch
        {
            MeetingStatus.Draft => throw new MeetingException("Meeting is draft."),
            MeetingStatus.Finished => throw new MeetingException("Meeting is already finished."),
            MeetingStatus.Cancelled => throw new MeetingException("Meeting is cancelled."),
            _ => MeetingStatus.Finished
        };
    }

    /// <summary>
    /// Отменяет встречу.
    /// </summary>
    /// <exception cref="MeetingException">
    /// Если встреча уже отменена.
    /// Если встреча завершена.
    /// </exception>
    public void Cancel()
    {
        Status = Status switch
        {
            MeetingStatus.Draft => throw new MeetingException("Meeting is draft."),
            MeetingStatus.Cancelled => throw new MeetingException("Meeting is already cancelled."),
            _ => MeetingStatus.Cancelled
        };
    }
}

namespace Spotly.Meeting.Domain.Aggregates;

/// <summary>
/// Статус встречи.
/// </summary>
public enum MeetingStatus
{
    /// <summary>
    /// Черновик.
    /// </summary>
    Draft = 1,
    
    /// <summary>
    /// Планируется.
    /// </summary>
    Scheduled = 2,
    
    /// <summary>
    /// В процессе.
    /// </summary>
    Ongoing = 3,
    
    /// <summary>
    /// Завершена.
    /// </summary>
    Finished = 4,
    
    /// <summary>
    /// Отменена.
    /// </summary>
    Cancelled = 5
}

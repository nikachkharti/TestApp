namespace TaskManager.Application.Models
{
    public record TodoForGettingDto
    (
        Guid Id,
        string Title,
        DateTime StartDate,
        DateTime EndDate,
        bool IsCompleted,
        Guid UserId
    );
}

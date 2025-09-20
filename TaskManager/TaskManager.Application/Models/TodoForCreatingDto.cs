namespace TaskManager.Application.Models
{
    public record TodoForCreatingDto
    (
        Guid Id,
        string Title,
        DateTime StartDate,
        DateTime EndDate,
        bool IsCompleted,
        Guid UserId
    );
}

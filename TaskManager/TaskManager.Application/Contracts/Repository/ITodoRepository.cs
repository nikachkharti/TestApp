using TaskManager.Domain.Entities;

namespace TaskManager.Application.Contracts.Repository
{
    public interface ITodoRepository
    {
        List<Todo> GetTodosOfUser(Guid userId);
        Todo GetTodo(Guid id);
        Guid AddTodo(Todo model);
        Guid DeleteTodo(Guid id);
    }
}

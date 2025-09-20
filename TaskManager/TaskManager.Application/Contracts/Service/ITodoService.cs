using TaskManager.Application.Models;

namespace TaskManager.Application.Contracts.Service
{
    public interface ITodoService
    {
        IEnumerable<TodoForGettingDto> GetAllTodosOfSpecificUser(Guid userId);
        IEnumerable<TodoForGettingDto> GetAllTodos();
        TodoForGettingDto GetSingleTodo(Guid todoId);
        Guid AddNewTodo(TodoForCreatingDto todoForCreatingDto);
        Guid DeleteExistedTodo(Guid id);
    }
}

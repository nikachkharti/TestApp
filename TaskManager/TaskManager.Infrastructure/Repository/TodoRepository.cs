using TaskManager.Application.Contracts.Repository;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly List<Todo> _todoList = new();
        private static readonly Lock _todoRepositoryLocker = new();

        public Guid AddTodo(Todo model)
        {
            model.Id = Guid.NewGuid();

            lock (_todoRepositoryLocker)
            {
                _todoList.Add(model);
            }

            return model.Id;
        }

        public Guid DeleteTodo(Guid id)
        {
            lock (_todoRepositoryLocker)
            {
                var todo = _todoList.FirstOrDefault(t => t.Id == id);
                if (todo != null)
                {
                    _todoList.Remove(todo);
                    return id;
                }
            }
            return Guid.Empty;
        }

        public Todo GetTodo(Guid id)
        {
            lock (_todoRepositoryLocker)
            {
                return _todoList.FirstOrDefault(t => t.Id == id);
            }
        }

        public List<Todo> GetTodosOfUser(Guid userId)
        {
            lock (_todoRepositoryLocker)
            {
                return _todoList
                    .Where(t => t.UserId == userId)
                    .ToList();
            }
        }
    }
}

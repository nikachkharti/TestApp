using TaskManager.Application.Contracts.Repository;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly Lock _todoRepositoryLocker = new();

        private static readonly List<Todo> _todoList = new()
        {
            new Todo()
            {
                Id = Guid.Parse("a3d2f5c6-1b7d-4f9e-8c45-12d4e6b89f01"),
                Title = "Learn ASP.NET Core",
                StartDate = DateTime.UtcNow.AddDays(-3),
                EndDate = DateTime.UtcNow.AddDays(2),
                IsCompleted = false,
                UserId = Guid.Parse("f1e8d3c2-5a7b-4b9d-9e2a-9d3a1b6c7e88")
            },
            new Todo()
            {
                Id = Guid.Parse("c7f8b9d2-3a4b-41c9-932e-1a2b3c4d5e6f"),
                Title = "Deploy to Production",
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow.AddDays(5),
                IsCompleted = false,
                UserId = Guid.Parse("a2b3c4d5-6e7f-48a9-90b1-c2d3e4f5a678")
            },
            new Todo()
            {
                Id = Guid.Parse("d9e1f2a3-4b5c-678d-890e-1f2a3b4c5d6e"),
                Title = "Write Unit Tests",
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow.AddDays(-2),
                IsCompleted = true,
                UserId = Guid.Parse("f1e8d3c2-5a7b-4b9d-9e2a-9d3a1b6c7e88")
            }
        };

        public Guid AddTodo(Todo model)
        {
            model.Id = Guid.NewGuid();

            lock (_todoRepositoryLocker)
            {
                _todoList.Add(model);
            }

            return model.Id;
        }

        public List<Todo> AllTodos()
        {
            lock (_todoRepositoryLocker)
            {
                return _todoList;
            }
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

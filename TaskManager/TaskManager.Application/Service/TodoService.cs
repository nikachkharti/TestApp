using FluentValidation;
using MapsterMapper;
using TaskManager.Application.Contracts.Repository;
using TaskManager.Application.Contracts.Service;
using TaskManager.Application.Models;
using TaskManager.Domain.Entities;
using ValidationException = FluentValidation.ValidationException;

namespace TaskManager.Application.Service
{
    public class TodoService : ITodoService
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;
        private readonly IValidator<TodoForCreatingDto> _todoForCreatingValidator;

        public TodoService(IMapper mapper, ITodoRepository todoRepository, IValidator<TodoForCreatingDto> todoForCreatingValidator)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
            _todoForCreatingValidator = todoForCreatingValidator;
        }

        public Guid AddNewTodo(TodoForCreatingDto todoForCreatingDto)
        {
            ValidateInput(todoForCreatingDto);
            var todo = _mapper.Map<Todo>(todoForCreatingDto);

            return _todoRepository.AddTodo(todo);
        }

        public Guid DeleteExistedTodo(Guid id)
        {
            return _todoRepository.DeleteTodo(id);
        }

        public IEnumerable<TodoForGettingDto> GetAllTodosOfSpecificUser(Guid userId)
        {
            var todos = _todoRepository.GetTodosOfUser(userId);

            if (todos.Count <= 0)
            {
                return Enumerable.Empty<TodoForGettingDto>();
            }

            return _mapper.Map<List<TodoForGettingDto>>(todos);
        }

        public TodoForGettingDto GetSingleTodo(Guid todoId)
        {
            var todo = _todoRepository.GetTodo(todoId);

            if (todo is null)
            {
                return null;
            }

            return _mapper.Map<TodoForGettingDto>(todo);
        }

        private void ValidateInput(TodoForCreatingDto todoForCreatingDto)
        {
            var validationResult = _todoForCreatingValidator.Validate(todoForCreatingDto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}

using Mapster;
using TaskManager.Application.Models;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TodoForCreatingDto, Todo>()
              .Map(dest => dest.Id, src => src.Id == Guid.Empty ? Guid.NewGuid() : src.Id)
              .Map(dest => dest.Title, src => src.Title)
              .Map(dest => dest.StartDate, src => src.StartDate)
              .Map(dest => dest.EndDate, src => src.EndDate)
              .Map(dest => dest.IsCompleted, src => src.IsCompleted)
              .Map(dest => dest.UserId, src => src.UserId);

            config.NewConfig<Todo, TodoForGettingDto>()
              .Map(dest => dest.Id, src => src.Id)
              .Map(dest => dest.Title, src => src.Title)
              .Map(dest => dest.StartDate, src => src.StartDate)
              .Map(dest => dest.EndDate, src => src.EndDate)
              .Map(dest => dest.IsCompleted, src => src.IsCompleted)
              .Map(dest => dest.UserId, src => src.UserId);

        }
    }
}

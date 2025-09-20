using FluentValidation;
using TaskManager.Application.Models;

namespace TaskManager.Application.Validators
{
    public class TodoForCreatingDtoValidator : AbstractValidator<TodoForCreatingDto>
    {
        public TodoForCreatingDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("EndDate is required.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("EndDate must be greater than or equal to StartDate.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}

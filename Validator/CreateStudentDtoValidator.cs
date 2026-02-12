using FluentValidation;
using StudentAPI.DTO;

namespace StudentAPI.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Age)
                .InclusiveBetween(5, 100)
                .WithMessage("Age must be between 5 and 100");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be greater than 0");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid Gender"); 
        }
    }

    public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Age)
                .InclusiveBetween(5, 100)
                .WithMessage("Age must be between 5 and 100");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be greater than 0");

            RuleFor(x => x.RowVersion)
                .NotNull().WithMessage("RowVersion is required for concurrency check");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid Gender"); 
        }
    }
}

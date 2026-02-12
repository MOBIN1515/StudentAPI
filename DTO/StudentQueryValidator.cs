using FluentValidation;

namespace StudentAPI.DTO;

public class StudentQueryValidator : AbstractValidator<StudentQueryDto>
{
    public StudentQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50);
    }
}

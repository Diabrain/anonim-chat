using anonim_chat.API.Models;
using FluentValidation;

namespace anonim_chat.API.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(e => e.UserName)
            .NotEmpty()
            .Length(4, 50);

        RuleFor(e => e.Password)
            .Equal(e => e.ConfirmPassword)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
    }
}

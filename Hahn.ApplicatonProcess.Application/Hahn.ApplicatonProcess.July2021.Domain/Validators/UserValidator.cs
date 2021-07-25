using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.Models;

namespace Plex.ConnectedManufacturing.Domain.CQRS.Validators.EventLog
{
  public class UserValidator : AbstractValidator<User>
  {
    private const int MinimumUserAge = 18;
    private const int MinimumNameCharacter = 3;

    public UserValidator()
    {
      RuleFor(x => x.Age)
        .GreaterThan(MinimumUserAge)
        .WithMessage($"User age must be greater than {MinimumUserAge}");

      // TODO: check is null & empty required or not
      RuleFor(x => x.FirstName)
        .NotNull().NotEmpty().MinimumLength(MinimumNameCharacter)
        .WithMessage($"At least {MinimumNameCharacter} character required for {nameof(User.FirstName)}");

      // TODO: check is null & empty required or not
      RuleFor(x => x.LastName)
        .NotNull().NotEmpty().MinimumLength(MinimumNameCharacter)
        .WithMessage($"At least {MinimumNameCharacter} character required for {nameof(User.LastName)}");

      RuleFor(x => x.Email)
        .Matches(@"(.*@*\.[a-zA-Z]{2,6}$)")
        .WithMessage($"{nameof(User.Email)} is not valid.");
    }
  }
}


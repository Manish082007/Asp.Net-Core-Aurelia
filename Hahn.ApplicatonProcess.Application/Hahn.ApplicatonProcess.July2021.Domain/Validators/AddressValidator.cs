using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System.Linq;

namespace Plex.ConnectedManufacturing.Domain.CQRS.Validators.EventLog
{
  public class AddressValidator : AbstractValidator<Address>
  {
    public AddressValidator()
    {
      When(user => string.IsNullOrWhiteSpace(user.PostalCode), () => {

        _ = RuleFor(x => x.PostalCode)
        .Length(5)
        .Must(IsValidPostalCodeNumber)
        .WithMessage("Germany has 5 digit postal code system since 1993.");

      });
    }

    private bool IsValidPostalCodeNumber(string postalCode)
    {
      return int.TryParse(postalCode, out _);
    }
  }
}


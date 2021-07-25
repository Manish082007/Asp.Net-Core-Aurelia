using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.Models;

namespace Plex.ConnectedManufacturing.Domain.CQRS.Validators.EventLog
{
  public class AssetValidator : AbstractValidator<Asset>
  {
    public AssetValidator()
    {
      RuleFor(x => x.Name)
        .Matches(@"^([a-zA-Z0-9.]+\s)*[a-zA-Z0-9.]{2,50}$")
        .WithMessage($"{nameof(Asset.Name)} is invalid");

      RuleFor(x => x.Symbol)
        .Matches(@"^[A-Z]{2,20}$")
        .WithMessage($"{nameof(Asset.Symbol)} is invalid");
    }
  }
}


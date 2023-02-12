using AypWebAPI.Models.RequestModels;
using FluentValidation;

namespace AypWebAPI.Models.Validators
{
    public class UpdatePlayerValidator: AbstractValidator<UpdatePlayer>
    {
        public UpdatePlayerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.BackNumber).NotEmpty();
        }
    }
}

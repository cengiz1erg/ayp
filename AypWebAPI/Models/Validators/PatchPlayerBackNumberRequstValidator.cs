using AypWebAPI.Models.RequestModels;
using FluentValidation;

namespace AypWebAPI.Models.Validators
{
    public class PatchPlayerBackNumberRequstValidator: AbstractValidator<PatchPlayerBackNumberRequest>
    {
        public PatchPlayerBackNumberRequstValidator()
        {
            RuleFor(x => x.BackNumber).NotEmpty();
        }
    }
}

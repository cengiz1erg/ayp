using AypWebAPI.Models.RequestModels;
using FluentValidation;

namespace AypWebAPI.Models.Validators
{
    public class PostPlayerValidator: AbstractValidator<PostPlayer>
    {
        public PostPlayerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.BackNumber).NotEmpty();
        }
    }
}

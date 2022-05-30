using System.Linq;
using FluentValidation;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.CQRS.Commands;

namespace NoMansSkyRecipies.CQRS.Validation
{
    public class AddResourceCommandValidator : AbstractValidator<AddResourceCommand>
    {

        public AddResourceCommandValidator(IResourceRepository resourceRepository)
        {
            RuleFor(x => !string.IsNullOrWhiteSpace(x.Model.ResourceType));
            RuleFor(x => !string.IsNullOrWhiteSpace(x.Model.Name));
            RuleFor(x => x.Model.Value > 0);
        }
    }
}

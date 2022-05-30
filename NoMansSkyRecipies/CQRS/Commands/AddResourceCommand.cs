using MediatR;
using NmsDisplayData;
using NmsRecipes.DAL.Model;

namespace NoMansSkyRecipies.CQRS.Commands
{
    public class AddResourceCommand : IRequest<(int newId, DisplayedResource resource)>
    {
        public RawResourceModel Model { get; }

        public AddResourceCommand(RawResourceModel model)
        {
            Model = model;
        }
    }
}

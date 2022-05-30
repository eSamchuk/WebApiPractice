using System.Collections.Generic;
using MediatR;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;

namespace NoMansSkyRecipies.CQRS.Queries
{
    public class GetResourceByIdQuery : IRequest<DisplayedResource>
    {
        public int Id { get; }

        public GetResourceByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}

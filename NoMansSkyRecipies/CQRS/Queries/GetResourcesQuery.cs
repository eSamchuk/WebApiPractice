using System.Collections.Generic;
using MediatR;
using NmsDisplayData;

namespace NoMansSkyRecipies.CQRS.Queries
{
    public class GetResourcesQuery : IRequest<List<DisplayedResource>>
    {
       
    }
}

using System.Collections.Generic;
using MediatR;

namespace NoMansSkyRecipies.CQRS.Queries
{
    public class GetSimplifiedResourcesQuery : IRequest<List<string>>
    {

    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.CQRS.Queries;

namespace NoMansSkyRecipies.CQRS.Handlers
{
    public class GetSimplifiedResourcesHandler : IRequestHandler<GetSimplifiedResourcesQuery, List<string>>
    {
        private readonly IResourceRepository _resourceRepository;

        public GetSimplifiedResourcesHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }


        public async Task<List<string>> Handle(GetSimplifiedResourcesQuery request, CancellationToken cancellationToken)
        {
            return (await Task.FromResult(this._resourceRepository.GetAllItems())).
                Select(x => x.Name)
                .ToList();
        }
    }
}

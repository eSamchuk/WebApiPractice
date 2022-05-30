using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.CQRS.Queries;

namespace NoMansSkyRecipies.CQRS.Handlers
{
    public class GetAllResourcesHandler : IRequestHandler<GetResourcesQuery, List<DisplayedResource>>
    {
        private readonly IResourceRepository _resourceRepository;

        public GetAllResourcesHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<List<DisplayedResource>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
        {
            var result = (await Task.FromResult(this._resourceRepository.GetAllItems()))
                .Select(x => new DisplayedResource()
                {
                    ResourceName = x.Name,
                    ResourceTypeName = x.RawResourceType.ResourceTypeName,
                    Value = x.Value
                }).ToList();

            return result;
        }
    }
}

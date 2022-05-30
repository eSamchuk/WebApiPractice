using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nms.Mappings;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.CQRS.Queries;
using NoMansSkyRecipies.Data.Entities;

namespace NoMansSkyRecipies.CQRS.Handlers
{
    public class GetResourceByIdHandler : IRequestHandler<GetResourceByIdQuery, DisplayedResource>
    {
        private readonly IResourceRepository _resourceRepository;

        public GetResourceByIdHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<DisplayedResource> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {
            RawResource resource = await Task.FromResult(this._resourceRepository.GetItemById(request.Id));

            return resource.MapToDisplayedResource();
        }
    }
}

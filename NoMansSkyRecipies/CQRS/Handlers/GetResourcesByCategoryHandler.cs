using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nms.Mappings;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.CQRS.Queries;

namespace NoMansSkyRecipies.CQRS.Handlers
{
    public class GetResourcesByCategoryHandler : IRequestHandler<GetResourceByCategoryQuery, List<DisplayedResource>>
    {
        public IResourceRepository ResourceRepository { get; }

        public GetResourcesByCategoryHandler(IResourceRepository resourceRepository)
        {
            ResourceRepository = resourceRepository;
        }

        public async Task<List<DisplayedResource>> Handle(GetResourceByCategoryQuery request, CancellationToken cancellationToken)
        {
            return (await Task.FromResult(this.ResourceRepository.GetItemsByCondition(request.PredicateExpression)))
                .Select(x => x.MapToDisplayedResource()).ToList();

        }
    }
}

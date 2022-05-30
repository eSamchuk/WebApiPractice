using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nms.Mappings;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.CQRS.Commands;
using NoMansSkyRecipies.Data.Entities;

namespace NoMansSkyRecipies.CQRS.Handlers
{
    public class AddResourceHandler : IRequestHandler<AddResourceCommand, (int newId, DisplayedResource resource)>
    {
        private readonly IResourceRepository _resourceRepository;

        public AddResourceHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }


        public async Task<(int newId, DisplayedResource resource)> Handle(AddResourceCommand request, CancellationToken cancellationToken)
        {
            DisplayedResource result = null;
            var model = request.Model;
            var resourceType = await Task.FromResult(
                this._resourceRepository.GetRawResourceTypes().FirstOrDefault(x => x.ResourceTypeName == model.ResourceType));

            int newId = await Task.FromResult(this._resourceRepository.AddItem(new RawResource()
            {
                Name = model.Name,
                Value = model.Value,
                RawResourceTypeId = resourceType.Id
            }));

            if (newId != 0)
            {
                result = this._resourceRepository.GetItemById(newId)?.MapToDisplayedResource();
            }file:///C:/Program%20Files%20(x86)/ACD%20Systems/ACDSee/8.0.Pro/QuickStart/browse.html

            return (newId, result);
        }
    }
}

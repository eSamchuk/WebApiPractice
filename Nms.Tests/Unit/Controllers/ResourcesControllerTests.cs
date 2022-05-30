using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Controllers.v1;
using NoMansSkyRecipies.Data.Entities;
using NUnit;
using NUnit.Framework;

namespace Nms.Tests.Unit.Controllers
{
    
    public class ResourcesControllerTests
    {
        private Mock<IResourceRepository> _resourceRepository;
        private Mock<IRecipeRepository> _recipeRepository;

        private ResourcesController _resourcesController;

        [SetUp]
        public void Setup()
        {
            this._recipeRepository = new Mock<IRecipeRepository>();



            this._resourceRepository = new Mock<IResourceRepository>();
            this._resourceRepository.Setup(x => x.GetItemById(100)).Returns((RawResource)null);
            this._resourceRepository.Setup(x => x.GetItemById(1)).Returns(new RawResource { Id = 1, Name = "some_name", Value = 100, RawResourceTypeId = 1});


            //this._resourcesController = new ResourcesController(this._resourceRepository.Object, this._recipeRepository.Object);

        }


        public void GetResourceById_Existing()
        {
            var itemById = this._resourcesController.GetResourceById(1).Result;

             //Assert.That(itemById.);
        }

    }
}

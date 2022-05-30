using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NmsDisplayData;
using NmsRecipes.DAL.Model;
using NUnit.Framework;

namespace Nms.Tests.Itegration
{
    [TestFixture]
    public class RecipeIntegrationTests : IntegrationTestBaseClass
    {
        [Test]
        public async Task Get_All_Recipes()
        {
            var response = await this._client.GetAsync("/api/v1/Recipes");
            var content = await response.Content.ReadAsAsync<List<DisplayedRecipe>>();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.NotNull(content);
            Assert.NotZero(content.Count);
            Assert.That(content.Count == 31);
        }

        [TestCase("Acid", 2)]
        [TestCase("Aronium", 2)]
        [TestCase("Glass", 1)]
        public async Task Get_Recipe_For_Existing_Item(string itemName, int componentsNeeded)
        {
            var response = await this._client.GetAsync($"/api/v1/Recipes/{itemName}");
            var content = await response.Content.ReadAsAsync<DisplayedRecipe>();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            Assert.NotNull(content);
            Assert.That(content.ItemName == itemName);
            Assert.NotNull(content.NeededResources);
            Assert.That(content.NeededResources.Count == componentsNeeded);
            Assert.That(content.NeededResources.All(x => x.NeededAmount > 0 && 
                                                         x.Value > 0 && 
                                                         !string.IsNullOrWhiteSpace(x.ResourceName) && 
                                                         !string.IsNullOrWhiteSpace(x.ResourceTypeName)));
        }

        [Test]
        public async Task Get_Recipe_For_NotExisting_Item()
        {
            var response = await this._client.GetAsync($"/api/v1/Recipes/NonExistingItem");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Add_Invalid_Recipe_NoItem()
        {
            ////arrange
            RecipeModel model = new RecipeModel()
            {
                TargetItemId = 999,
                NeededResources = new List<(string resourceName, int neededAmount)>()
                {
                    ("Dioxite", 150),
                    ("Ammonia", 250)
                }
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PostAsync("/api/v1/Recipes", payload);
            var content = await response.Content.ReadAsAsync<Dictionary<string, IEnumerable<string>>>();

            ////assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.NotNull(content);
            Assert.IsNotEmpty(content);
            Assert.That(content.Count == 1);
            Assert.That(content["itemName"] != null);
        }

        [Test]
        public async Task Add_Valid_Recipe()
        {
            ////arrange
            RecipeModel model = new RecipeModel()
            {
                TargetItemId = 1,
                NeededResources = new List<(string resourceName, int neededAmount)>()
                {
                    ("Dioxite", 150),
                    ("Ammonia", 250)
                }
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PostAsync("/api/v1/Recipes", payload);
            var content = await response.Content.ReadAsAsync<DisplayedRecipe>();

            ////assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.NotNull(content);
            Assert.NotZero(content.Id);
            Assert.That(content.NeededResources.Count == model.NeededResources.Count);

        }


        [Test]
        public async Task Delete_Valid_Recipe()
        {
            ////act
            var response = await this._client.DeleteAsync("/api/v1/Recipes/1");

            ////assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async Task Delete_Invalid_Recipe()
        { 
            ////act
            var response = await this._client.DeleteAsync("/api/v1/Recipes/10000");

            ////assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}

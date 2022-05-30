using System;
using NmsDisplayData;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nms.StaticData;
using NmsRecipes.DAL.Model;

namespace Nms.Tests.Itegration
{
    public class ResourcesIntegrationTest : IntegrationTestBaseClass
    {

        public ResourcesIntegrationTest() : base()
        {
        }


        [Test, Order(1)]
        public async Task Test_v1_Get_All_Items()
        {
            ////arrange

            ////act
            var response = await this._client.GetAsync("api/v1/Resources");

            ////assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<List<DisplayedResource>>();

            Assert.IsNotNull(content);
            Assert.NotZero(content.Count);
            Assert.That(content.Count == 20);
        }

        [TestCase(SimplifiedResourceCategories.Gases, ResourceTypesNames.Gas)]
        [TestCase(SimplifiedResourceCategories.Minerals, ResourceTypesNames.MinedMineral)]
        [TestCase(SimplifiedResourceCategories.Plants, ResourceTypesNames.HarvestedPlant)]
        public async Task Test_v1_Get_By_Categories(string category, string resourceType)
        {
            ////arrange

            ////act
            var response = await this._client.GetAsync($"api/v1/Resources/{category}");

            ////assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<List<DisplayedResource>>();

            Assert.IsNotNull(content);
            Assert.NotZero(content.Count);
            Assert.IsNotNull(content.All(x => x.ResourceTypeName == resourceType));
        }

        [TestCase(1, ResourcesNames.Ammonia)]
        [TestCase(4, ResourcesNames.Dioxite)]
        [TestCase(12, ResourcesNames.Paraffinium)]
        public async Task Test_Get_Existing_Resource_By_Id(int id, string expectedName)
        {
            ////arrange

            ////act
            var response = await this._client.GetAsync($"api/v1/Resources/{id}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<DisplayedResource>();

            ////assert
            Assert.IsNotNull(content);
            Assert.That(content.ResourceName == expectedName);
        }

        public async Task Test_Get_NotExisting_Resource_By_Id()
        {
            ////act
            var response = await this._client.GetAsync($"api/v1/Resources/999");



            ////assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var content = await response.Content.ReadAsAsync<DisplayedResource>();
            Assert.IsNull(content);
        }

        [TestCase(1, 1)]
        [TestCase(9, 4)]
        public async Task Test_Get_Existing_Resource_UsedIn(int id, int usedInCount)
        {
            ////act
            var response = await this._client.GetAsync($"api/v1/Resources/{id}/UsedIn");

            var content = await response.Content.ReadAsAsync<List<string>>();
            ////assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.NotZero(content.Count());
            Assert.That(content.Count() == usedInCount);
        }

        [Test]
        public async Task Test_Add_Valid_Resource()
        {
            ////arrange
            var model = new RawResourceModel()
            {
                Name = "New valid resource",
                Value = 184,
                ResourceType = ResourceTypesNames.Gas
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PostAsync($"api/v1/Resources/", payload);

            ////assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Test_Add_InValid_Resource_NoValue()
        {
            ////arrange
            var model = new RawResourceModel()
            {
                Name = "New resource no value",
                ResourceType = ResourceTypesNames.Gas
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PostAsync($"api/v1/Resources/", payload);

            ////assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Test_Add_InValid_Resource_NoName()
        {
            ////arrange
            var model = new RawResourceModel()
            {
                Value = 12,
                ResourceType = ResourceTypesNames.Gas
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PostAsync($"api/v1/Resources/", payload);

            ////assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Test_Add_InValid_Resource_InvalidType()
        {
            ////arrange
            var model = new RawResourceModel()
            {
                Name = "New resource invalid type",
                Value = 12,
                ResourceType = "Type"
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PostAsync($"api/v1/Resources/", payload);

            ////assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Test_Add_InValid_Resource_ExistingName()
        {
            ////arrange
            var model = new RawResourceModel()
            {
                Name = "Ammonia",
                Value = 12,
                ResourceType = ResourceTypesNames.MinedMineral
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PostAsync($"api/v1/Resources/", payload);

            ////assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Test_Update_Valid_Resource()
        {
            ////arrange
            var model = new RawResourceModel()
            {
                Name = "Ammonia",
                Value = 188,
                ResourceType = ResourceTypesNames.MinedMineral
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PutAsync($"api/v1/Resources/1", payload);

            ////assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Test_Update_NotExisting_Resource()
        {
            ////arrange
            var model = new RawResourceModel()
            {
                Name = "Ammonia2",
                Value = 188,
                ResourceType = ResourceTypesNames.MinedMineral
            };

            StringContent payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            ////act
            var response = await this._client.PutAsync($"api/v1/Resources/999", payload);

            ////assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

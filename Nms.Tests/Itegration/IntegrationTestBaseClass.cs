using NUnit.Framework;
using System.Net.Http;

namespace Nms.Tests.Itegration
{
    [TestFixture]
    public class IntegrationTestBaseClass
    {
        protected readonly HttpClient _client;

        public IntegrationTestBaseClass()
        {
            var appFactory = new ApplicationFactory();

            this._client = appFactory.CreateClient();
        }
    }
}

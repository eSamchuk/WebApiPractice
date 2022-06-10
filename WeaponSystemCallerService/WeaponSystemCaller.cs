using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeaponSystemCaller
{
    public class WeaponSystemCallerService : IWeaponSystemCallerService
    {
        private readonly HttpClient _client;

        public WeaponSystemCallerService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<string> CallForWeapons()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri("api/v1/Multitools", UriKind.Relative));
                var result = await this._client.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsStringAsync();
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}

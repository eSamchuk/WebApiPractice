using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeaponSystemCaller
{
    public interface IWeaponSystemCallerService
    {
        public Task<string> CallForWeapons();
    }
}

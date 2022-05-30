using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Repositories;
using NoMansSkyRecipies.CustomSettings;

namespace NoMansSkyRecipies.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IResourceRepository, ResourcesRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();
            services.AddTransient<ICraftedItemsRepository, CraftedItemsRepository>();
            services.AddTransient<IMiningOutpostRepository, MiningOutpostRepository>();
            services.AddTransient<IElectroMagneticGeneratorRepository, ElectromagneticGeneratorRepository>();
            services.AddTransient<IEnergySupplyRepository, EnergySupplyRepository>();
            services.AddTransient<IHotspotClassesRepository, HotspotClassesRepository>();
            services.AddTransient<IExctractorRepository, ExctractorRepository>();

        }
    }
}

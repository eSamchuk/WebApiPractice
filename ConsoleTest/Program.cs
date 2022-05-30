using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using Nms.StaticData.Hotspot;
using NmsDisplayData;
using NmsRecipes.DAL.Model;
using NoMansSkyRecipies.Data.Entities.Resources;
using NoMansSkyRecipies.Extensions;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OutpostModel model = new OutpostModel()
            {
                EnergySupply = new EnergySupplyModel()
                {
                    Plant = new ElectroMagneticPlantModel()
                    {
                        Generators = new List<ElectromagneticGeneratorModel>()
                        {
                            new ElectromagneticGeneratorModel()
                        }
                    }
                },
                Exctractors = new List<ExctractorModel>()
                {
                    new ExctractorModel()
                }
            };



            var result = JsonConvert.SerializeObject(model, Formatting.Indented);

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}

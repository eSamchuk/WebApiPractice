using Microsoft.EntityFrameworkCore;
using Nms.StaticData;
using Nms.StaticData.Hotspot;
using NoMansSkyRecipies.Data.Entities;
using NoMansSkyRecipies.Data.Entities.Energy;
using NoMansSkyRecipies.Data.Entities.Plants;
using NoMansSkyRecipies.Data.Entities.Resources;

namespace NoMansSkyRecipies.Data
{
    public class RecipiesDbContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<RawResource> RawResources { get; set; }
        public DbSet<RawResourceType> RawResourceTypes { get; set; }
        public DbSet<CraftableItem> CraftableItems { get; set; }
        public DbSet<Recipie> Recipies { get; set; }
        public DbSet<NeededResource> NeededResources { get; set; }
        public DbSet<EnergySupply> EnergySupplies { get; set; }
        public DbSet<ElectroMagneticPlant> ElectroMagneticPlants { get; set; }
        public DbSet<ElectromagneticGenerator> ElectromagneticGenerators { get; set; }
        public DbSet<HotspotClass> HotspotClasses { get; set; }
        public DbSet<MiningOutpost> MiningOutposts { get; set; }
        public DbSet<Extractor> Extractors { get; set; }



        public RecipiesDbContext(DbContextOptions<RecipiesDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Biodome>(b =>
            {
                b.HasOne(x => x.Plant);
            });

            modelBuilder.Entity<Plant>(b =>
            {
                b.HasOne(x => x.Resource);
            });


            modelBuilder.Entity<Extractor>(b =>
            {
                b.HasOne(x => x.MiningOutpost);
            });
            
            modelBuilder.Entity<MiningOutpost>(b =>
            {
                b.HasOne(x => x.EnergySupply);
                b.HasOne(x => x.ResourceType);
                b.HasOne(x => x.HotspotClass).WithMany(x => x.MiningOutposts);
                b.HasMany(x => x.Extractors).WithOne(x => x.MiningOutpost);
            });

            modelBuilder.Entity<HotspotClass>(b =>
            {
                b.HasMany(x => x.ElectroMagneticPlants).WithOne(x => x.HotspotClass);
                b.HasMany(x => x.MiningOutposts).WithOne(x => x.HotspotClass);
            });

            modelBuilder.Entity<ElectroMagneticPlant>(b =>
            {
                b.HasOne(x => x.HotspotClass).WithMany(x => x.ElectroMagneticPlants);
                b.HasMany(x => x.Generators).WithOne(x => x.Plant);
            });

            modelBuilder.Entity<ElectromagneticGenerator>(b =>
            {
                b.HasOne(x => x.Plant);
            });

            modelBuilder.Entity<RawResource>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(b => b.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                b.HasOne(x => x.RawResourceType)
                    .WithMany(x => x.RawResources)
                    .HasForeignKey(x => x.RawResourceTypeId);
            });

            modelBuilder.Entity<Recipie>(b =>
            {
                b.HasMany(x => x.NeededResources).WithOne(x => x.Recipie).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.ResultingItem).WithMany(x => x.Recipies).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CraftableItem>(b =>
            {
                b.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                b.HasMany(x => x.Recipies).WithOne(x => x.ResultingItem);
            });

            modelBuilder.Entity<NeededResource>(b =>
            {
                b.HasOne(x => x.CraftableItem);
                b.HasOne(x => x.RawResource);
            });

            modelBuilder.Entity<RawResourceType>(b =>
            {
                b.HasData(
                    new RawResourceType(id: 1, resourceTypeName: ResourceTypesNames.MinedMineral),
                    new RawResourceType(id: 2, resourceTypeName: ResourceTypesNames.HarvestedPlant),
                    new RawResourceType(id: 3, resourceTypeName: ResourceTypesNames.Gas),
                    new RawResourceType(id: 4, resourceTypeName: ResourceTypesNames.CompressedMineral)
                );
            });

            this.SeedInitialData(modelBuilder);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            var classS = new HotspotClass
                { Id = 1, Class = HotspotClassesNames.ClassS, MaxConcentration = 100 };
            var classA = new HotspotClass
                { Id = 2, Class = HotspotClassesNames.ClassA, MaxConcentration = 80 };
            var classB = new HotspotClass
                { Id = 3, Class = HotspotClassesNames.ClassB, MaxConcentration = 60 };
            var classC = new HotspotClass
                { Id = 4, Class = HotspotClassesNames.ClassC, MaxConcentration = 40 };

            modelBuilder.Entity<HotspotClass>().HasData(classA, classB, classC, classS);

            var ammonia = new RawResource(id: 1, name: ResourcesNames.Ammonia, rawResourceTypeId: 1) { Value = 62 };
            var cactusFlesh = new RawResource(id: 2, name: ResourcesNames.CactusFlesh, rawResourceTypeId: 2) { Value = 28 };
            var condencedCarbon = new RawResource(id: 3, name: ResourcesNames.CondencedCarbon, rawResourceTypeId: 4) { Value = 24 };
            var dioxite = new RawResource(id: 4, name: ResourcesNames.Dioxite, rawResourceTypeId: 1) { Value = 62 };
            var faecium = new RawResource(id: 5, name: ResourcesNames.Faecium, rawResourceTypeId: 2) { Value = 30 };
            var frostCrystal = new RawResource(id: 6, name: ResourcesNames.FrostCrystal, rawResourceTypeId: 2) { Value = 12 };
            var fungalMold = new RawResource(id: 7, name: ResourcesNames.FungalMold, rawResourceTypeId: 2) { Value = 16 };
            var gammaRoot = new RawResource(id: 8, name: ResourcesNames.GammaRoot, rawResourceTypeId: 2) { Value = 16 };
            var ionizedCobalt = new RawResource(id: 9, name: ResourcesNames.IonizedCobalt, rawResourceTypeId: 4) { Value = 401 };
            var mordite = new RawResource(id: 10, name: ResourcesNames.Mordite, rawResourceTypeId: 2) { Value = 40 };
            var nitorgen = new RawResource(id: 11, name: ResourcesNames.Nitrogen, rawResourceTypeId: 3) { Value = 20 };
            var paraffinium = new RawResource(id: 12, name: ResourcesNames.Paraffinium, rawResourceTypeId: 1) { Value = 62 };
            var phosphorus = new RawResource(id: 13, name: ResourcesNames.Phosphorus, rawResourceTypeId: 1) { Value = 62 };
            var pureFerrite = new RawResource(id: 14, name: ResourcesNames.PureFerrite, rawResourceTypeId: 4) { Value = 28 };
            var pyrite = new RawResource(id: 15, name: ResourcesNames.Pyrite, rawResourceTypeId: 1) { Value = 62 };
            var radon = new RawResource(id: 16, name: ResourcesNames.Radon, rawResourceTypeId: 3) { Value = 20 };
            var solanium = new RawResource(id: 17, name: ResourcesNames.Solanium, rawResourceTypeId: 2) { Value = 70 };
            var starBulb = new RawResource(id: 18, name: ResourcesNames.StarBulb, rawResourceTypeId: 2) { Value = 32 };
            var sulphurine = new RawResource(id: 19, name: ResourcesNames.Sulphurine, rawResourceTypeId: 3) { Value = 20 };
            var uranium = new RawResource(id: 20, name: ResourcesNames.Uranium, rawResourceTypeId: 1) { Value = 62 };

            modelBuilder.Entity<RawResource>().HasData(
                ammonia, cactusFlesh, condencedCarbon, dioxite, faecium, frostCrystal, fungalMold, gammaRoot, ionizedCobalt,
                mordite, nitorgen, paraffinium, phosphorus, pureFerrite, pyrite, radon, solanium, starBulb, sulphurine, uranium
                );

            var acid = new CraftableItem(id: 1, name: CraftedItemsNames.Acid) { Value = 188000 };
            var aronium = new CraftableItem(id: 2, name: CraftedItemsNames.Aronium) { Value = 25000 };
            var circutBoard = new CraftableItem(id: 3, name: CraftedItemsNames.CircutBoard) { Value = 916250 };
            var cryoPump = new CraftableItem(id: 4, name: CraftedItemsNames.CryoPump) { Value = 1500000 };
            var cryogenicChamber = new CraftableItem(id: 5, name: CraftedItemsNames.CryogenicChamber) { Value = 3800000 };
            var dirtyBronze = new CraftableItem(id: 6, name: CraftedItemsNames.DirtyBronze) { Value = 25000 };
            var enrichedCarbon = new CraftableItem(id: 7, name: CraftedItemsNames.EnrichedCarbon) { Value = 50000 };
            var fusionAccelerant = new CraftableItem(id: 8, name: CraftedItemsNames.FusionAccelerant) { Value = 1500000 };
            var fusionIgnitor = new CraftableItem(id: 9, name: CraftedItemsNames.FusionIgnitor) { Value = 15600000 };
            var glass = new CraftableItem(id: 10, name: CraftedItemsNames.Glass) { Value = 200 };
            var goedesite = new CraftableItem(id: 11, name: CraftedItemsNames.Goedesite) { Value = 150000 };
            var grantine = new CraftableItem(id: 12, name: CraftedItemsNames.Grantine) { Value = 25000 };
            var heatCapacitor = new CraftableItem(id: 13, name: CraftedItemsNames.HeatCapacitor) { Value = 188000 };
            var herox = new CraftableItem(id: 14, name: CraftedItemsNames.Herox) { Value = 25000 };
            var hotIce = new CraftableItem(id: 15, name: CraftedItemsNames.HotIce) { Value = 320000 };
            var irideste = new CraftableItem(id: 31, name: CraftedItemsNames.Iridesite) { Value = 150000 };
            var lemmium = new CraftableItem(id: 16, name: CraftedItemsNames.Lemmium) { Value = 25000 };
            var liquidExplosive = new CraftableItem(id: 17, name: CraftedItemsNames.LiquidExplosive) { Value = 800500 };
            var livingGlass = new CraftableItem(id: 18, name: CraftedItemsNames.LivingGlass) { Value = 566000 };
            var lubricant = new CraftableItem(id: 19, name: CraftedItemsNames.Lubricant) { Value = 110000 };
            var magnoGold = new CraftableItem(id: 20, name: CraftedItemsNames.MagnoGold) { Value = 25000 };
            var nitrogenSalt = new CraftableItem(id: 21, name: CraftedItemsNames.NitrogenSalt) { Value = 50000 };
            var organicCatalyst = new CraftableItem(id: 22, name: CraftedItemsNames.OrganicCatalyst) { Value = 320000 };
            var polyFibre = new CraftableItem(id: 23, name: CraftedItemsNames.PolyFibre) { Value = 130000 };
            var portableReactor = new CraftableItem(id: 24, name: CraftedItemsNames.PortableReactor) { Value = 4200000 };
            var quantumProcessor = new CraftableItem(id: 25, name: CraftedItemsNames.QuantumProcessor) { Value = 5200000 };
            var semiconductor = new CraftableItem(id: 26, name: CraftedItemsNames.Semiconductor) { Value = 400000 };
            var stasisDevice = new CraftableItem(id: 27, name: CraftedItemsNames.StasisDevice) { Value = 15600000 };
            var superconductor = new CraftableItem(id: 28, name: CraftedItemsNames.Superconductor) { Value = 1500000 };
            var thermicCondensate = new CraftableItem(id: 29, name: CraftedItemsNames.ThermicCondensate) { Value = 50000 };
            var unstableGel = new CraftableItem(id: 30, name: CraftedItemsNames.UnstableGel) { Value = 50000 };

            modelBuilder.Entity<CraftableItem>().HasData(
                acid, aronium, circutBoard, cryoPump, cryogenicChamber, dirtyBronze, enrichedCarbon, fusionAccelerant, fusionIgnitor,
                glass, goedesite, grantine, heatCapacitor, herox, hotIce, lemmium, liquidExplosive, livingGlass, lubricant, magnoGold,
                nitrogenSalt, organicCatalyst, polyFibre, portableReactor, quantumProcessor, semiconductor, stasisDevice, superconductor,
                thermicCondensate, unstableGel, irideste
                );

            modelBuilder.Entity<Recipie>().HasData(
                new Recipie(id: 1, resultingItemId: 1),
                new Recipie(id: 2, resultingItemId: 2),
                new Recipie(id: 3, resultingItemId: 3),
                new Recipie(id: 4, resultingItemId: 4),
                new Recipie(id: 5, resultingItemId: 5),
                new Recipie(id: 6, resultingItemId: 6),
                new Recipie(id: 7, resultingItemId: 7),
                new Recipie(id: 8, resultingItemId: 8),
                new Recipie(id: 9, resultingItemId: 9),
                new Recipie(id: 10, resultingItemId: 10),
                new Recipie(id: 11, resultingItemId: 11),
                new Recipie(id: 12, resultingItemId: 12),
                new Recipie(id: 13, resultingItemId: 13),
                new Recipie(id: 14, resultingItemId: 14),
                new Recipie(id: 15, resultingItemId: 15),
                new Recipie(id: 16, resultingItemId: 16),
                new Recipie(id: 17, resultingItemId: 17),
                new Recipie(id: 18, resultingItemId: 18),
                new Recipie(id: 19, resultingItemId: 19),
                new Recipie(id: 20, resultingItemId: 20),
                new Recipie(id: 21, resultingItemId: 21),
                new Recipie(id: 22, resultingItemId: 22),
                new Recipie(id: 23, resultingItemId: 23),
                new Recipie(id: 24, resultingItemId: 24),
                new Recipie(id: 25, resultingItemId: 25),
                new Recipie(id: 26, resultingItemId: 26),
                new Recipie(id: 27, resultingItemId: 27),
                new Recipie(id: 28, resultingItemId: 28),
                new Recipie(id: 29, resultingItemId: 29),
                new Recipie(id: 30, resultingItemId: 30),
                new Recipie(id: 31, resultingItemId: 31)
                );

            modelBuilder.Entity<NeededResource>().HasData(
                ////Acid
                new NeededResource() { Id = 1, RecipieId = 1, RawResourceId = mordite.Id, NeededAmount = 25 },
                new NeededResource() { Id = 2, RecipieId = 1, RawResourceId = fungalMold.Id, NeededAmount = 600 },
                ////Aronium
                new NeededResource() { Id = 3, RecipieId = 2, RawResourceId = paraffinium.Id, NeededAmount = 50 },
                new NeededResource() { Id = 4, RecipieId = 2, RawResourceId = ionizedCobalt.Id, NeededAmount = 50 },
                ////circutBoard
                new NeededResource() { Id = 5, RecipieId = 3, CraftableItemId = heatCapacitor.Id, NeededAmount = 1 },
                new NeededResource() { Id = 6, RecipieId = 3, CraftableItemId = polyFibre.Id, NeededAmount = 1 },
                ////cryoPump
                new NeededResource() { Id = 7, RecipieId = 4, CraftableItemId = hotIce.Id, NeededAmount = 1 },
                new NeededResource() { Id = 8, RecipieId = 4, CraftableItemId = thermicCondensate.Id, NeededAmount = 1 },
                ////cryogenicChamber
                new NeededResource() { Id = 9, RecipieId = 5, CraftableItemId = cryoPump.Id, NeededAmount = 1 },
                new NeededResource() { Id = 10, RecipieId = 5, CraftableItemId = livingGlass.Id, NeededAmount = 1 },
                ////dirtyBronze
                new NeededResource() { Id = 11, RecipieId = 6, RawResourceId = pureFerrite.Id, NeededAmount = 100 },
                new NeededResource() { Id = 12, RecipieId = 6, RawResourceId = pyrite.Id, NeededAmount = 50 },
                ////enrichedCarbon
                new NeededResource() { Id = 13, RecipieId = 7, RawResourceId = radon.Id, NeededAmount = 250 },
                new NeededResource() { Id = 14, RecipieId = 7, RawResourceId = condencedCarbon.Id, NeededAmount = 50 },
                ////fusionAccelerant
                new NeededResource() { Id = 15, RecipieId = 8, CraftableItemId = organicCatalyst.Id, NeededAmount = 1 },
                new NeededResource() { Id = 16, RecipieId = 8, CraftableItemId = nitrogenSalt.Id, NeededAmount = 1 },
                ////fusionIgnitor
                new NeededResource() { Id = 17, RecipieId = 9, CraftableItemId = quantumProcessor.Id, NeededAmount = 1 },
                new NeededResource() { Id = 18, RecipieId = 9, CraftableItemId = portableReactor.Id, NeededAmount = 1 },
                new NeededResource() { Id = 19, RecipieId = 9, CraftableItemId = goedesite.Id, NeededAmount = 1 },
                ////glass
                new NeededResource() { Id = 20, RecipieId = 10, RawResourceId = frostCrystal.Id, NeededAmount = 40 },
                ////goedesite
                new NeededResource() { Id = 21, RecipieId = 11, CraftableItemId = dirtyBronze.Id, NeededAmount = 1 },
                new NeededResource() { Id = 22, RecipieId = 11, CraftableItemId = lemmium.Id, NeededAmount = 1 },
                new NeededResource() { Id = 23, RecipieId = 11, CraftableItemId = herox.Id, NeededAmount = 1 },
                ////grantine
                new NeededResource() { Id = 24, RecipieId = 12, RawResourceId = ionizedCobalt.Id, NeededAmount = 50 },
                new NeededResource() { Id = 25, RecipieId = 12, RawResourceId = dioxite.Id, NeededAmount = 50 },
                ////heatCapacitor
                new NeededResource() { Id = 26, RecipieId = 13, RawResourceId = frostCrystal.Id, NeededAmount = 100 },
                new NeededResource() { Id = 27, RecipieId = 13, RawResourceId = solanium.Id, NeededAmount = 200 },
                ////herox
                new NeededResource() { Id = 28, RecipieId = 14, RawResourceId = ionizedCobalt.Id, NeededAmount = 50 },
                new NeededResource() { Id = 29, RecipieId = 14, RawResourceId = ammonia.Id, NeededAmount = 50 },
                ////hotIce
                new NeededResource() { Id = 30, RecipieId = 15, CraftableItemId = enrichedCarbon.Id, NeededAmount = 1 },
                new NeededResource() { Id = 31, RecipieId = 15, CraftableItemId = nitrogenSalt.Id, NeededAmount = 1 },
                ////lemmium
                new NeededResource() { Id = 32, RecipieId = 16, RawResourceId = pureFerrite.Id, NeededAmount = 100 },
                new NeededResource() { Id = 33, RecipieId = 16, RawResourceId = uranium.Id, NeededAmount = 50 },
                ////liquidExplosive
                new NeededResource() { Id = 34, RecipieId = 17, CraftableItemId = acid.Id, NeededAmount = 1 },
                new NeededResource() { Id = 35, RecipieId = 17, CraftableItemId = unstableGel.Id, NeededAmount = 1 },
                ////livingGlass
                new NeededResource() { Id = 36, RecipieId = 18, CraftableItemId = glass.Id, NeededAmount = 5 },
                new NeededResource() { Id = 37, RecipieId = 18, CraftableItemId = lubricant.Id, NeededAmount = 1 },
                ////lubricant
                new NeededResource() { Id = 38, RecipieId = 19, RawResourceId = faecium.Id, NeededAmount = 50 },
                new NeededResource() { Id = 39, RecipieId = 19, RawResourceId = gammaRoot.Id, NeededAmount = 400 },
                ////magnoGold
                new NeededResource() { Id = 40, RecipieId = 20, RawResourceId = ionizedCobalt.Id, NeededAmount = 50 },
                new NeededResource() { Id = 41, RecipieId = 20, RawResourceId = phosphorus.Id, NeededAmount = 50 },
                ////nitrogenSalt
                new NeededResource() { Id = 42, RecipieId = 21, RawResourceId = nitorgen.Id, NeededAmount = 250 },
                new NeededResource() { Id = 43, RecipieId = 21, RawResourceId = condencedCarbon.Id, NeededAmount = 50 },
                ////organicCatalyst
                new NeededResource() { Id = 44, RecipieId = 22, CraftableItemId = thermicCondensate.Id, NeededAmount = 1 },
                new NeededResource() { Id = 45, RecipieId = 22, CraftableItemId = enrichedCarbon.Id, NeededAmount = 1 },
                ////polyFibre
                new NeededResource() { Id = 46, RecipieId = 23, RawResourceId = cactusFlesh.Id, NeededAmount = 100 },
                new NeededResource() { Id = 47, RecipieId = 23, RawResourceId = starBulb.Id, NeededAmount = 200 },
                ////portableReactor
                new NeededResource() { Id = 48, RecipieId = 24, CraftableItemId = fusionAccelerant.Id, NeededAmount = 1 },
                new NeededResource() { Id = 49, RecipieId = 24, CraftableItemId = liquidExplosive.Id, NeededAmount = 1 },
                ////quantumProcessor
                new NeededResource() { Id = 50, RecipieId = 25, CraftableItemId = superconductor.Id, NeededAmount = 1 },
                new NeededResource() { Id = 51, RecipieId = 25, CraftableItemId = circutBoard.Id, NeededAmount = 1 },
                ////semiconductor
                new NeededResource() { Id = 52, RecipieId = 26, CraftableItemId = thermicCondensate.Id, NeededAmount = 1 },
                new NeededResource() { Id = 53, RecipieId = 26, CraftableItemId = nitrogenSalt.Id, NeededAmount = 1 },
                ////stasisDevice
                new NeededResource() { Id = 54, RecipieId = 27, CraftableItemId = irideste.Id, NeededAmount = 1 },
                new NeededResource() { Id = 55, RecipieId = 27, CraftableItemId = cryogenicChamber.Id, NeededAmount = 1 },
                new NeededResource() { Id = 56, RecipieId = 27, CraftableItemId = quantumProcessor.Id, NeededAmount = 1 },
                ////superconductor
                new NeededResource() { Id = 57, RecipieId = 28, CraftableItemId = semiconductor.Id, NeededAmount = 1 },
                new NeededResource() { Id = 58, RecipieId = 28, CraftableItemId = enrichedCarbon.Id, NeededAmount = 1 },
                ////thermicCondensate
                new NeededResource() { Id = 59, RecipieId = 29, RawResourceId = sulphurine.Id, NeededAmount = 250 },
                new NeededResource() { Id = 60, RecipieId = 29, RawResourceId = condencedCarbon.Id, NeededAmount = 50 },
                ////unstableGel
                new NeededResource() { Id = 61, RecipieId = 30, RawResourceId = cactusFlesh.Id, NeededAmount = 200 },
                ////irideste
                new NeededResource() { Id = 62, RecipieId = 31, CraftableItemId = grantine.Id, NeededAmount = 1 },
                new NeededResource() { Id = 63, RecipieId = 31, CraftableItemId = magnoGold.Id, NeededAmount = 1 },
                new NeededResource() { Id = 64, RecipieId = 31, CraftableItemId = aronium.Id, NeededAmount = 1 }

                );
        }
    }
}

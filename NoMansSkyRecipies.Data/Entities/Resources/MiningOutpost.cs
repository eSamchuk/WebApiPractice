using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Nms.StaticData.Constants;
using NoMansSkyRecipies.Data.Entities.Energy;

namespace NoMansSkyRecipies.Data.Entities.Resources
{
    public class MiningOutpost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ResourceTypeId { get; set; }

        public RawResource ResourceType { get; set; }

        [Required]
        public int HotspotClassId { get; set; }

        public HotspotClass HotspotClass { get; set; }

        [Required]
        public int EnergySupplyId { get; set; }
        public EnergySupply EnergySupply { get; set; }

        [Required]
        public int SupplyDepots { get; set; }

        [Required]
        public bool HaveTeleport { get; set; } = true;

        public List<Extractor> Extractors { get; set; }

        [NotMapped]
        public int RequiredPower => this.ExtractorCount * Constants.ExtractorConsumption +
                                    (this.HaveTeleport ? Constants.TeleporterConsumption : 0);

        [NotMapped]
        public int MaxResourceStored => this.SupplyDepots * Constants.SupplyDepotCapacity +
                                        this.ExtractorCount * Constants.ExtractorInnerStorage;

        [NotMapped]
        public int TotalExtractionHourRate => this.Extractors.Sum(x => x.ExtractionRate);

        [NotMapped]
        public int ExtractorCount => this.Extractors.Count();

        [NotMapped]
        public int EnergyBalance
        {
            get
            {
                var supply = this.EnergySupply;
                int absolutePowerIncome = supply.DayTimeOutput * Constants.DayDurationSeconds +
                                  2 * supply.IntermediateTimeOutput * Constants.IntermediateDurationSeconds +
                                  supply.NightTimeOutput * Constants.NightDurationSeconds;

                var daytimeEnegryBalance = (supply.DayTimeOutput * Constants.DayDurationSeconds) -
                                           (this.RequiredPower * Constants.DayDurationSeconds);

                var reservePotential = daytimeEnegryBalance > supply.MaxEnergyReserve ? supply.MaxEnergyReserve : daytimeEnegryBalance;

                var nightEnergyConsumption = this.RequiredPower * Constants.NightDurationSeconds;

                var result = reservePotential - nightEnergyConsumption;

                return result;
            }
        }
    }
}

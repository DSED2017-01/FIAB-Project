using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class TANK_LOG
    {
        public TANK_LOG()
        {
            TANK_LOG_DAILY = new HashSet<TANK_LOG_DAILY>();
        }

        public int IdPk { get; set; }
        public int? PeriodFk { get; set; }
        public int TankFk { get; set; }
        public int? SpeciesFk { get; set; }
        public string SpeciesText { get; set; }
        public string SpeciesText2 { get; set; }
        public int Qty { get; set; }
        public string Comment { get; set; }
        public int StuffFk { get; set; }
        public int? OrderFk { get; set; }
        public int? SizeFk { get; set; }

        public MOVEMENT_PERIOD PeriodFkNavigation { get; set; }
        public RECORD_PET_SIZE SizeFkNavigation { get; set; }
        public MARINE_SPECIES SpeciesFkNavigation { get; set; }
        public SYS_STUFF StuffFkNavigation { get; set; }
        public TANK TankFkNavigation { get; set; }
        public ICollection<TANK_LOG_DAILY> TANK_LOG_DAILY { get; set; }
    }
}

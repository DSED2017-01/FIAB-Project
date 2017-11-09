using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class TankLog
    {
        public TankLog()
        {
            TankLogDaily = new HashSet<TankLogDaily>();
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

        public MovementPeriod PeriodFkNavigation { get; set; }
        public RecordPetSize SizeFkNavigation { get; set; }
        public MarineSpecies SpeciesFkNavigation { get; set; }
        public SysStuff StuffFkNavigation { get; set; }
        public Tank TankFkNavigation { get; set; }
        public ICollection<TankLogDaily> TankLogDaily { get; set; }
    }
}

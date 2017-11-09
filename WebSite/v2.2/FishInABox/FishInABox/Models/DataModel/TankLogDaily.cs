using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class TankLogDaily
    {
        public int IdPk { get; set; }
        public DateTime LogDate { get; set; }
        public int LogFk { get; set; }
        public int ReasonFk { get; set; }
        public int Qty { get; set; }
        public string Comment { get; set; }
        public int? StuffFk { get; set; }

        public TankLog LogFkNavigation { get; set; }
        public ReasonMortality ReasonFkNavigation { get; set; }
        public SysStuff StuffFkNavigation { get; set; }
    }
}

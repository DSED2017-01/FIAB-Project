using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class TANK_LOG_DAILY
    {
        public int IdPk { get; set; }
        public DateTime LogDate { get; set; }
        public int LogFk { get; set; }
        public int ReasonFk { get; set; }
        public int Qty { get; set; }
        public string Comment { get; set; }
        public int? StuffFk { get; set; }

        public TANK_LOG LogFkNavigation { get; set; }
        public REASON_MORTALITY ReasonFkNavigation { get; set; }
        public SYS_STUFF StuffFkNavigation { get; set; }
    }
}

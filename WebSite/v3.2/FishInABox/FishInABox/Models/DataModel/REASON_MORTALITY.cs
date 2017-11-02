using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class REASON_MORTALITY
    {
        public REASON_MORTALITY()
        {
            TANK_LOG_DAILY = new HashSet<TANK_LOG_DAILY>();
        }

        public int IdPk { get; set; }
        public string IdCode { get; set; }
        public string Text { get; set; }

        public ICollection<TANK_LOG_DAILY> TANK_LOG_DAILY { get; set; }
    }
}

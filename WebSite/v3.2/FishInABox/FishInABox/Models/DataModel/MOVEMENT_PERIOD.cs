using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MOVEMENT_PERIOD
    {
        public MOVEMENT_PERIOD()
        {
            TANK_LOG = new HashSet<TANK_LOG>();
        }

        public int IdPk { get; set; }
        public DateTime StartDate { get; set; }
        public string Text { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool? ClosedFlag { get; set; }

        public ICollection<TANK_LOG> TANK_LOG { get; set; }
    }
}

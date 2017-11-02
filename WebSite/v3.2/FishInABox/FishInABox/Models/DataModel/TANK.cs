using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class TANK
    {
        public TANK()
        {
            TANK_LOG = new HashSet<TANK_LOG>();
        }

        public int IdPk { get; set; }
        public int BayFk { get; set; }
        public string IdCode { get; set; }
        public string Text { get; set; }
        public string Rfid { get; set; }

        public TANK_BAY BayFkNavigation { get; set; }
        public ICollection<TANK_LOG> TANK_LOG { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class TANK_BAY
    {
        public TANK_BAY()
        {
            TANK = new HashSet<TANK>();
        }

        public int IdPk { get; set; }
        public string IdCode { get; set; }
        public string Text { get; set; }

        public ICollection<TANK> TANK { get; set; }
    }
}

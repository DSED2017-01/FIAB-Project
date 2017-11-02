using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class SYS_STUFF
    {
        public SYS_STUFF()
        {
            TANK_LOG = new HashSet<TANK_LOG>();
            TANK_LOG_DAILY = new HashSet<TANK_LOG_DAILY>();
        }

        public int IdPk { get; set; }
        public string IdCode { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public ICollection<TANK_LOG> TANK_LOG { get; set; }
        public ICollection<TANK_LOG_DAILY> TANK_LOG_DAILY { get; set; }
    }
}

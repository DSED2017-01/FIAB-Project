using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class Tank
    {
        public Tank()
        {
            TankLog = new HashSet<TankLog>();
        }

        public int IdPk { get; set; }
        public int BayFk { get; set; }
        public string IdCode { get; set; }
        public string Text { get; set; }
        public string Rfid { get; set; }

        public TankBay BayFkNavigation { get; set; }
        public ICollection<TankLog> TankLog { get; set; }
    }
}

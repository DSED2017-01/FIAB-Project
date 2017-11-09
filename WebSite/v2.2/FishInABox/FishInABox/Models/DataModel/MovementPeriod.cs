using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MovementPeriod
    {
        public MovementPeriod()
        {
            TankLog = new HashSet<TankLog>();
        }

        public int IdPk { get; set; }
        public DateTime StartDate { get; set; }
        public string Text { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool? ClosedFlag { get; set; }

        public ICollection<TankLog> TankLog { get; set; }
    }
}

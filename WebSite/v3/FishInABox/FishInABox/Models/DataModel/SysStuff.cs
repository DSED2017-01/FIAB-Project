using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class SysStuff
    {
        public SysStuff()
        {
            TankLog = new HashSet<TankLog>();
            TankLogDaily = new HashSet<TankLogDaily>();
        }

        public int IdPk { get; set; }
        public string IdCode { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public ICollection<TankLog> TankLog { get; set; }
        public ICollection<TankLogDaily> TankLogDaily { get; set; }
    }
}

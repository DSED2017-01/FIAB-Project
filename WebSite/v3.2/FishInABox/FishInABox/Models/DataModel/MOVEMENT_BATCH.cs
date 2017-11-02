using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MOVEMENT_BATCH
    {
        public int IdPk { get; set; }
        public int ItemFk { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MovementBatch
    {
        public int IdPk { get; set; }
        public int ItemFk { get; set; }
        public int Quantity { get; set; }
    }
}

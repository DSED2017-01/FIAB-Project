using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("MOVEMENT_BATCH")]
    public partial class MovementBatch
    {
        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("ITEM_FK")]
        public int ItemFk { get; set; }
        [Column("QUANTITY")]
        public int Quantity { get; set; }
    }
}

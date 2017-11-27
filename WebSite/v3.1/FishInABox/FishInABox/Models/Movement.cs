using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("MOVEMENT")]
    public partial class Movement
    {
        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("TIMESTAMP", TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
        [Column("FROM_FK")]
        public int FromFk { get; set; }
        [Column("TANK_FK")]
        public int TankFk { get; set; }
        [Column("INTIAL_QTY")]
        public int IntialQty { get; set; }
        [Column("QTY_MOVED")]
        public int QtyMoved { get; set; }
        [Column("PERIOD_FK")]
        public int PeriodFk { get; set; }
        [Column("BATCH_FK")]
        public int BatchFk { get; set; }
        [Column("DAY")]
        public int Day { get; set; }
        [Column("COMMENT")]
        [StringLength(100)]
        public string Comment { get; set; }
    }
}

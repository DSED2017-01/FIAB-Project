using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("MOVEMENT_PERIOD")]
    public partial class MovementPeriod
    {
        public MovementPeriod()
        {
            TankLog = new HashSet<TankLog>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("START_DATE", TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column("TEXT")]
        [StringLength(50)]
        public string Text { get; set; }
        [Column("CLOSED_DATE", TypeName = "datetime")]
        public DateTime? ClosedDate { get; set; }
        [Column("CLOSED_FLAG")]
        public bool ClosedFlag { get; set; }

        [InverseProperty("PeriodFkNavigation")]
        public ICollection<TankLog> TankLog { get; set; }
    }
}

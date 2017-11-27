using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("TANK_LOG_DAILY")]
    public partial class TankLogDaily
    {
        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("LOG_DATE", TypeName = "datetime")]
        public DateTime LogDate { get; set; }
        [Column("LOG_FK")]
        public int LogFk { get; set; }
        [Column("REASON_FK")]
        public int ReasonFk { get; set; }
        [Column("QTY")]
        public int Qty { get; set; }
        [Column("COMMENT")]
        [StringLength(100)]
        public string Comment { get; set; }
        [Column("STUFF_FK")]
        public int? StuffFk { get; set; }

        [ForeignKey("LogFk")]
        [InverseProperty("TankLogDaily")]
        public TankLog LogFkNavigation { get; set; }
        [ForeignKey("ReasonFk")]
        [InverseProperty("TankLogDaily")]
        public ReasonMortality ReasonFkNavigation { get; set; }
        [ForeignKey("StuffFk")]
        [InverseProperty("TankLogDaily")]
        public SysStuff StuffFkNavigation { get; set; }
    }
}

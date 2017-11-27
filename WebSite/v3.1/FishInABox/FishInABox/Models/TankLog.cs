using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("TANK_LOG")]
    public partial class TankLog
    {
        public TankLog()
        {
            TankLogDaily = new HashSet<TankLogDaily>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("PERIOD_FK")]
        public int? PeriodFk { get; set; }
        [Column("TANK_FK")]
        public int TankFk { get; set; }
        [Column("SPECIES_FK")]
        public int? SpeciesFk { get; set; }
        [Column("SPECIES_TEXT")]
        [StringLength(50)]
        public string SpeciesText { get; set; }
        [Column("SPECIES_TEXT_2")]
        [StringLength(50)]
        public string SpeciesText2 { get; set; }
        [Column("QTY")]
        public int Qty { get; set; }
        [Column("COMMENT")]
        [StringLength(100)]
        public string Comment { get; set; }
        [Column("STUFF_FK")]
        public int StuffFk { get; set; }
        [Column("ORDER_FK")]
        public int? OrderFk { get; set; }
        [Column("SIZE_FK")]
        public int? SizeFk { get; set; }

        [ForeignKey("PeriodFk")]
        [InverseProperty("TankLog")]
        public MovementPeriod PeriodFkNavigation { get; set; }
        [ForeignKey("SizeFk")]
        [InverseProperty("TankLog")]
        public RecordPetSize SizeFkNavigation { get; set; }
        [ForeignKey("SpeciesFk")]
        [InverseProperty("TankLog")]
        public MarineSpecies SpeciesFkNavigation { get; set; }
        [ForeignKey("StuffFk")]
        [InverseProperty("TankLog")]
        public SysStuff StuffFkNavigation { get; set; }
        [ForeignKey("TankFk")]
        [InverseProperty("TankLog")]
        public Tank TankFkNavigation { get; set; }
        [InverseProperty("LogFkNavigation")]
        public ICollection<TankLogDaily> TankLogDaily { get; set; }
    }
}

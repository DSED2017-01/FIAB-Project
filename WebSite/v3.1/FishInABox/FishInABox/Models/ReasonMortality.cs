using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("REASON_MORTALITY")]
    public partial class ReasonMortality
    {
        public ReasonMortality()
        {
            TankLogDaily = new HashSet<TankLogDaily>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("ID_CODE")]
        [StringLength(10)]
        public string IdCode { get; set; }
        [Column("TEXT")]
        [StringLength(100)]
        public string Text { get; set; }

        [InverseProperty("ReasonFkNavigation")]
        public ICollection<TankLogDaily> TankLogDaily { get; set; }
    }
}

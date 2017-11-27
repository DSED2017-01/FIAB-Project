using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("TANK_BAY")]
    public partial class TankBay
    {
        public TankBay()
        {
            Tank = new HashSet<Tank>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Required]
        [Column("ID_CODE")]
        [StringLength(3)]
        public string IdCode { get; set; }
        [Column("TEXT")]
        [StringLength(100)]
        public string Text { get; set; }

        [InverseProperty("BayFkNavigation")]
        public ICollection<Tank> Tank { get; set; }
    }
}

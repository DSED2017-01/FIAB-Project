using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("TANK")]
    public partial class Tank
    {
        public Tank()
        {
            TankLog = new HashSet<TankLog>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("BAY_FK")]
        public int BayFk { get; set; }
        [Required]
        [Column("ID_CODE")]
        [StringLength(10)]
        public string IdCode { get; set; }
        [Column("TEXT")]
        [StringLength(100)]
        public string Text { get; set; }
        [Column("RFID")]
        [StringLength(20)]
        public string Rfid { get; set; }

        [ForeignKey("BayFk")]
        [InverseProperty("Tank")]
        public TankBay BayFkNavigation { get; set; }
        [InverseProperty("TankFkNavigation")]
        public ICollection<TankLog> TankLog { get; set; }
    }
}

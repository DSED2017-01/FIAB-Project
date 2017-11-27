using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("SYS_STUFF")]
    public partial class SysStuff
    {
        public SysStuff()
        {
            TankLog = new HashSet<TankLog>();
            TankLogDaily = new HashSet<TankLogDaily>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Required]
        [Column("ID_CODE")]
        [StringLength(10)]
        public string IdCode { get; set; }
        [Column("FAMILY_NAME")]
        [StringLength(25)]
        public string FamilyName { get; set; }
        [Column("FIRST_NAME")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("MIDDLE_NAME")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [InverseProperty("StuffFkNavigation")]
        public ICollection<TankLog> TankLog { get; set; }
        [InverseProperty("StuffFkNavigation")]
        public ICollection<TankLogDaily> TankLogDaily { get; set; }
    }
}

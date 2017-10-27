//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FishInABox.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TANK_LOG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TANK_LOG()
        {
            this.TANK_LOG_DAILY = new HashSet<TANK_LOG_DAILY>();
        }
    
        public int ID_PK { get; set; }
        [Display(Name = "Period")]
        public Nullable<int> PERIOD_FK { get; set; }
        [Display(Name = "Tank ID")]
        public int TANK_FK { get; set; }
        [Display(Name ="Species Record")]
        public Nullable<int> SPECIES_FK { get; set; }
        [Display(Name = "Species Description")]
        public string SPECIES_TEXT { get; set; }
        [Display(Name = "Species Other Info.")]
        public string SPECIES_TEXT_2 { get; set; }
        [Display(Name = "Quantity")]
        public int QTY { get; set; }
        [Display(Name = "Initial Information")]
        public string COMMENT { get; set; }
        [Display(Name = "Stuff ID")]
        public int STUFF_FK { get; set; }
    
        public virtual MOVEMENT_PERIOD MOVEMENT_PERIOD { get; set; }
        public virtual TANK TANK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TANK_LOG_DAILY> TANK_LOG_DAILY { get; set; }
        public virtual SYS_STUFF SYS_STUFF { get; set; }
    }
}

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

    public partial class SYS_STUFF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_STUFF()
        {
            this.TANK_LOG_DAILY = new HashSet<TANK_LOG_DAILY>();
            this.TANK_LOG = new HashSet<TANK_LOG>();
        }
    
        public int ID_PK { get; set; }
        [Display(Name = "Stuff ID")]
        public string ID_CODE { get; set; }
        [Display(Name = "Family Name")]
        public string FAMILY_NAME { get; set; }
        [Display(Name = "First Name")]
        public string FIRST_NAME { get; set; }
        [Display(Name = "Middle Name")]
        public string MIDDLE_NAME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TANK_LOG_DAILY> TANK_LOG_DAILY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TANK_LOG> TANK_LOG { get; set; }
    }
}

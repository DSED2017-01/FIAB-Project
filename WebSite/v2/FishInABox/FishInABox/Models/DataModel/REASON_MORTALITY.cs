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
    
    public partial class REASON_MORTALITY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REASON_MORTALITY()
        {
            this.TANK_LOG_DAILY = new HashSet<TANK_LOG_DAILY>();
        }
    
        public int ID_PK { get; set; }
        public string ID_CODE { get; set; }
        public string TEXT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TANK_LOG_DAILY> TANK_LOG_DAILY { get; set; }
    }
}

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
    
    public partial class MOVEMENT_PERIOD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MOVEMENT_PERIOD()
        {
            this.TANK_LOG = new HashSet<TANK_LOG>();
        }
    
        public int ID_PK { get; set; }
        public System.DateTime START_DATE { get; set; }
        public string TEXT { get; set; }
        public Nullable<System.DateTime> CLOSED_DATE { get; set; }
        public bool CLOSED_FLAG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TANK_LOG> TANK_LOG { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSED06_Aquatic_Pet_Store.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MARINE_CLASS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MARINE_CLASS()
        {
            this.MARINE_SPECIES = new HashSet<MARINE_SPECIES>();
        }
    
        public int ID_PK { get; set; }
        public string TEXT { get; set; }
        public string SCHEDULE4 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MARINE_SPECIES> MARINE_SPECIES { get; set; }
    }
}

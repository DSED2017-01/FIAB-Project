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
    
    public partial class RECORD_PET
    {
        public int ID_PK { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> SPECIES_FK { get; set; }
        public int SIZE_FK { get; set; }
        public Nullable<int> GROUP_FK { get; set; }
    
        public virtual MARINE_SPECIES MARINE_SPECIES { get; set; }
        public virtual RECORD_GROUP RECORD_GROUP { get; set; }
        public virtual RECORD_PET_SIZE RECORD_PET_SIZE { get; set; }
    }
}

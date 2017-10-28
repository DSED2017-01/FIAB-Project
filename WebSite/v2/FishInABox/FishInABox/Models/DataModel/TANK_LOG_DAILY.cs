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

    public partial class TANK_LOG_DAILY
    {
        public int ID_PK { get; set; }
        public int LOG_FK { get; set; }
        public int REASON_FK { get; set; }
        public int QTY { get; set; }
        public string COMMENT { get; set; }
        public Nullable<int> STUFF_FK { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public System.DateTime LOG_DATE { get; set; }
    
        public virtual REASON_MORTALITY REASON_MORTALITY { get; set; }
        public virtual TANK_LOG TANK_LOG { get; set; }
        public virtual SYS_STUFF SYS_STUFF { get; set; }
    }
}

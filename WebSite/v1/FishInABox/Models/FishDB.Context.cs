﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FishInABox.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AQUATIC_PET_STOREEntities : DbContext
    {
        public AQUATIC_PET_STOREEntities()
            : base("name=AQUATIC_PET_STOREEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PET_GROUP> PET_GROUP { get; set; }
        public virtual DbSet<PET_INFO> PET_INFO { get; set; }
        public virtual DbSet<PET_RECORD> PET_RECORD { get; set; }
        public virtual DbSet<PET_SIZE> PET_SIZE { get; set; }
        public virtual DbSet<RECORD_PACKING> RECORD_PACKING { get; set; }
    }
}

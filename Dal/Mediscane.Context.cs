﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MedicineProjectEntities : DbContext
    {
        public MedicineProjectEntities()
            : base("name=MedicineProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MEDICINESTOCKtbl> MEDICINESTOCKtbl { get; set; }
        public virtual DbSet<MEDICINEtbl> MEDICINEtbl { get; set; }
        public virtual DbSet<REMINDERDETAILStbl> REMINDERDETAILStbl { get; set; }
        public virtual DbSet<REMINDERStbl> REMINDERStbl { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<USERStbl> USERStbl { get; set; }
    }
}

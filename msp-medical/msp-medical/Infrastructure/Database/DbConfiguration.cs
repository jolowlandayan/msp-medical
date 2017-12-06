using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using msp_medical.Infrastructure.Entities;
using msp_medical.Infrastructure.Configuration;

namespace msp_medical.Infrastructure.Database
{
    public class DbConfiguration : DbContext
    {
        public DbConfiguration() : base("name=Local") //database name
        {

        }
        public DbSet<PatientInfo> PatientInfo { get; set; }
        public DbSet<PatientPicture> PatientPicture { get; set; }
        public DbSet<State> State { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PatientConfig());
            modelBuilder.Configurations.Add(new PatientPictureConfig());
            modelBuilder.Configurations.Add(new StateConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
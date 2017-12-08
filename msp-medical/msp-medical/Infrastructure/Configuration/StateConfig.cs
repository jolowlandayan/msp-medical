using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using msp_medical.Infrastructure.Entities;
using System.Data.Entity.ModelConfiguration;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace msp_medical.Infrastructure.Configuration
{
    public class StateConfig : EntityTypeConfiguration<State>
    {
        public StateConfig()
        {
            ToTable("State");
            HasKey(x => x.Id);

            Property(x => x.Id)
              .HasColumnName("Id")
              .HasColumnType(SqlDbType.Int.ToString())
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ETag)
                .HasColumnName("ETag")
                .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Data)
                .HasColumnName("Data")
                .HasColumnType(SqlDbType.NVarChar.ToString());
        }
    }
}
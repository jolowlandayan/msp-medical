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
    public class PatientPictureConfig : EntityTypeConfiguration<PatientPicture>
    {
        public PatientPictureConfig()
        {
            ToTable("PatientPicture");
            HasKey(x => x.Id);

            Property(x => x.Id)
              .HasColumnName("Id")
              .HasColumnType(SqlDbType.Int.ToString())
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.PictureURL)
                .HasColumnName("PictureURL")
                .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType(SqlDbType.DateTime.ToString());

            Property(x => x.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType(SqlDbType.DateTime.ToString());

            HasRequired(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .WillCascadeOnDelete(false);
        }
    }
}
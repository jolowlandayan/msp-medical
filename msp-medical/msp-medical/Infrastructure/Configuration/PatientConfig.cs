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
    public class PatientConfig : EntityTypeConfiguration<PatientInfo>
    {
        public PatientConfig()
        {
            ToTable("Patients");
            HasKey(x => x.PatientId);

            Property(x => x.PatientId)
                .HasColumnName("PatientId")
                .HasColumnType(SqlDbType.Int.ToString())
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Sex)
                .HasColumnName("Sex")
                .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Age)
               .HasColumnName("Age")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.MaritalStatus)
               .HasColumnName("MaritalStatus")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Birthday)
               .HasColumnName("Birthday")
               .HasColumnType(SqlDbType.DateTime.ToString());

            Property(x => x.Address)
               .HasColumnName("Address")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.ContactNumber)
               .HasColumnName("ContactNumber")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.DateOfAdmission)
               .HasColumnName("DateOfAdmission")
               .HasColumnType(SqlDbType.DateTime.ToString());

            Property(x => x.AggravatingFactors)
               .HasColumnName("AggravatingFactors")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.RelievingFactors)
               .HasColumnName("RelievingFactors")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Intensity)
               .HasColumnName("Intensity")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Timing)
               .HasColumnName("Timing")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Medications)
               .HasColumnName("Medications")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.PreviousHospitalization)
               .HasColumnName("PreviousHospitalization")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.MedicationsTaken)
               .HasColumnName("MedicationsTaken")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Diseases)
               .HasColumnName("Diseases")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.InjuriesAccidents)
               .HasColumnName("InjuriesAccidents")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Operations)
               .HasColumnName("Operations")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Allergies)
               .HasColumnName("Allergies")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.WaterSupply)
               .HasColumnName("WaterSupply")
               .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.DrinkingWater)
              .HasColumnName("DrinkingWater")
              .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.HouseholdMembers)
              .HasColumnName("HouseholdMembers")
              .HasColumnType(SqlDbType.NVarChar.ToString());

            Property(x => x.Description)
              .HasColumnName("Description")
              .HasColumnType(SqlDbType.NVarChar.ToString());
        }
    }
}
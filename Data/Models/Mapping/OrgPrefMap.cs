using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class OrgPrefMap : EntityTypeConfiguration<OrgPref>
    {
        public OrgPrefMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PrefKey)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.PrefValue)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Description)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("OrgPrefs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.PrefKey).HasColumnName("PrefKey");
            this.Property(t => t.PrefValue).HasColumnName("PrefValue");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Organisation)
                .WithMany(t => t.OrgPrefs)
                .HasForeignKey(d => d.OrgID);

        }
    }
}

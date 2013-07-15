using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class PersonPrefMap : EntityTypeConfiguration<PersonPref>
    {
        public PersonPrefMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PrefKey)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.PrefValue)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("PersonPrefs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PersonID).HasColumnName("PersonID");
            this.Property(t => t.PrefKey).HasColumnName("PrefKey");
            this.Property(t => t.PrefValue).HasColumnName("PrefValue");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.PersonPrefs)
                .HasForeignKey(d => d.PersonID);

        }
    }
}

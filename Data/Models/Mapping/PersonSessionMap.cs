using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class PersonSessionMap : EntityTypeConfiguration<PersonSession>
    {
        public PersonSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.ConnectionID);

            // Properties
            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.UserAgent)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("PersonSession");
            this.Property(t => t.ConnectionID).HasColumnName("ConnectionID");
            this.Property(t => t.PersonID).HasColumnName("PersonID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.UserAgent).HasColumnName("UserAgent");
            //this.Property(t => t.ConnectTimestamp).HasColumnName("ConnectTimestamp");
            this.Property(t => t.DisconnectTimestamp).HasColumnName("DisconnectTimestamp");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.PersonSessions)
                .HasForeignKey(d => d.PersonID);

        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class OrganisationDomainMap : EntityTypeConfiguration<OrganisationDomain>
    {
        public OrganisationDomainMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Domain)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("OrganisationDomain");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.Domain).HasColumnName("Domain");
            this.Property(t => t.Id).HasColumnName("Id");

            // Relationships
            this.HasRequired(t => t.Organisation)
                .WithMany(t => t.OrganisationDomains)
                .HasForeignKey(d => d.OrgID);

        }
    }
}

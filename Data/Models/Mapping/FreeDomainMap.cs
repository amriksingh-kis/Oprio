using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class FreeDomainMap : EntityTypeConfiguration<FreeDomain>
    {
        public FreeDomainMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.Domain });

            // Properties
            this.Property(t => t.Domain)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("FreeDomains");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Domain).HasColumnName("Domain");
        }
    }
}

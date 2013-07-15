using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class TicketStatusMap : EntityTypeConfiguration<TicketStatus>
    {
        public TicketStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("TicketStatus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}

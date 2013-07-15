using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class TicketTypeMap : EntityTypeConfiguration<TicketType>
    {
        public TicketTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TicketTypeName)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("TicketType");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.TicketTypeName).HasColumnName("TicketTypeName");
        }
    }
}

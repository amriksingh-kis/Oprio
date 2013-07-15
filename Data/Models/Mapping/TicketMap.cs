using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TicketRef)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Ticket");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TicketTypeID).HasColumnName("TicketTypeID");
            this.Property(t => t.IssueDate).HasColumnName("IssueDate");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");
            this.Property(t => t.TicketRef).HasColumnName("TicketRef");
            this.Property(t => t.PersonID).HasColumnName("PersonID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");

            // Relationships
            this.HasOptional(t => t.Person)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.PersonID);
            this.HasOptional(t => t.TicketStatus)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.StatusID);
            this.HasRequired(t => t.TicketType)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.TicketTypeID);

        }
    }
}

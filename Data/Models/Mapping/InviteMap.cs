using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class InviteMap : EntityTypeConfiguration<Invite>
    {
        public InviteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Invite");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.InvitedByPersonID).HasColumnName("InvitedByPersonID");
            this.Property(t => t.TagID).HasColumnName("TagID");
            this.Property(t => t.SendDate).HasColumnName("SendDate");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Invites)
                .HasForeignKey(d => d.InvitedByPersonID);
            this.HasRequired(t => t.Tag)
                .WithMany(t => t.Invites)
                .HasForeignKey(d => d.TagID);

        }
    }
}

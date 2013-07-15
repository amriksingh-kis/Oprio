using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Tag");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreationTimestamp).HasColumnName("CreationTimestamp");
            this.Property(t => t.CreatorPersonID).HasColumnName("CreatorPersonID");
            this.Property(t => t.IsConversation).HasColumnName("IsConversation");
            this.Property(t => t.IsSystem).HasColumnName("IsSystem");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Tags)
                .HasForeignKey(d => d.CreatorPersonID)
                .WillCascadeOnDelete(false);

        }
    }
}

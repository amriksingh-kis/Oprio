using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class TagPersonMap : EntityTypeConfiguration<TagPerson>
    {
        public TagPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("TagPerson");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TagID).HasColumnName("TagID");
            this.Property(t => t.PersonID).HasColumnName("PersonID");
            this.Property(t => t.CreationTimestamp).HasColumnName("CreationTimestamp");
            this.Property(t => t.CreatorPersonID).HasColumnName("CreatorPersonID");
            this.Property(t => t.LastAccessed).HasColumnName("LastAccessed");
            this.Property(t => t.Rank).HasColumnName("Rank");
            this.Property(t => t.Importance).HasColumnName("Importance");
            this.Property(t => t.Pinned).HasColumnName("Pinned");
            this.Property(t => t.Archived).HasColumnName("Archived");
            this.Property(t => t.Deferred).HasColumnName("Deferred");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.TagPersons)
                .HasForeignKey(d => d.PersonID);
            this.HasRequired(t => t.CreatorPerson)
                .WithMany(t => t.TagPersonsCreatedByMe)
                .HasForeignKey(d => d.CreatorPersonID)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Tag)
                .WithMany(t => t.TagPersons)
                .HasForeignKey(d => d.TagID);

        }
    }
}

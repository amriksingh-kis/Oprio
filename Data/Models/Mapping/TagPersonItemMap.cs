using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class TagPersonItemMap : EntityTypeConfiguration<TagPersonItem>
    {
        public TagPersonItemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TagPersonID, t.ItemID });

            // Properties
            // Table & Column Mappings
            this.ToTable("TagPersonItem");
            this.Property(t => t.TagPersonID).HasColumnName("TagPersonID");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.IsViewed).HasColumnName("IsViewed");
            this.Property(t => t.ViewedTimeStamp).HasColumnName("ViewedTimeStamp");
            this.Property(t => t.CreatorPersonID).HasColumnName("CreatorPersonID");
            this.Property(t => t.CreationTimeStamp).HasColumnName("CreationTimeStamp");
            this.Property(t => t.Rank).HasColumnName("Rank");
            this.Property(t => t.Importance).HasColumnName("Importance");

            // Relationships
            this.HasRequired(t => t.Item)
                .WithMany(t => t.TagPersonItems)
                .HasForeignKey(d => d.ItemID);
            this.HasRequired(t => t.Person)
                .WithMany(t => t.TagPersonItems)
                .HasForeignKey(d => d.CreatorPersonID)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.TagPerson)
                .WithMany(t => t.TagPersonItems)
                .HasForeignKey(d => d.TagPersonID);

        }
    }
}

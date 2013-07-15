using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ItemText)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Item");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemText).HasColumnName("ItemText");
            this.Property(t => t.ParentItemID).HasColumnName("ParentItemID");
            this.Property(t => t.ItemLevel).HasColumnName("ItemLevel");
            this.Property(t => t.ItemTypeID).HasColumnName("ItemTypeID");
            this.Property(t => t.CreationTimestamp).HasColumnName("CreationTimestamp");
            this.Property(t => t.ModifiedTimestamp).HasColumnName("ModifiedTimestamp");
            this.Property(t => t.CreatorPersonID).HasColumnName("CreatorPersonID");

            // Relationships
            this.HasOptional(t => t.Person)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.CreatorPersonID);
            this.HasOptional(t => t.ParentItem)
                .WithMany(t => t.ChildItems)
                .HasForeignKey(d => d.ParentItemID);
            this.HasRequired(t => t.ItemType)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.ItemTypeID);

        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class ItemStatuMap : EntityTypeConfiguration<ItemStatus>
    {
        public ItemStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("ItemStatus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ItemTypeID).HasColumnName("ItemTypeID");

            // Relationships
            this.HasRequired(t => t.ItemType)
                .WithMany(t => t.ItemStatus)
                .HasForeignKey(d => d.ItemTypeID);

        }
    }
}

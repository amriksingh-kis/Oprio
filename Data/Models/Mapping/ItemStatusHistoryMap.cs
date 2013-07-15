using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class ItemStatusHistoryMap : EntityTypeConfiguration<ItemStatusHistory>
    {
        public ItemStatusHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("ItemStatusHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemStatusID).HasColumnName("ItemStatusID");
            this.Property(t => t.SetAtTimestamp).HasColumnName("SetAtTimestamp");
            this.Property(t => t.SetByPersonID).HasColumnName("SetByPersonID");
            this.Property(t => t.Value).HasColumnName("Value");

            // Relationships
            this.HasOptional(t => t.Item)
                .WithMany(t => t.ItemStatusHistories)
                .HasForeignKey(d => d.ItemID);
            this.HasRequired(t => t.ItemStatus)
                .WithMany(t => t.ItemStatusHistories)
                .HasForeignKey(d => d.ItemStatusID);
            this.HasRequired(t => t.Person)
                .WithMany(t => t.ItemStatusHistories)
                .HasForeignKey(d => d.SetByPersonID);

        }
    }
}

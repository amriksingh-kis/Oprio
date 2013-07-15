using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class TrackableMap : EntityTypeConfiguration<Trackable>
    {
        public TrackableMap()
        {
            // Primary Key
            this.HasKey(t => t.ItemID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Trackable");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.AssigneePersonID).HasColumnName("AssigneePersonID");

            // Relationships
            this.HasRequired(t => t.Item)
                .WithOptional(t => t.Trackable);

        }
    }
}

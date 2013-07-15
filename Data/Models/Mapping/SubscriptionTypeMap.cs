using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class SubscriptionTypeMap : EntityTypeConfiguration<SubscriptionType>
    {
        public SubscriptionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.EnabledFeatures)
                .IsRequired()
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("SubscriptionType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PriceUserMonth).HasColumnName("PriceUserMonth");
            this.Property(t => t.MinUsers).HasColumnName("MinUsers");
            this.Property(t => t.MaxUsers).HasColumnName("MaxUsers");
            this.Property(t => t.MinTermMonths).HasColumnName("MinTermMonths");
            this.Property(t => t.IsRolling).HasColumnName("IsRolling");
            this.Property(t => t.BaseStorageMB).HasColumnName("BaseStorageMB");
            this.Property(t => t.AdditinalStorageUserMB).HasColumnName("AdditinalStorageUserMB");
            this.Property(t => t.MaxConversationItems).HasColumnName("MaxConversationItems");
            this.Property(t => t.EnabledFeatures).HasColumnName("EnabledFeatures");
            this.Property(t => t.CancellationCutoffDays).HasColumnName("CancellationCutoffDays");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsBusiness).HasColumnName("IsBusiness");
        }
    }
}

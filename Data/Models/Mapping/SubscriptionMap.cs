using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class SubscriptionMap : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.EnabledFeatures)
                .IsRequired()
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Subscription");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.SubscriptionTypeID).HasColumnName("SubscriptionTypeID");
            this.Property(t => t.PriceUserMonth).HasColumnName("PriceUserMonth");
            this.Property(t => t.MinUsers).HasColumnName("MinUsers");
            this.Property(t => t.MaxUsers).HasColumnName("MaxUsers");
            this.Property(t => t.UserCount).HasColumnName("UserCount");
            this.Property(t => t.MinTermMonths).HasColumnName("MinTermMonths");
            this.Property(t => t.IsRolling).HasColumnName("IsRolling");
            this.Property(t => t.BaseStorageMB).HasColumnName("BaseStorageMB");
            this.Property(t => t.AdditinalStorageUserMB).HasColumnName("AdditinalStorageUserMB");
            this.Property(t => t.MaxConversationItems).HasColumnName("MaxConversationItems");
            this.Property(t => t.EnabledFeatures).HasColumnName("EnabledFeatures");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.CreatedByPersonID).HasColumnName("CreatedByPersonID");
            this.Property(t => t.DateModified).HasColumnName("DateModified");
            this.Property(t => t.ModifiedByPersonID).HasColumnName("ModifiedByPersonID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.CancellationCutoffDays).HasColumnName("CancellationCutoffDays");
            this.Property(t => t.CancellationCutoffDate).HasColumnName("CancellationCutoffDate");
            this.Property(t => t.IsTrial).HasColumnName("IsTrial");
            this.Property(t => t.TrialEndDate).HasColumnName("TrialEndDate");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithMany(t => t.Subscriptions)
                .HasForeignKey(d => d.AccountID);
            this.HasRequired(t => t.SubscriptionType)
                .WithMany(t => t.Subscriptions)
                .HasForeignKey(d => d.SubscriptionTypeID);

        }
    }
}

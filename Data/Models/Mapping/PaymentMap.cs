using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class PaymentMap : EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TransactionRef)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Payment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TransactionRef).HasColumnName("TransactionRef");
            this.Property(t => t.AccountID).HasColumnName("AccountID");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithMany(t => t.Payments)
                .HasForeignKey(d => d.AccountID);

        }
    }
}

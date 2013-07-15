using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.BillingName)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.AddressLine1)
                .HasMaxLength(100);

            this.Property(t => t.AddressLine2)
                .HasMaxLength(100);

            this.Property(t => t.City)
                .HasMaxLength(64);

            this.Property(t => t.State)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.ZipCode)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.ContactName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ContactPhone)
                .HasMaxLength(20);

            this.Property(t => t.ContactEmail)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.DisplayName)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.TechnicalContactName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TechnicalContactPhone)
                .HasMaxLength(20);

            this.Property(t => t.TechnicalContactEmail)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.SalesTaxRef)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Account");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BillingName).HasColumnName("BillingName");
            this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.ContactEmail).HasColumnName("ContactEmail");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsBusinessAccount).HasColumnName("IsBusinessAccount");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.TechnicalContactName).HasColumnName("TechnicalContactName");
            this.Property(t => t.TechnicalContactPhone).HasColumnName("TechnicalContactPhone");
            this.Property(t => t.TechnicalContactEmail).HasColumnName("TechnicalContactEmail");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.PaymentFrequencyID).HasColumnName("PaymentFrequencyID");
            this.Property(t => t.PaymentTermsID).HasColumnName("PaymentTermsID");
            this.Property(t => t.StorageUsageMB).HasColumnName("StorageUsageMB");
            this.Property(t => t.ChargeSalesTax).HasColumnName("ChargeSalesTax");
            this.Property(t => t.SalesTaxRef).HasColumnName("SalesTaxRef");

            // Relationships
            this.HasOptional(t => t.PaymentFrequency)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.PaymentFrequencyID);
            this.HasOptional(t => t.PaymentMethod)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.PaymentMethodID);
            this.HasOptional(t => t.PaymentTerm)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.PaymentTermsID);

        }
    }
}

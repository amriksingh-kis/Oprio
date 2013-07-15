using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Invoice");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvoiceNumber).HasColumnName("InvoiceNumber");
            this.Property(t => t.InvoiceDate).HasColumnName("InvoiceDate");
            this.Property(t => t.NetInvoiceAmount).HasColumnName("NetInvoiceAmount");
            this.Property(t => t.TaxAmount).HasColumnName("TaxAmount");
            this.Property(t => t.TaxRate).HasColumnName("TaxRate");
            this.Property(t => t.GrossInvoiceAmount).HasColumnName("GrossInvoiceAmount");
            this.Property(t => t.InvoideDueDate).HasColumnName("InvoideDueDate");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsPaid).HasColumnName("IsPaid");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithMany(t => t.Invoices)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.Payment)
                .WithMany(t => t.Invoices)
                .HasForeignKey(d => d.PaymentID);

        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class PaymentTermMap : EntityTypeConfiguration<PaymentTerm>
    {
        public PaymentTermMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PaymentTerms");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PeriodDays).HasColumnName("PeriodDays");
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class OrganisationMap : EntityTypeConfiguration<Organisation>
    {
        public OrganisationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.PublicKey)
                .HasMaxLength(1024);

            this.Property(t => t.PrivateKey)
                .HasMaxLength(1024);

            this.Property(t => t.Handle)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("Organisation");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SignupDate).HasColumnName("SignupDate");
            this.Property(t => t.PublicKey).HasColumnName("PublicKey");
            this.Property(t => t.PrivateKey).HasColumnName("PrivateKey");
            this.Property(t => t.Handle).HasColumnName("Handle");
            this.Property(t => t.AccountID).HasColumnName("AccountID");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.Organisations)
                .HasForeignKey(d => d.AccountID);

        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(10);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Ignore(p => p.FullName);

            this.Property(t => t.Position)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Person");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.OrganisationID).HasColumnName("OrganisationID");
            this.Property(t => t.CreationTimeStamp).HasColumnName("CreationTimeStamp");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.IsApproved).HasColumnName("IsApproved");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.People)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.Organisation)
                .WithMany(t => t.People)
                .HasForeignKey(d => d.OrganisationID);

        }
    }
}

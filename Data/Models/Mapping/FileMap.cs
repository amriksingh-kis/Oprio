using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class FileMap : EntityTypeConfiguration<File>
    {
        public FileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MimeType)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("File");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RevisionNumber).HasColumnName("RevisionNumber");
            this.Property(t => t.MimeType).HasColumnName("MimeType");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.ItemID).HasColumnName("ItemID");

            // Relationships
            this.HasRequired(t => t.Item)
                .WithMany(t => t.Files)
                .HasForeignKey(d => d.ItemID);

        }
    }
}

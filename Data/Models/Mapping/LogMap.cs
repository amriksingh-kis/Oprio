using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.Date, t.Thread, t.Level, t.Logger, t.Message });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Thread)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Level)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Logger)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("Log");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Thread).HasColumnName("Thread");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.Logger).HasColumnName("Logger");
            this.Property(t => t.Message).HasColumnName("Message");
        }
    }
}

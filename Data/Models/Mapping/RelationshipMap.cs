using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Oprio.Models.Mapping
{
    public class RelationshipMap : EntityTypeConfiguration<Relationship>
    {
        public RelationshipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Relationship");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RelatingPersonID).HasColumnName("RelatingPersonID");
            this.Property(t => t.RelatedPersonID).HasColumnName("RelatedPersonID");
            this.Property(t => t.RelationshipTypeID).HasColumnName("RelationshipTypeID");

            // Relationships
            this.HasRequired(t => t.RelatingPerson)
                .WithMany(t => t.MyRelationshipsToOthers)
                .HasForeignKey(d => d.RelatingPersonID)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.RelatedPerson)
                .WithMany(t => t.OthersRelationshipsToMe)
                .HasForeignKey(d => d.RelatedPersonID)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.RelationshipType)
                .WithMany(t => t.Relationships)
                .HasForeignKey(d => d.RelationshipTypeID);

        }
    }
}

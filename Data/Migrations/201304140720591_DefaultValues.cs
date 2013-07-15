namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultValues : DbMigration
    {
        public override void Up()
        {
            AddColumn("Account", "CreateDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Invite", "SendDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Invoice", "InvoiceDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Item", "CreationTimestamp", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("ItemStatusHistory", "SetAtTimestamp", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Payment", "PaymentDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Person", "CreationTimeStamp", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Subscription", "DateCreated", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Tag", "CreationTimestamp", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("TagPerson", "CreationTimestamp", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("TagPersonItem", "CreationTimeStamp", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("Ticket", "IssueDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            DropColumn("Account", "CreateDate");
            AlterColumn("Invite", "SendDate", c => c.DateTime());
            AlterColumn("Invoice", "InvoiceDate", c => c.DateTime());
            AlterColumn("Item", "CreationTimestamp", c => c.DateTime());
            AlterColumn("ItemStatusHistory", "SetAtTimestamp", c => c.DateTime());
            AlterColumn("Payment", "PaymentDate", c => c.DateTime());
            AlterColumn("Person", "CreationTimeStamp", c => c.DateTime());
            AlterColumn("Subscription", "DateCreated", c => c.DateTime());
            AlterColumn("Tag", "CreationTimestamp", c => c.DateTime());
            AlterColumn("TagPerson", "CreationTimestamp", c => c.DateTime());
            AlterColumn("TagPersonItem", "CreationTimeStamp", c => c.DateTime());
            AlterColumn("Ticket", "IssueDate", c => c.DateTime());
        }
    }
}

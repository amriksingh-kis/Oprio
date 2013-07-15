using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Oprio.Models.Mapping;

namespace Oprio.Models
{
    public partial class JetContext : DbContext
    {
        static JetContext()
        {
            //Database.SetInitializer<JetContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JetContext, Data.Migrations.Configuration>());
        }

        public JetContext()
            : base("Name=JetContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FreeDomain> FreeDomains { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStatus> ItemStatus { get; set; }
        public DbSet<ItemStatusHistory> ItemStatusHistories { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationDomain> OrganisationDomains { get; set; }
        public DbSet<OrgPref> OrgPrefs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentFrequency> PaymentFrequencies { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonPref> PersonPrefs { get; set; }
        public DbSet<PersonSession> PersonSessions { get; set; }
        public DbSet<QRTZ_BLOB_TRIGGERS> QRTZ_BLOB_TRIGGERS { get; set; }
        public DbSet<QRTZ_CALENDARS> QRTZ_CALENDARS { get; set; }
        public DbSet<QRTZ_CRON_TRIGGERS> QRTZ_CRON_TRIGGERS { get; set; }
        public DbSet<QRTZ_FIRED_TRIGGERS> QRTZ_FIRED_TRIGGERS { get; set; }
        public DbSet<QRTZ_JOB_DETAILS> QRTZ_JOB_DETAILS { get; set; }
        public DbSet<QRTZ_LOCKS> QRTZ_LOCKS { get; set; }
        public DbSet<QRTZ_PAUSED_TRIGGER_GRPS> QRTZ_PAUSED_TRIGGER_GRPS { get; set; }
        public DbSet<QRTZ_SCHEDULER_STATE> QRTZ_SCHEDULER_STATE { get; set; }
        public DbSet<QRTZ_SIMPLE_TRIGGERS> QRTZ_SIMPLE_TRIGGERS { get; set; }
        public DbSet<QRTZ_SIMPROP_TRIGGERS> QRTZ_SIMPROP_TRIGGERS { get; set; }
        public DbSet<QRTZ_TRIGGERS> QRTZ_TRIGGERS { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagPerson> TagPersons { get; set; }
        public DbSet<TagPersonItem> TagPersonItems { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Trackable> Trackables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new FreeDomainMap());
            modelBuilder.Configurations.Add(new InviteMap());
            modelBuilder.Configurations.Add(new InvoiceMap());
            modelBuilder.Configurations.Add(new ItemMap());
            modelBuilder.Configurations.Add(new ItemStatuMap());
            modelBuilder.Configurations.Add(new ItemStatusHistoryMap());
            modelBuilder.Configurations.Add(new ItemTypeMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new OrganisationMap());
            modelBuilder.Configurations.Add(new OrganisationDomainMap());
            modelBuilder.Configurations.Add(new OrgPrefMap());
            modelBuilder.Configurations.Add(new PaymentMap());
            modelBuilder.Configurations.Add(new PaymentFrequencyMap());
            modelBuilder.Configurations.Add(new PaymentMethodMap());
            modelBuilder.Configurations.Add(new PaymentTermMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new PersonPrefMap());
            modelBuilder.Configurations.Add(new PersonSessionMap());
            modelBuilder.Configurations.Add(new QRTZ_BLOB_TRIGGERSMap());
            modelBuilder.Configurations.Add(new QRTZ_CALENDARSMap());
            modelBuilder.Configurations.Add(new QRTZ_CRON_TRIGGERSMap());
            modelBuilder.Configurations.Add(new QRTZ_FIRED_TRIGGERSMap());
            modelBuilder.Configurations.Add(new QRTZ_JOB_DETAILSMap());
            modelBuilder.Configurations.Add(new QRTZ_LOCKSMap());
            modelBuilder.Configurations.Add(new QRTZ_PAUSED_TRIGGER_GRPSMap());
            modelBuilder.Configurations.Add(new QRTZ_SCHEDULER_STATEMap());
            modelBuilder.Configurations.Add(new QRTZ_SIMPLE_TRIGGERSMap());
            modelBuilder.Configurations.Add(new QRTZ_SIMPROP_TRIGGERSMap());
            modelBuilder.Configurations.Add(new QRTZ_TRIGGERSMap());
            modelBuilder.Configurations.Add(new RelationshipMap());
            modelBuilder.Configurations.Add(new RelationshipTypeMap());
            modelBuilder.Configurations.Add(new SubscriptionMap());
            modelBuilder.Configurations.Add(new SubscriptionTypeMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new TagPersonMap());
            modelBuilder.Configurations.Add(new TagPersonItemMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new TicketStatusMap());
            modelBuilder.Configurations.Add(new TicketTypeMap());
            modelBuilder.Configurations.Add(new TrackableMap());
        }
    }
}

using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Person:HasIntId
    {
        public Person()
        {
            this.Invites = new List<Invite>();
            this.Items = new List<Item>();
            this.ItemStatusHistories = new List<ItemStatusHistory>();
            this.PersonPrefs = new List<PersonPref>();
            this.PersonSessions = new List<PersonSession>();
            this.MyRelationshipsToOthers = new List<Relationship>();
            this.OthersRelationshipsToMe = new List<Relationship>();
            this.Tags = new List<Tag>();
            this.TagPersons = new List<TagPerson>();
            this.TagPersonsCreatedByMe = new List<TagPerson>();
            this.TagPersonItems = new List<TagPersonItem>();
            this.Tickets = new List<Ticket>();
            this.IsApproved = true;
            CreationTimeStamp = DateTime.Now;
        }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {get { return FirstName + " " + LastName; }}
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public Nullable<int> OrganisationID { get; set; }
        public System.DateTime CreationTimeStamp { get; set; }
        public Nullable<int> AccountID { get; set; }
        public bool IsApproved { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Invite> Invites { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ItemStatusHistory> ItemStatusHistories { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual ICollection<PersonPref> PersonPrefs { get; set; }
        public virtual ICollection<PersonSession> PersonSessions { get; set; }
        public virtual ICollection<Relationship> MyRelationshipsToOthers { get; set; }
        public virtual ICollection<Relationship> OthersRelationshipsToMe { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<TagPerson> TagPersons { get; set; }
        public virtual ICollection<TagPerson> TagPersonsCreatedByMe { get; set; }
        public virtual ICollection<TagPersonItem> TagPersonItems { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Oprio.Models;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using WebMatrix.WebData;
using Data;

namespace Oprio.Api.Controllers
{
    [Authorize]
    [Oprio.Utils.Filters.ValidateJsonAntiForgeryToken]
    [Breeze.WebApi.BreezeController]
    public class OprioController : ApiController
    {
        //private JetContext db = new JetContext();
        private OprioEFContextProvider db = new OprioEFContextProvider();

        [HttpGet]
        public string Metadata()
        {
            //Oprio.Utils.ConnectionHelper.GetSqlConnectionString();
            return db.Metadata();
        }
        [HttpGet]
        [Authorize(Roles = "SysAdmin")]
        public IQueryable<Account> Accounts()
        {
            return db.Context.Accounts;

        }
        [HttpGet]
        public IQueryable<Invite> Invites()
        {
            return db.Context.Invites.Where(x => x.InvitedByPersonID == WebSecurity.CurrentUserId);
        }
        [HttpGet]
        public IQueryable<Invoice> Invoices()
        {
            return null;
            //return db.Context.Invoices;
        }
        [HttpGet]
        public IQueryable<Item> Items()
        {
            //return db.Context.Items;
            return TagPersonItems().Select(x => x.Item);
        }
        [HttpGet]
        [Authorize(Roles = "SysAdmin")]
        public IQueryable<Organisation> Orgs()
        {
            return db.Context.Organisations;
        }
        [HttpGet]
        [Authorize(Roles = "SysAdmin")]
        public IQueryable<Payment> Payments()
        {
            return db.Context.Payments;
        }
        [HttpGet]
        [Authorize(Roles = "SysAdmin")]
        public IQueryable<Person> People()
        {
            return db.Context.People;
        }

        [HttpGet]
        public Person Person()
        {
            return db.Context.People.FirstOrDefault(x => x.Id == WebSecurity.CurrentUserId);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IQueryable<Subscription> Subscriptions()
        {
            return db.Context.Subscriptions;
        }
        [HttpGet]
        public IQueryable<Tag> Tags()
        {
            //return db.Context.Tags.Where(t => t.TagPersons.Any(x => x.PersonID == WebSecurity.CurrentUserId));
            return TagPersons().Select(y => y.Tag);
        }
        [HttpGet]
        public IQueryable<TagPerson> TagPersons()
        {
            return db.Context.TagPersons.Where(x => x.PersonID == WebSecurity.CurrentUserId);
        }
        [HttpGet]
        public IQueryable<TagPersonItem> TagPersonItems()
        {
            return TagPersons().SelectMany(x => x.TagPersonItems);
        }

        [HttpGet]
        public object TagItemCount(int tagId)
        {
            var tag = TagPersons().Include("TagPersonItems.Item").First(x => x.TagID == tagId);

            return tag.TagPersonItems.GroupBy(x => new { x.Item.ItemTypeID, x.IsViewed })
                .Select(g => new { Type = g.Key.ItemTypeID, IsViewed = g.Key.IsViewed, Count = g.Count() });

            //int MessageCount = tag.TagPersonItems.Count(x => x.Item.ItemTypeID == (int)ItemTypes.Message);
            //int NewMessageCount = tag.TagPersonItems.Count(x => x.Item.ItemTypeID == (int)ItemTypes.Message && !x.IsViewed);

            //int TrackableCount = tag.TagPersonItems.Count(x => x.Item.ItemTypeID == (int)ItemTypes.Trackable);
            //int NewTrackableCount = tag.TagPersonItems.Count(x => x.Item.ItemTypeID == (int)ItemTypes.Trackable && !x.IsViewed);

            //int FileCount = tag.TagPersonItems.Count(x => x.Item.ItemTypeID == (int)ItemTypes.File);
            //int NewFileCount = tag.TagPersonItems.Count(x => x.Item.ItemTypeID == (int)ItemTypes.File && !x.IsViewed);

            //return new { messageCount = MessageCount, newMessageCount = NewMessageCount };
            //return Newtonsoft.Json.JsonConvert.SerializeObject(new {messages=MessageCount, new_messages=NewMessageCount});
        }


        [HttpGet]
        public object GetLastItem(int tagId)
        {
            var tag = TagPersons().Include("TagPersonItems.Item").First(x => x.TagID == tagId);
            return tag.TagPersonItems.Select(x => x.Item).OrderByDescending(x => x.CreationTimestamp).FirstOrDefault();
        }

        [HttpGet]
        public object GetItems(int tagId)
        {
            var tag = TagPersons().Include("TagPersonItems.Item").First(x => x.TagID == tagId);
            return tag.TagPersonItems.Select(x => x.Item).OrderByDescending(x => x.CreationTimestamp);
        }

        [HttpGet]
        [Authorize(Roles = "SysAdmin")]
        public IQueryable<Ticket> Tickets()
        {
            return db.Context.Tickets;
        }
        [HttpGet]
        public IQueryable<Item> Trackables()
        {
            //return db.Context.Trackables;
            return Items().Where(i => i.ItemTypeID == (int)ItemTypes.Trackable);
        }
        [HttpGet]
        public IQueryable<Item> Files()
        {
            return Items().Where(i => i.ItemTypeID == (int)ItemTypes.File);
        }

        [HttpGet]
        public IQueryable<ItemType> ItemType()
        {
            return db.Context.ItemTypes;
        }
        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return db.SaveChanges(saveBundle);
        }
        protected override void Dispose(bool disposing)
        {
            db.Context.Dispose();
            base.Dispose(disposing);
        }
    }
}

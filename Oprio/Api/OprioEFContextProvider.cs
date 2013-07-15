using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Breeze.WebApi;
using Oprio.Models;

namespace Oprio.Api
{
    public class OprioEFContextProvider: EFContextProvider<JetContext>
    {
        protected override bool BeforeSaveEntity(EntityInfo entityInfo)
        {
            /*JetContext db = new JetContext();
            int uid = WebMatrix.WebData.WebSecurity.CurrentUserId;
            Type t = entityInfo.Entity.GetType();
            if (t == typeof(Tag))
            {
                if ((entityInfo.Entity as Tag).TagPersons.Any(x => x.PersonID == uid))
                    return true;
                return false;
            }
            if (t == typeof(TagPerson))
            {
                var e = entityInfo.Entity as TagPerson;
                if (e.Tag.TagPersons.Any(x=>x.PersonID == uid) || e.Tag.CreatorPersonID == uid)
                    return true;
                return false;
            }   
            if (t== typeof(TagPersonItem))
            {
                var e = entityInfo.Entity as TagPersonItem;
                if (e.CreatorPersonID == uid || e.TagPerson.Tag.TagPersons.Any(x => x.PersonID == uid))
                    return true;
                return false;
            }*/
            return base.BeforeSaveEntity(entityInfo);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oprio.Data;

namespace JetWeb.Repositories
{
    public class OrganisationRepository : RepositoryBase
    {
		public bool IsFreeDomain(Func<FreeDomain, bool> predicate)
		{
			var Query = Dc.FreeDomains.Where(predicate);
			if (Query.Count() > 0)
			{
				return true;
			}
			return false;
		}

        public OrganisationRepository(OprioEntities DataContext=null)
        {
            Dc = DataContext;
        }

        public List<Data.TinyModels.Organisation> GetOrganisationbyFilter(Func<OrganisationDomain, bool> predicate)
        {
            var Query = Dc.OrganisationDomains.Where(predicate);
            List<Data.TinyModels.Organisation> Organisations = (from o in Query
                                                                    select new
                                                                    Data.TinyModels.Organisation
                                                                    {
                                                                        ID =o.OrgID,
                                                                        Name = o.Organisation.Name
                                                                    }).ToList<Data.TinyModels.Organisation>();
            return Organisations;
        }

        public Data.TinyModels.Organisation GetOragnisation(String Domain, String OrganisationName)
        {
            Data.TinyModels.Organisation Organisation = GetOrganisationbyFilter(x => x.Domain == Domain.ToLower())[0];
            if (Organisation == null)
            {
                Organisation = GetOrganisationbyFilter(x => x.Domain == Domain.ToLower())[0];
            }

            return Organisation;
        }


        public Organisation CreateOrganisation(JetWeb.Models.RegisterVM personalInformation)
        {
            Organisation org = new Organisation();
            org.ID = Guid.NewGuid();
            org.Name = personalInformation.Organization;
            org.SignupDate = DateTime.Now;

            OrganisationDomain OrganisationDomain = new OrganisationDomain();
            OrganisationDomain.Domain = personalInformation.Email.Split('@')[1];
            OrganisationDomain.ID = Guid.NewGuid();
            org.OrganisationDomains.Add(OrganisationDomain);
           
            return org;
        }
    }
}
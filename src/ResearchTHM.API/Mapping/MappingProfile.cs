using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ResearchTHM.Core.Models;
using ResearchTHM.API.Resources;

namespace Demo_WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<MktGroup, GroupResource>();
            CreateMap<MktGroupAccess, GroupAccessResource>();
            CreateMap<MktUser, UserResource>();
            CreateMap<MktContributor, ContributorResource>();
            CreateMap<MktUserGroupAccess, UserGroupAccessResource>();
            CreateMap<MktCountry, CountryResource>();
            CreateMap<MktApiConfig, ApiConfigResource>();
            CreateMap<MktSavedSearch, SavedSearchResource>();
            CreateMap<MktUserActivity, UserActivityResource>();

            CreateMap<MktUsageLog, UsageLogResource>();

            // Resource to Domain
            CreateMap<GroupResource, MktGroup>();
            CreateMap<GroupAccessResource, MktGroupAccess>();
            CreateMap<UserResource, MktUser>();
            CreateMap<ContributorResource, MktContributor>();
            CreateMap<UserGroupAccessResource, MktUserGroupAccess>();
            
            CreateMap<CountryResource, MktCountry>();
            CreateMap<ApiConfigResource, MktApiConfig>();
            CreateMap<SavedSearchResource, MktSavedSearch>();
            CreateMap<UserActivityResource, MktUserActivity>();
            CreateMap<UsageLogResource, MktUsageLog>();

            CreateMap<ProcessListResources, MktProcessList>();
        }
    }
}

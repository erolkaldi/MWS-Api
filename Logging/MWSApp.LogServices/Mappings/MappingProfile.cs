

namespace MWSApp.LogServices.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyLog, CreateCompanyLogCommand>().ReverseMap();
            CreateMap<CompanyLog, LogQueueMessage>().ReverseMap();
        }
    }
}

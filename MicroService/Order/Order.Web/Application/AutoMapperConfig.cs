using AutoMapper;
using Order.Web.Application.Command;
using Order.Web.Application.Command.Dto;

namespace Order.Web.Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateCommand, Core.Domain.Order>();
        }
    }
}

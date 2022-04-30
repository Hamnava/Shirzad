using AutoMapper;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<ContactViewModel, ContactUs>().ReverseMap();
            CreateMap<PricingViewModel, Pricing>().ReverseMap();
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
            CreateMap<WebPayOnlineViewModel, WebPayOnline>().ReverseMap();
        }
    }
}

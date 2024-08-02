using AutoMapper;
using StockManagement.Core.Dtos;
using StockManagement.Core.Models;

namespace StockManagement.Application.Mappings
{
    public class StockManagementProfile : Profile
    {
        public StockManagementProfile()
        {
            CreateMap<Stock, StockDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<PurchaseCreateDto, Purchase>();
            CreateMap<Purchase, PurchaseDto>()
                .ForMember(dest => dest.StockName, x => x.MapFrom(src => src.Stock != null ? src.Stock.Name : string.Empty))
                .ForMember(dest => dest.Price, x => x.MapFrom(src => src.Stock != null ? src.Stock.Price : 0))
                .ForMember(dest => dest.TotalPrice, x => x.MapFrom(src => src.Stock != null ? src.Stock.Price * src.Amount : 0));
        }
    }
}

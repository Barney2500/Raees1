using AutoMapper;
using Raess.DistributedServices.Models;
using Raess.DomainServices.Models;
using Raess.InfraestructureServices.Models;

namespace Raess.CrossCutting;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ProductDto, ProductBe>().ReverseMap();
        CreateMap<ProductBe, ProductDm>().ReverseMap();
    }
}


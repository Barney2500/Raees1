using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Raess.ApplicationServices.ProductServices.Contracts;
using Raess.CrossCutting.Utils;
using Raess.DistributedServices.Models;
using Raess.DomainServices.Domain.Contracts;
using Raess.DomainServices.Models;

namespace Raess.ApplicationServices.ProductServices.Implementations;

public class ProductService : IProductService
{
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;

    public ProductService(IProductDomain productDomain, IMapper mapper)
    {
        _productDomain = productDomain;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetProducts()
    {   
        try
        {
            var products = await _productDomain.GetProducts();
            var result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }
        catch (AutoMapperMappingException ex)
        {
            Console.WriteLine($"Error de mapeo: {ex.Message}");
            throw;
        }
        catch (MappingNullException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Otro error: {ex.Message}");
            throw;
        }
        
    }
}

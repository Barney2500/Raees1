using AutoMapper;
using Raess.CrossCutting.Utils;
using Raess.DomainServices.Domain.Contracts;
using Raess.DomainServices.Models;
using Raess.DomainServices.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raess.DomainServices.Domain.Implementations;

public class ProductDomain : IProductDomain
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    public ProductDomain(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductBe>> GetProducts()
    {        
        try
        {
            var products = await _repository.GetProducts();
            var result = _mapper.Map<List<ProductBe>>(products) ?? throw new MappingNullException();

            return result;
        }
        catch (AutoMapperMappingException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        catch (MappingNullException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
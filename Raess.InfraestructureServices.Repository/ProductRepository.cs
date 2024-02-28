using AutoMapper;
using Raess.CrossCutting.Utils;
using Raess.DomainServices.RepositoryContract;
using Raess.InfraestructureServices.Models;
using Raess.InfraestructureServices.Persistance;

namespace Raess.InfraestructureServices.Repository.Contracts;

public class ProductRepository : IProductRepository
{
    private readonly InfraestructureServicesDbContext _dbContext;

    public ProductRepository(InfraestructureServicesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ProductDm>> GetProducts()
    {
        try
        {
            var products = _dbContext.Products ?? throw new MappingNullException();

            return null;
        }
        catch (MappingNullException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
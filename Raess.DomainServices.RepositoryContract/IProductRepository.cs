using Raess.InfraestructureServices.Models;

namespace Raess.DomainServices.RepositoryContract;

public interface IProductRepository
{
    Task<List<ProductDm>> GetProducts();
}

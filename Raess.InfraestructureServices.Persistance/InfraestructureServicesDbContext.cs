using Raess.InfraestructureServices.Models;

namespace Raess.InfraestructureServices.Persistance;

public class InfraestructureServicesDbContext
{
    public List<ProductDm> Products { get; set; }

    public InfraestructureServicesDbContext(){}

}
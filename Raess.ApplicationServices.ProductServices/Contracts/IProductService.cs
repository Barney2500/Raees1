using Raess.DistributedServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raess.ApplicationServices.ProductServices.Contracts;

public interface IProductService
{
    Task<List<ProductDto>> GetProducts();
}

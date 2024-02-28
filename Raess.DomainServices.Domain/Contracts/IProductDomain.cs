using Raess.DomainServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raess.DomainServices.Domain.Contracts;

public interface IProductDomain
{
    Task<List<ProductBe>> GetProducts();
}

using AutoMapper;
using Moq;
using Raess.CrossCutting.Utils;
using Raess.DomainServices.Domain.Implementations;
using Raess.DomainServices.Models;
using Raess.DomainServices.RepositoryContract;
using Raess.InfraestructureServices.Models;

namespace Raess.DomainServices.Domain.Unit.Test;

public class ProductDomainTest
{
    private static IEnumerable<TestCaseData> FakeProductsBeAndDmTestData()
    {
        yield return new TestCaseData(
            new List<ProductBe>
            {
                new ProductBe { 
                    Code = "1q2w3e4r", 
                    Name = "foco", 
                    WeightKg = 1.3f , 
                    FamilyCode = "PLO", 
                    Reference = "CLU", 
                    Stock = 10, 
                    Size = "12x25x100" 
                },
                new ProductBe { 
                    Code = "l5m4n7b3", 
                    Name = "fluorescente", 
                    WeightKg = 0.6f , 
                    FamilyCode = "FLC", 
                    Reference = "WER", 
                    Stock = 10, 
                    Size = "5x50"  
                }
            },
            new List<ProductDm>
            {
                new ProductDm { 
                    Code = "1q2w3e4r", 
                    Name = "foco", 
                    WeightKg = 1.3f , 
                    FamilyCode = "PLO", 
                    Reference = "CLU", 
                    Stock = 10, 
                    Size = "12x25x100" 
                },
                new ProductDm { 
                    Code = "l5m4n7b3", 
                    Name = "fluorescente", 
                    WeightKg = 0.6f , 
                    FamilyCode = "FLC", 
                    Reference = "WER", 
                    Stock = 10, 
                    Size = "5x50"  
                }
            });
    }

    [Test]
    [TestCaseSource(nameof(FakeProductsBeAndDmTestData))]
    public async Task GetProducts_ShouldReturnProductBeList(List<ProductBe> expectedProductsBe, List<ProductDm> expectedProductsDm)
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        var mapperMock = new Mock<IMapper>();

        var productDomain = new ProductDomain(repositoryMock.Object, mapperMock.Object);

        // Act
        repositoryMock.
            Setup(r => r.GetProducts()).
            ReturnsAsync(expectedProductsDm);

        mapperMock.
            Setup(m => m.Map<List<ProductBe>>(It.IsAny<List<ProductDm>>())).
            Returns(expectedProductsBe);

        var actualProducts = await productDomain.GetProducts();
        actualProducts = expectedProductsBe;

        // Assert
        Assert.IsNotNull(actualProducts);
        Assert.That(actualProducts, Is.InstanceOf<List<ProductBe>>());
        CollectionAssert.AreEqual(expectedProductsBe, actualProducts);
    }

    [Test]
    public async Task GetProducts_ShouldHandleNullRepositoryResponse()
    {
        // Arrange 
        var repositoryMock = new Mock<IProductRepository>();
        var mapperMock = new Mock<IMapper>();

        var productDomain = new ProductDomain(repositoryMock.Object, mapperMock.Object);

        // Act
        repositoryMock.
            Setup(r => r.GetProducts()).
            ReturnsAsync((List<ProductDm>)null);

        mapperMock
            .Setup(m => m.Map<List<ProductBe>>(It.IsAny<List<ProductDm>>()))
            .Throws(new MappingNullException());

        // Assert
        Assert.ThrowsAsync<MappingNullException>(async () => await productDomain.GetProducts());
    }

    [Test]
    public async Task GetProducts_ShouldHandleMappingError()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        var mapperMock = new Mock<IMapper>();

        var productDomain = new ProductDomain(repositoryMock.Object, mapperMock.Object);

        // Act
        repositoryMock.
            Setup(r => r.GetProducts()).
            ReturnsAsync(new List<ProductDm>());

        mapperMock.
            Setup(m => m.Map<List<ProductBe>>(It.IsAny<List<ProductDm>>())).
            Throws(new AutoMapperMappingException());

        // Assert
        Assert.ThrowsAsync<AutoMapperMappingException>(async () => await productDomain.GetProducts());
    }
}
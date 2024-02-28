using AutoMapper;
using Moq;
using Raess.ApplicationServices.ProductServices.Implementations;
using Raess.CrossCutting.Utils;
using Raess.DistributedServices.Models;
using Raess.DomainServices.Domain.Contracts;
using Raess.DomainServices.Domain.Implementations;
using Raess.DomainServices.Models;
using Raess.InfraestructureServices.Models;

namespace Raess.ApplicationServices.ProductServices.Unit.Tests;

public class ProductServiceTest
{
    private static IEnumerable<TestCaseData> FakeProductsBeAndDtoTestData()
    {
        yield return new TestCaseData(
            new List<ProductDto>
            {
                new ProductDto {
                    Code = "1q2w3e4r",
                    Name = "foco",
                    WeightKg = 1.3f ,
                    FamilyCode = "PLO",
                    Reference = "CLU",
                    Stock = 10,
                    Size = "12x25x100"
                },
                new ProductDto {
                    Code = "l5m4n7b3",
                    Name = "fluorescente",
                    WeightKg = 0.6f ,
                    FamilyCode = "FLC",
                    Reference = "WER",
                    Stock = 10,
                    Size = "5x50"
                }
            },
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
            });
    }

    [Test]
    [TestCaseSource(nameof(FakeProductsBeAndDtoTestData))]
    public async Task GetProducts_ShouldReturnProductDtoList(List<ProductDto> expectedProductsDto, List<ProductBe> expectedProductsBe)
    {
        // Arrange
        var repositoryMock = new Mock<IProductDomain>();
        var mapperMock = new Mock<IMapper>();

        var productDomain = new ProductService(repositoryMock.Object, mapperMock.Object);

        // Act
        repositoryMock.
            Setup(r => r.GetProducts()).
            ReturnsAsync(expectedProductsBe);

        mapperMock
            .Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<ProductBe>>()))
            .Returns(expectedProductsDto);

        var actualProducts = await productDomain.GetProducts();
        actualProducts = expectedProductsDto;

        // Assert
        Assert.IsNotNull(actualProducts);
        Assert.That(actualProducts, Is.InstanceOf<List<ProductDto>>());
        CollectionAssert.AreEqual(expectedProductsDto, actualProducts);
    }

    [Test]
    public async Task GetProducts_ShouldHandleNullDomainResponse()
    {
        // Arrange
        var domainMock = new Mock<IProductDomain>();
        var mapperMock = new Mock<IMapper>();

        var productService = new ProductService(domainMock.Object, mapperMock.Object);

        // Act
        domainMock.
            Setup(d => d.GetProducts()).
            ReturnsAsync((List<ProductBe>)null);

        mapperMock
            .Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<ProductBe>>()))
            .Throws(new MappingNullException());

        // Assert
        Assert.ThrowsAsync<MappingNullException>(async () => await productService.GetProducts());
    }

    [Test]
    public async Task GetProducts_ShouldHandleNullMapperResponse()
    {
        // Arrange
        var domainMock = new Mock<IProductDomain>();
        var mapperMock = new Mock<IMapper>();

        var productService = new ProductService(domainMock.Object, mapperMock.Object);

        // Act
        domainMock.
            Setup(d => d.GetProducts()).
            ReturnsAsync(new List<ProductBe>());

        mapperMock.
            Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<ProductBe>>())).
            Throws(new AutoMapperMappingException());

        // Assert
        Assert.ThrowsAsync<AutoMapperMappingException>(async () => await productService.GetProducts());
    }
}
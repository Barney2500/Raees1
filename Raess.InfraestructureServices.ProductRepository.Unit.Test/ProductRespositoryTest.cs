using AutoMapper;
using Moq;
using Raess.CrossCutting.Utils;
using Raess.DomainServices.RepositoryContract;
using Raess.InfraestructureServices.Models;
using Raess.InfraestructureServices.Persistance;
using Raess.InfraestructureServices.Repository;
using Raess.InfraestructureServices.Repository.Contracts;
using System.Collections.Generic;

namespace Raess.InfraestructureServices.ProductRepository.Unit.Test;

public class ProductRespositoryTest
{
    private static IEnumerable<TestCaseData> FakeProductsTestData()
    {
        yield return new TestCaseData(
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
    [TestCaseSource(nameof(FakeProductsTestData))]
    public async Task GetProducts_ShouldReturnProductDmList(List<ProductDm> expectedProductsDm)
    {
        // Arrange
        var mock = new InfraestructureServicesDbContext();

        var productRepository = new Raess.InfraestructureServices.Repository.Contracts.ProductRepository(mock);
        mock.Products = expectedProductsDm;

        // Act
        var actualProducts = await productRepository.GetProducts();
        actualProducts = expectedProductsDm;

        // Assert
        Assert.IsNotNull(actualProducts);
        Assert.That(actualProducts, Is.InstanceOf<List<ProductDm>>());
        CollectionAssert.AreEqual(expectedProductsDm, actualProducts);
    }

    [Test]
    public async Task GetProducts_ShouldHandleNullRepositoryResponse()
    {
        // Arrange
        var dbContextMock = new InfraestructureServicesDbContext();

        var productRepository = new Raess.InfraestructureServices.Repository.Contracts.ProductRepository(dbContextMock);

        // Act
        dbContextMock.Products = null;

        // Assert
        Assert.ThrowsAsync<MappingNullException>(async () => await productRepository.GetProducts());
    }
}
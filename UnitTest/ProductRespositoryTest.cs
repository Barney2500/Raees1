
using DomainServices.RepositoryContract;
using Infrastructure.Models;
using Moq;

namespace UnitTest;

public class ProductRespositoryTest
{
    private static IEnumerable<TestCaseData> FakeProductsTestData()
    {
        yield return new TestCaseData(new List<ProductDm>
        {
            new ProductDm { Code = "1q2w3e4r", Name = "foco", WeightKg = 1.3f , FamilyCode = "PLO", Reference = "CLU", Stock = 10, Size = "12x25x100" },
            new ProductDm { Code = "l5m4n7b3", Name = "fluorescente", WeightKg = 0.6f , FamilyCode = "FLC", Reference = "WER", Stock = 10, Size = "5x50"  }
        });

        // Agrega más conjuntos de datos según sea necesario
        yield return new TestCaseData(new List<ProductDm>
        {
            new ProductDm { Code = "n5m2b6n0", Name = "bombilla", WeightKg = 19.1f , FamilyCode = "GLT", Reference = "XCL", Stock = 100, Size = "15x15" },
            new ProductDm { Code = "b6n5m43c", Name = "fluorescente XL", WeightKg = 2.5f , FamilyCode = "POI", Reference = "XCV", Stock = 5, Size = "5x50"  }
        });
    }

    [Test]
    [TestCaseSource(nameof(FakeProductsTestData))]
    public async Task GetProducts_ShouldReturnProductDmList(List<ProductDm> expectedProducts)
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();

        mockRepository.Setup(
            repo => repo.GetProducts()).
            ReturnsAsync(expectedProducts);

        // Act
        var actualProducts = await mockRepository.Object.GetProducts();

        // Assert
        Assert.IsNotNull(actualProducts);
        Assert.That(actualProducts, Is.InstanceOf<List<ProductDm>>());
        CollectionAssert.AreEqual(expectedProducts, actualProducts);
    }
}
using System.Net.Http.Json;
using DbProjectExample.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DbProjectExample.Tests
{
    public class ProductsApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetProducts_ReturnsSeededProducts()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/products");

            // Assert
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            
            Assert.NotNull(products);
            Assert.Equal(10, products.Count);
            Assert.Contains(products, p => p.Name == "Milk");
            Assert.Contains(products, p => p.Name == "Coffee");
        }
    }
}

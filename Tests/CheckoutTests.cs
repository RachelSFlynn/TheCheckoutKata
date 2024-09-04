using Xunit;
using Domain.Products;
using Domain.Checkout;
using FluentAssertions;

namespace Tests;

public class CheckoutTests
{
    private Checkout _checkout;

    public CheckoutTests()
    {
        var pricingRulesA = new List<PricingRule>
        {
            { new PricingRule(3, 50) },
            { new PricingRule(4, 40) }
        };

        var pricingRulesB = new List<PricingRule>
        {
            { new PricingRule(3, 50) }
        };

        var pricingRuleC = new List<PricingRule>();

        var products = new Dictionary<string, Product>
        {
            { "A", new Product(60, pricingRulesA, "Item A") },
            { "B", new Product(70, pricingRulesB, "Item B" ) },
            { "C", new Product(80, pricingRuleC, "Item C", "Item C does not have any offers right now. Check back later." ) }
        };

        _checkout = new Checkout(products);
    }

    [Fact]
    public void ScanWithSubTotal_ProductWithNoOffers_CorrectPrice()
    {
        // Arrange
        decimal expectedPrice = 70 * 3;

        // Act
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("B");
        decimal totalPrice = _checkout.GetSubTotal();

        // Assert
        totalPrice.Should().Be(expectedPrice);
    }

    [Fact]
    public void ScanWithTotal_ProductWithNoOffers_CorrectPrice()
    {
        // Arrange
        decimal expectedPrice = 80 * 3;

        // Act
        _checkout.Scan("C");
        _checkout.Scan("C");
        _checkout.Scan("C");
        decimal totalPrice = _checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(expectedPrice);
    }

    [Theory]
    [InlineData(50, "B", "B", "B")]
    [InlineData(190, "C", "B", "A", "B", "B")]
    [InlineData(40, "A", "A", "A", "A")]
    [InlineData(100, "A", "A", "A", "A", "A")]
    [InlineData(90, "A", "B", "A", "B", "A", "B", "A")]
    public void ScanWithTotal_ProductWithOffers_CorrectPrice(decimal expectedPrice, params string[] products)
    {
        // Arrange

        // Act
        foreach (var product in products)
        {
            _checkout.Scan(product);
        }
        decimal totalPrice = _checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(expectedPrice);
    }

    [Fact]
    private void ScanProduct_ProductNotAdded_ThrowsException()
    {
        // Arrange
        var productSku = "Invalid";

        // Act
        Action act = () => _checkout.Scan(productSku);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage($"Failed to scan {productSku} with error: Invalid SKU: {productSku}");
    }

    [Fact]
    public void ScanProduct_ShouldAddProductToScannedProduct_WhenProductIsValid()
    {
        // Arrange
        var productSku = "Scan";
        var products = new Dictionary<string, Product>
        {
            { productSku, new Product(10.0m, new List<PricingRule>(), "Scan should add product") }
        };
        var checkout = new Checkout(products);

        // Act
        checkout.Scan(productSku);

        // Assert
        checkout.ScannedItems.Should().ContainKey(productSku);
        checkout.ScannedItems[productSku].Should().Be(1);
    }
}
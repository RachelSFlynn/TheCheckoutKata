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
            { new PricingRule(4, 40) },
            { new PricingRule(5, 30) },
            { new PricingRule(6, 20) }
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
    public void Checkout_ProductWithNoOffers_CorrectPrice()
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


    [Fact]
    public void Checkout_ProductWithOffers_CorrectPrice()
    {
        // Arrange
        decimal expectedPrice = 50;

        // Act
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("B");
        decimal totalPrice = _checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(expectedPrice);
    }
}
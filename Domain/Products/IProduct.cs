namespace Domain.Products;

internal class IProduct
{
    /// <summary>
    /// Gets or sets the unit price.
    /// </summary>
    decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the pricing rules for the item.
    /// </summary>
    IList<PricingRule> Offers { get; set; } = new List<PricingRule>();
}

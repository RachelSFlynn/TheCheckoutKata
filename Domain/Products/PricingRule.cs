namespace Domain.Products;

internal class PricingRule
{
    /// <summary>
    /// Gets or sets the quantity needed for discount.
    /// </summary>
    internal int SpecialQuantity { get; set; }

    /// <summary>
    /// Gets or sets the discount price.
    /// </summary>
    internal decimal SpecialPrice { get; set; }

    internal PricingRule(int specialQuantity, decimal specialPrice)
    {
        SpecialQuantity = specialQuantity;
        SpecialPrice = specialPrice;
    }
}

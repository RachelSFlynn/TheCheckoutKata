namespace Domain.Products;

public class PricingRule
{
    /// <summary>
    /// Gets or sets the quantity needed for discount.
    /// </summary>
    public int SpecialQuantity { get; set; }

    /// <summary>
    /// Gets or sets the discount price.
    /// </summary>
    public decimal SpecialPrice { get; set; }

    public PricingRule(int specialQuantity, decimal specialPrice)
    {
        SpecialQuantity = specialQuantity;
        SpecialPrice = specialPrice;
    }
}

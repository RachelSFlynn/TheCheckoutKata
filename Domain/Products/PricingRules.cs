namespace Domain.Products;

public class PricingRules
{
    /// <summary>
    /// Gets or sets the quantity needed for discount.
    /// </summary>
    public int SpecialQuantity { get; set; }

    /// <summary>
    /// Gets or sets the discount price.
    /// </summary>
    public decimal SpecialPrice { get; set; }

    /// <summary>
    /// Gets or sets a list of products linked in the offer.
    /// </summary>
    public IList<Product>? LinkedProducts { get; set; }

    public PricingRules(int specialQuantity, decimal specialPrice)
    {
        SpecialQuantity = specialQuantity;
        SpecialPrice = specialPrice;
    }
}

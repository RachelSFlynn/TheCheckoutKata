namespace Domain.Products;

public class Product
{
    /// <summary>
    /// Gets or sets the unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the pricing rules for the item.
    /// </summary>
    public IList<PricingRules>? Offers { get; set; }

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the item.
    /// </summary>
    public string? Description { get; set; }

    public Product(decimal unitPrice, string name, string description = "No description given")
    {
        Name = name;
        UnitPrice = unitPrice;
        Description = description;
    }
}

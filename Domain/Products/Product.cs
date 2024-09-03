namespace Domain.Products;

internal class Product
{
    /// <summary>
    /// Gets or sets the unit price.
    /// </summary>
    internal decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the pricing rules for the item.
    /// </summary>
    internal IList<PricingRule> Offers { get; set; } = new List<PricingRule>();

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    internal string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the item.
    /// </summary>
    internal string? Description { get; set; }

    internal Product(decimal unitPrice, string name, string description = "No description given")
    {
        Name = name;
        UnitPrice = unitPrice;
        Description = description;
    }
}

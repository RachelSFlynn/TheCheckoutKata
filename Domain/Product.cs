namespace Domain;

public class Product
{
    /// <summary>
    /// Gets or sets the Primary Key for the item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Stock Keeping Units for the item.
    /// </summary>
    public string SKU { get; set; }

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the unit price.
    /// </summary>
    public int UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the description of the item.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the pricing rules for the item.
    /// </summary>
    public IList<PricingRules> Offers { get; set; }

    public Product(string sku, string name, int unitPrice, string description = "No description given")
    {
        SKU = sku;
        Name = name;
        UnitPrice = unitPrice;
        Description = description;
    }
}

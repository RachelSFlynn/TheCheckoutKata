using Domain.Products;

namespace Domain.Checkout;

public class Checkout : ICheckout
{
    private readonly Dictionary<string, Product> _products;

    public Dictionary<string, int> ScannedItems { get; set; }

    public Checkout(Dictionary<string, Product> products)
    {
        _products = products;
        ScannedItems = new Dictionary<string, int>();
    }

    /// <summary>
    /// Scans the products and adds it to the dictionary of scanned products.
    /// </summary>
    /// <param name="product">The product to scan</param>
    /// <exception cref="ArgumentException">Throws an exception if something is wrong, focus here is on if the product doesnt exist</exception>
    public void Scan(string product)
    {
        try
        {
            if (!_products.ContainsKey(product))
            {
                throw new ArgumentException($"Invalid SKU: {product}");
            }

            if (ScannedItems.ContainsKey(product))
            {
                ScannedItems[product]++;
            }
            else
            {
                ScannedItems[product] = 1;
            }
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Failed to scan {product} with error: {e.Message}");
        }
    }

    /// <summary>
    /// Gets the total price including discounts
    /// </summary>
    /// <returns>The total price</returns>
    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;

        foreach (var item in ScannedItems)
        {
            var sku = item.Key;
            var quantity = item.Value;
            var product = _products[sku];

            if (product.Offers.Any())
            {
                var remainingQuantity = quantity;
                var sortedOffers = product.Offers.OrderByDescending(o => o.SpecialQuantity).ToList();

                foreach (var offer in sortedOffers)
                {
                    var matchingOffer = sortedOffers.FirstOrDefault(o => remainingQuantity >= o.SpecialQuantity);

                    if (matchingOffer != null)
                    {
                        int offerCount = remainingQuantity / offer.SpecialQuantity;
                        totalPrice += offerCount * offer.SpecialPrice;
                        remainingQuantity %= offer.SpecialQuantity;
                    }
                }

                if (remainingQuantity > 0)
                {
                    totalPrice += remainingQuantity * product.UnitPrice;
                }
            }
            else
            {
                totalPrice += quantity * product.UnitPrice;
            }
        }

        return totalPrice;
    }

    /// <summary>
    /// Gets the current subtotal which is the full price before discounts.
    /// </summary>
    /// <returns><The subtotal/returns>
    public decimal GetSubTotal()
    {
        decimal subTotal = 0;

        foreach (var item in ScannedItems)
        {
            var sku = item.Key;
            var quantity = item.Value;
            var product = _products[sku];

            subTotal += quantity * product.UnitPrice;
        }

        return subTotal;
    }
}

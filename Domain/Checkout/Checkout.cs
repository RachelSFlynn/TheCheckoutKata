using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Products;

namespace Domain.Checkout;

public class Checkout : ICheckout
{
    private readonly Dictionary<string, Product> _products;
    private readonly Dictionary<string, int> _scannedItems;

    public Checkout(Dictionary<string, Product> products)
    {
        _products = products;
        _scannedItems = new Dictionary<string, int>();
    }

    public void Scan(string item)
    {
        if (!_products.ContainsKey(item))
        {
            throw new ArgumentException($"Invalid SKU: {item}");
        }

        if (_scannedItems.ContainsKey(item))
        {
            _scannedItems[item]++;
        }
        else
        {
            _scannedItems[item] = 1;
        }
    }

    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;

        foreach (var item in _scannedItems)
        {
            var sku = item.Key;
            var quantity = item.Value;
            var product = _products[sku];

            if (product.Offers.Any())
            {
                foreach (var offer in product.Offers)
                {
                    if (quantity >= offer.SpecialQuantity)
                    {
                        GetOfferPriceTotal(totalPrice, quantity, product, offer);
                    }
                }
            }
            else
            {
                totalPrice += quantity * product.UnitPrice;
            }
        }

        return totalPrice;
    }

    private decimal GetOfferPriceTotal(decimal totalPrice, int quantity, Product product, PricingRule offer)
    {
        int specialPriceCount = quantity / offer.SpecialQuantity;
        decimal specialPriceTotal = specialPriceCount * offer.SpecialPrice;

        int regularPriceCount = quantity % offer.SpecialQuantity;
        decimal regularPriceTotal = regularPriceCount * product.UnitPrice;

        totalPrice += specialPriceTotal + regularPriceTotal;

        return totalPrice;
    }
}

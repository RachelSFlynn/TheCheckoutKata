# The Checkout Task

## The Objective:

In your shop, products are identified by letters (A, B, C, etc.) and priced individually. Some items have special multi-price offers (e.g., buy multiples for a discount). The checkout system must handle items scanned in any order and apply the correct pricing, including offers. Pricing rules can change frequently, so they need to be passed in at the start of each transaction.

You need to implement a class library to handle this, including tests.

## Architecture

The checkout system is implemented through a `Checkout` object and interface, which encapsulate the primary pricing calculations.

The `Checkout` class maintains a dictionary of all products, ensuring the validation of unique SKUs.

Within the `Checkout` class, you can perform operations such as scanning items, retrieving the total price, and obtaining a subtotal. The subtotal represents the price of all scanned items without any discounts, while the total price includes applicable discounts.

A `Product` class includes key attributes such as unit price and a list of pricing rules. These pricing rules represent special offers, allowing multiple offers to be associated with a single product.

## Extension

It would be beneficial to extend the system to include pricing rules that apply across multiple products. This enhancement would necessitate rearchitecting the solution to eliminate the dependency on a dictionary of SKU strings and products within the Checkout class.

### Prerequisites
1. .NET 8.0 SDK or later
1. Any IDE that supports C# (e.g., Visual Studio, Visual Studio Code)

### Installation
Clone the repository:
git clone https://github.com/yourusername/ZenithTask.git
cd ZenithTask

### Install dependencies:
dotnet restore

### Build the project:
dotnet build

## Usage
Here’s an example of how to use the Checkout class:

C#

```
var pricingRulesA = new List<PricingRule>
{
    { new PricingRule(3, 50) },
    { new PricingRule(4, 40) }
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

checkout = new Checkout(products);

checkout.Scan("A");
checkout.Scan("B");
checkout.Scan("A");
checkout.Scan("A");

Console.WriteLine($"Subtotal: {checkout.Subtotal()}");  // Output: price without discounts
Console.WriteLine($"Total: {checkout.Total()}");        // Output: price with discounts
```

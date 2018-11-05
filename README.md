# Summary

The Gilded Rose wants to sell its goods. Let's give them an API for their
customers.

# Assumptions

The Gilded Rose's inventory is made of "the finest items", i.e., each item
has a high price and a low volume.

Merchants with access to the Inventory already have accounts established via
another set of endpoints. We assume the account portal provides them with the
ability to retrieve an API key, revoke an API key, and set up a payment
method.

The Gilded Rose uses a payment processor (e.g., Stripe).

# Examples

First, run the server, e.g.:

`c:\projects\gilded-rose\src\GildedRose.Web>dotnet run`

Then, get the items the Gilded Rose is selling:

`curl https://localhost:5001/api/items -k`

And finally, order something:

`curl -H "Content-Type: application/json" -H "Authorization: Bearer E5E2C487F77BA" -X POST -d @order.json -k https://localhost:5001/api/orders`

Where `order.json` is:

```
{
  "customer": {
    "id": "251ab349-1a86-4287-8a1f-0e2c60377f1f",
    "name": "Good Customer"
  },
  "items": [{ "itemId": "bf222c45-dab5-4898-8a14-8cceea4c7f28", "count": 1 }]
}
```

# Detailed Design

## API Workflows

- List items for sale

  GET /api/items => IEnumerable<InventoriedItem>

- Buy an item

  POST Order /api/orders => OrderResult

## Contexts

- Clients
- Inventory
- Ordering
- Payments

### Context Interactions

```
List: Clients <-> Inventory

Buy:  Clients <-> Ordering <-> Payment
                           <-> Inventory
```

## Models

`Item`: something the Gilded Rose sells

`PurchasedItem`: something a `Customer` has purchased

`InventoriedItem`: an `Item` in the inventory; has an Id and Count

`Customer`: a potential buyer; has an API key for making purchases; has a payment method established

`OrderProcessor`: handles the potential purchase of Items

`Order`: a collection of items a customer wants to purchase

`OrderResult`: the result of placing an `Order`

`OrderItem`: an inventoried item that a customer wants to purchase

`PaymentProcessor`: handles billing `Customer`s

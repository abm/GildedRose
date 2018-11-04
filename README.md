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

# Detailed Design

## API Workflows

- List items for sale

  GET /items => IEnumerable<InventoriedItem>

- Buy an item

  POST Order /purchases => PurchasedItem

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
`OrderingSystem`: handles the potential purchase of Items
`PaymentProcessor`: handles billing `Customer`s

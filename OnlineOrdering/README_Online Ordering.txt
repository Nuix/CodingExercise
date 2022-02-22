Nuix Online Ordering User Story Details
=======================================
** Included are a .SQL DDL for generating all the DB objects as well as a .bak backup **

1. The products table uses a rolling EndDate field to track price change history.  The ItemNo will be the product number used for stock, but the ID will be referenced in orders to be able to track item details at the time of purchase.  The null EndDate record is the current state.

2. Customers use the same EndDate pattern to track a record state.  We can see discount percent at time of order.

3. Order table holds the basic top level order info.  A status field would indicate order flow (created, sent, fulfilled, canceled, etc.)  This field would need evaluated in some data changes to check if the operation would still be allowed.  For example, cannot add an item to a fulfilled order.  The order total will remain null and would be calculated based on product costs, quantities, and customer discount at time of submit.

4. OrderDetail table contains line items.  The UI would be responsible for detecting if we currently have an existing order.  If not, CreateOrder would be called first, and the OrderID used to add new items.  AddOrderItem will add an item to the order or increment the qty to an existing record.

5. DeleteItem will remove an item from an order

6. UpdateOrderQty will update the qty of a record on OrderDetail

7. GetOrderByOrderId returns an Order record, and one or more OrderDetail records for an OrderID

8. Discounts contains the min/max range and percent for discounts.  GetCustomerSpend accepts a date range for reuse.  A process can call that to pull customer spend for the range, then use the discount table data to assign new discount percent to Customer records.
namespace Dal;

public struct OrderItem
{
    public int ID { get; set; }
    public int OrderID { get; set; }    
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    Order ID={ID}: {OrderID}, 
    ProductID - {ProductID}
    PricePerUnit: {Price}
    Amount in stock: {Amount}";
}

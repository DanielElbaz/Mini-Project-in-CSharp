namespace DalFacade.DO;

public struct OrderItem
{
    public Guid ID { get; set; }
    public int OrderID { get; set; }    
    public int ProductID { get; set; }
    public int PricePerUnit { get; set; }
    public int Amount { get; set; }
}

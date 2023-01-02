
using System.Diagnostics;
//using DO;
namespace DO
{ 

/// <summary>
/// structure for order
/// </summary>
public struct Order
{
    public int ID { get; set; } 
    /// <summary>
    /// id
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customer name   
    /// </summary>
    public string? CustomerEmail { get; set; }  
    /// <summary>
    /// costumer email
    /// </summary>
    public string? CustomerAddress{ get; set; }
    /// <summary>
    /// customer address
    /// </summary>
    public DateTime? OrderDate { get; set; } 
    /// <summary>
    /// date of the order
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// ship date
    /// </summary>
    public DateTime? DeliveryDate { get; set; } /// <summary>
    /// delivery time
    /// </summary>
    /// <returns></returns>
    /// tostring method 
    public override string ToString() => $@"
    Order ID={ID}: , 
    CustomerName - {CustomerName}
    CustomerEmail: {CustomerEmail}
    CustomerAdress: {CustomerAdress}
    OrderDate: {OrderDate}
    ShipDate: {ShipDate}
    DeliveryDate: {DeliveryDate}";
}
}


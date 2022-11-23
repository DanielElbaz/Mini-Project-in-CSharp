using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int ItemID { get; set; } 
    public int ProductID { get; set; } 
    public string? ProductName { get; set; }
    public double ProductPrice { get; set; }
    public int Amount { get; set; } 
    public double TotalPrice { get; set; }
}

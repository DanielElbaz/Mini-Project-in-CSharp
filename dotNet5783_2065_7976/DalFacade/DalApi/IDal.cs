using DO;
using Dal;

namespace DalApi;

public interface IDal 
{
    IProduct product { get; }
    IOrder order { get; }   
    IOrderItem orderitem { get; }

}

using DO;
//using Dal;

namespace DalApi
{
    public interface IDal
    {
        IProduct Product { get; }
        IOrder Order { get; }
        IOrderItem OrderItem { get; }

    }
}
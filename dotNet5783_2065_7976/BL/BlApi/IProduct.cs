using BO;

namespace BlApi;

public interface IProduct
{
    IEnumerable<Product> GetProducts();
    IEnumerable<ProductForList> GetListedProducts();
    Product GetByID(int id);
}

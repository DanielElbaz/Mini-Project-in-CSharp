using BO;

namespace BlApi;
/// <summary>
/// interface for product
/// </summary>
public interface IProduct
{
    
    /// <summary>
    /// gets the list of products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList> GetListedProducts();
    /// <summary>
    /// get a product details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Product GetProduct(int id);
    /// <summary>
    /// get product fo catalog
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns>product item   </returns>
    public ProductItem GetProductForCatalog(int id, Cart cart);
    /// <summary>
    /// add product for manager
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(Product product);
    /// <summary>
    /// update product for manager
    /// </summary>
    /// <param name="product"></param>
     public void UpdateProduct(Product product);
    /// <summary>
    /// delete product for manger
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduct(int id);

}

//using DO;

namespace DO
{
    /// <summary>
    /// struct for product
    /// </summary>
    /// 

    public struct Product
    {
        // enum cat { a, b, c };

        public int ID { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// category
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// price
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        /// in stock
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $@"
    Product ID: {ID} 
    Name :  {Name},
    category: {Category}
    Price: {Price}
    Amount in stock: {InStock}";

    }
}
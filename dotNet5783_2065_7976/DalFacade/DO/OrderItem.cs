namespace DO
{
    //using DO;
    /// <summary>
    /// struct for order item
    /// </summary>
    public struct OrderItem
    {
        public int ID { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// order id    
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// product id
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// price 
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// amount 
        /// </summary>
        /// <returns></returns>
        /// tostring method
        public override string ToString() => $@" 
    ID={ID}: {ID},
    Order ID={ID}: {OrderID}, 
    ProductID - {ProductID}
    PricePerUnit: {Price}
    Amount in stock: {Amount}";
    }
}
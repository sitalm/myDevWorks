namespace SignalROwinApplication.DomainModel
{
    using System.Collections.Generic;

    public interface IProductsRepository
    {
        IList<Product> GetAllProducts();

        void Insert(Product product);

        void Update(Product product);

        void Delete(int productId);
    }
}
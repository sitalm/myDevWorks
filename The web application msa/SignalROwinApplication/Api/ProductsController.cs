namespace SignalROwinApplication.Api
{
    using System.Collections.Generic;
    using System.Web.Http;
    using SignalROwinApplication.DomainModel;

    public class ProductsController : ApiController
    {
        private readonly IProductsRepository productsRepository = new ProductsRepository();

        public IEnumerable<Product> Get()
        {
            return this.productsRepository.GetAllProducts();
        }
    }
}

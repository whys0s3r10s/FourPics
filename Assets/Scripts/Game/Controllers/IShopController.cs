using System.Collections.Generic;

namespace FourPics
{
    public interface IShopController
    {
        void BuyProduct(ProductData product);

        List<ProductData> GetProducts();
    }
}
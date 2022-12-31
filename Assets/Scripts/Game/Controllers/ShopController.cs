using System;
using System.Collections.Generic;

namespace FourPics
{
    public class ShopController : IShopController
    {
        private readonly ShopContentData _shopContentData;
        private readonly IInventoryController _inventoryController;

        public ShopController(ShopContentData shopContentData, IInventoryController inventoryController)
        {
            _shopContentData = shopContentData ?? throw new ArgumentNullException(nameof(shopContentData));
            _inventoryController = inventoryController ?? throw new ArgumentNullException(nameof(inventoryController));
        }

        public List<ProductData> GetProducts()
        {
            return _shopContentData.Products;
        }

        public void BuyProduct(ProductData product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _inventoryController.AddCoins(product.CoinsReward);
        }
    }
}
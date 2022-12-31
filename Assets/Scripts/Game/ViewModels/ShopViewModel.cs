using System;
using System.Collections.Generic;

namespace FourPics
{
    public class ShopViewModel : ViewModelBase
    {
        private readonly IShopController _shopController;

        public List<ProductData> Products { get; private set; }

        public ShopViewModel(
            NavigationController navigationController,
            IShopController shopController) :
            base(navigationController)
        {
            _shopController = shopController ?? throw new ArgumentNullException(nameof(shopController));
        }

        public override void OnShowing()
        {
            base.OnShowing();

            UpdateRender();
        }

        public void OnBuy(ProductData product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _shopController.BuyProduct(product);
        }

        private void UpdateRender()
        {
            Products = _shopController.GetProducts();
        }
    }
}
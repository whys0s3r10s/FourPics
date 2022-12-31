using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace FourPics
{
    public class ShopView : ViewBase<ShopViewModel>
    {
        public override ViewNames ViewName => ViewNames.Shop;

        [SerializeField]
        private ShopItem shopItemPrefab;
        [SerializeField]
        private Transform shopItemsContainer;

        private readonly List<ShopItem> _shopItems = new();

        public override void Initialize(ShopViewModel viewModel)
        {
            base.Initialize(viewModel);

            Assert.IsNotNull(shopItemPrefab);
            Assert.IsNotNull(shopItemsContainer);
        }

        public void OnBack()
        {
            ViewModel.OnBack();
        }

        protected override void OnShowing()
        {
            base.OnShowing();

            UpdateShopItems();
        }

        private void UpdateShopItems()
        {
            if (ViewModel.Products == null)
                throw new InvalidOperationException("Products list is not defined");
            if (_shopItems == null)
                throw new InvalidOperationException($"{nameof(_shopItems)} list is not defined");

            for (int i = 0; i < ViewModel.Products.Count; i++)
            {
                if (i >= _shopItems.Count)
                {
                    ShopItem shopItem = Instantiate(shopItemPrefab, shopItemsContainer);

                    shopItem.Initialize(ViewModel.Products[i]);

                    shopItem.OnBuy += OnBuy;

                    _shopItems.Add(shopItem);
                }

                _shopItems[i].UpdateRender();
            }
        }

        private void OnBuy(ProductData product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            ViewModel.OnBuy(product);
        }
    }
}
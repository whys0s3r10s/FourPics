using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class ShopItem : MonoBehaviour
    {
        public Action<ProductData> OnBuy;

        [SerializeField]
        private Text coins;

        private ProductData _product;

        private void Awake()
        {
            Assert.IsNotNull(coins);
        }

        public void Initialize(ProductData product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public void OnClick()
        {
            if (_product == null)
                throw new InvalidOperationException($"{nameof(_product)} is not defined");

            OnBuy?.Invoke(_product);
        }

        public void UpdateRender()
        {
            if (_product == null)
                throw new InvalidOperationException($"{nameof(_product)} is not defined");

            coins.text = $"{_product.CoinsReward}";
        }
    }
}
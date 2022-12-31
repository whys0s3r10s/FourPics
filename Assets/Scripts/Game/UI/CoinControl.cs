using System;
using UnityEngine;
using UnityEngine.UI;

namespace FourPics
{
    public class CoinControl : MonoBehaviour
    {
        [SerializeField]
        private Text value;

        private CoinControlModel _controlModel;

        public void Initialize(CoinControlModel coinControlModel)
        {
            _controlModel = coinControlModel ?? throw new ArgumentNullException(nameof(coinControlModel));

            _controlModel.OnCoinsChanged += (coins) => UpdateRender(coins);

            UpdateRender();
        }

        public void UpdateRender(int? coins = null)
        {
            coins ??= _controlModel.Coins;

            value.text = $"{coins.Value}";
        }

        public void OnClick()
        {
            _controlModel.OnShop();
        }
    }
}
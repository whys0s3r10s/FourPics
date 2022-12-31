using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class HintItem : MonoBehaviour
    {
        public HintType HintType;

        [SerializeField]
        private Text price;

        private Action<HintType> _onClicked;

        public void Initialize(Action<HintType> onHintClicked, int hintPrice)
        {
            Assert.IsNotNull(price);

            _onClicked = onHintClicked ?? throw new ArgumentNullException(nameof(onHintClicked));

            price.text = $"{hintPrice}";
        }

        public void OnClick()
        {
            _onClicked?.Invoke(HintType);
        }
    }
}
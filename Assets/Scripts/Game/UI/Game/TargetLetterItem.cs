using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class TargetLetterItem : MonoBehaviour
    {
        public Text value;
        public Image background;

        public Color commonColor = Color.white;
        public Color lockedColor = Color.white;

        public Letter Letter { get; set; }

        private Action<Letter> _onClicked;

        public void Initialize(Action<Letter> onClicked)
        {
            Assert.IsNotNull(value);
            Assert.IsNotNull(background);

            _onClicked = onClicked ?? throw new ArgumentNullException(nameof(onClicked));
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateRender()
        {
            if (Letter != null)
            {
                value.text = $"{Letter.Value}";
            }
            else
            {
                value.text = "";
            }

            background.color = Letter != null && Letter.Locked ? lockedColor : commonColor;
        }

        public void OnClick()
        {
            if (Letter != null)
            {
                _onClicked?.Invoke(Letter);
            }
        }
    }
}
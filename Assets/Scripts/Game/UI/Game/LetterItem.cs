using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class LetterItem : MonoBehaviour
    {
        public Text value;
        public GameObject container;

        public Letter Letter { get; private set; }

        private Action<Letter> _onClicked;

        public void Initialize(Action<Letter> onClicked)
        {
            Assert.IsNotNull(value);

            _onClicked = onClicked ?? throw new ArgumentNullException(nameof(onClicked));
        }

        public void OnClick()
        {
            _onClicked?.Invoke(Letter);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetLetter(Letter letter)
        {
            Letter = letter ?? throw new ArgumentNullException(nameof(letter));

            UpdateRender();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateRender()
        {
            if (Letter.Attached)
            {
                value.text = "";
            }
            else
            {
                value.text = $"{Letter.Value}";
            }

            container.SetActive(!Letter.Removed && !Letter.Locked);
        }
    }
}
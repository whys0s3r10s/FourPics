using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace FourPics
{
    public class LettersControl : MonoBehaviour
    {
        public LetterItem letterItemPrefab;

        public Action<Letter> OnLetterClicked;

        private readonly List<LetterItem> _letterItems = new();

        private List<Letter> _letters;

        public void Initialize()
        {
            Assert.IsNotNull(letterItemPrefab);
        }

        public void Setup(List<Letter> letters)
        {
            _letters = letters ?? throw new ArgumentNullException(nameof(letters));

            // Show letter items
            for (int i = 0; i < letters.Count; i++)
            {
                if (i >= _letterItems.Count)
                {
                    LetterItem letterItem = Instantiate(letterItemPrefab, transform);
                    letterItem.Initialize(OnLetterClicked);
                    _letterItems.Add(letterItem);
                }

                _letterItems[i].Show();
            }

            // Hide unused letter items
            for (int i = letters.Count; i < _letterItems.Count; i++)
            {
                _letterItems[i].Hide();
            }

            UpdateRender();
        }

        public void UpdateRender()
        {
            for (int i = 0; i < _letters.Count; i++)
            {
                _letterItems[_letters[i].Index].SetLetter(_letters[i]);
            }
        }
    }
}
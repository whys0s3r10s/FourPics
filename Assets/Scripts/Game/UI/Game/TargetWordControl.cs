using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class TargetWordControl : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private TargetLetterItem targetLetterItemPrefab;
        [SerializeField]
        private string wrongAnimationTrigger = "wrong";
        [SerializeField]
        private GridLayoutGroup layoutGroup;
        [SerializeField]
        private RectTransform lettersContainer;

        public Action<Letter> OnLetterClicked;

        private readonly List<TargetLetterItem> _targetLetterItems = new();

        private List<Letter> _letters;
        private string _word;

        public void Initialize()
        {
            Assert.IsNotNull(animator);
            Assert.IsNotNull(targetLetterItemPrefab);
            Assert.IsNotNull(wrongAnimationTrigger);
            Assert.IsNotNull(layoutGroup);
            Assert.IsNotNull(lettersContainer);
        }

        public void Setup(string word, List<Letter> letters)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException($"'{nameof(word)}' cannot be null or empty.", nameof(word));

            _word = word;
            _letters = letters ?? throw new ArgumentNullException(nameof(letters));

            // Show target letter items based on the word length
            for (int i = 0; i < word.Length; i++)
            {
                if (i >= _targetLetterItems.Count)
                {
                    TargetLetterItem targetLetterItem = Instantiate(targetLetterItemPrefab, transform);
                    targetLetterItem.Initialize(OnLetterClicked);
                    _targetLetterItems.Add(targetLetterItem);
                }

                _targetLetterItems[i].Show();
            }

            // Hide unused target letters
            for (int i = word.Length; i < _targetLetterItems.Count; i++)
            {
                _targetLetterItems[i].Hide();
            }

            AdjustSize(_word.Length);

            UpdateRender();
        }

        public void UpdateRender()
        {
            if (_word == null)
                throw new InvalidOperationException($"{nameof(_word)} is not defined");
            if (_letters == null)
                throw new InvalidOperationException($"{nameof(_letters)} list not defined");
            if (_targetLetterItems == null)
                throw new InvalidOperationException($"{nameof(_targetLetterItems)} list not defined");

            for (int i = 0; i < _word.Length; i++)
            {
                Letter letter = null;

                for (int j = 0; j < _letters.Count; j++)
                {
                    if (i == _letters[j].AttachedIndex)
                    {
                        letter = _letters[j];
                    }
                }

                _targetLetterItems[i].Letter = letter;
                _targetLetterItems[i].UpdateRender();
            }
        }

        public void AnimateWrong()
        {
            if (animator == null)
                throw new InvalidOperationException($"{animator} is not defined");

            animator.SetTrigger(wrongAnimationTrigger);
        }

        private void AdjustSize(int letterCount)
        {
            if (letterCount <= 0)
                throw new ArgumentException($"{nameof(letterCount)} cannot be less or equal to 0");
            if (lettersContainer == null)
                throw new InvalidOperationException($"{lettersContainer} is not defined");
            if (layoutGroup == null)
                throw new InvalidOperationException($"{layoutGroup} is not defined");

            float availableWidth = lettersContainer.rect.width - layoutGroup.spacing.x * (letterCount - 1);
            float availableHeight = lettersContainer.rect.height;

            float maxLetterWidth = availableWidth / letterCount;
            float letterSize = Mathf.Min(maxLetterWidth, availableHeight);

            layoutGroup.cellSize = new Vector2(letterSize, letterSize);
        }
    }
}
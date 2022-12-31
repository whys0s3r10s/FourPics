using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace FourPics
{
    public class RemoveLetterHint : HintBase
    {
        public override HintType Type => HintType.RemoveLetter;

        public RemoveLetterHint(ILetterManager letterManager) : base(letterManager)
        {
        }

        public override bool CanBeUsed(GameInfo gameInfo)
        {
            if (gameInfo == null)
                throw new ArgumentNullException(nameof(gameInfo));
            if (gameInfo.Letters == null || gameInfo.Letters.Count == 0)
                throw new ArgumentException("Letters are null or empty", nameof(gameInfo));
            if (string.IsNullOrEmpty(gameInfo.Word))
                throw new ArgumentException($"Word cannot be null or empty.", nameof(gameInfo));

            List<Letter> availableLetters = LetterManager.GetLetterRemoveCandidates(gameInfo.Letters, gameInfo.Word);

            return availableLetters.Count > 0;
        }

        public override void Use(GameInfo gameInfo)
        {
            if (gameInfo == null)
                throw new ArgumentNullException(nameof(gameInfo));
            if (gameInfo.Letters == null || gameInfo.Letters.Count == 0)
                throw new ArgumentException("Letters are null or empty", nameof(gameInfo));
            if (string.IsNullOrEmpty(gameInfo.Word))
                throw new ArgumentException($"Word cannot be null or empty.", nameof(gameInfo));

            List<Letter> availableLetters = LetterManager.GetLetterRemoveCandidates(gameInfo.Letters, gameInfo.Word);

            if (availableLetters.Count == 0)
                throw new InvalidOperationException($"There are no more letters to be removed");

            Letter selectedLetter = availableLetters[Random.Range(0, availableLetters.Count)];

            LetterManager.Remove(selectedLetter);
        }
    }
}
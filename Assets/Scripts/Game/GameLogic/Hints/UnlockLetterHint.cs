using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace FourPics
{
    public class UnlockLetterHint : HintBase
    {
        public override HintType Type => HintType.UnlockLetter;

        public UnlockLetterHint(ILetterManager letterManager) : base(letterManager)
        {
        }

        public override bool CanBeUsed(GameInfo gameInfo)
        {
            return true;
        }

        public override void Use(GameInfo gameInfo)
        {
            if (gameInfo == null)
                throw new ArgumentNullException(nameof(gameInfo));
            if (gameInfo.Letters == null || gameInfo.Letters.Count == 0)
                throw new ArgumentException("Letters are null or empty", nameof(gameInfo));
            if (string.IsNullOrEmpty(gameInfo.Word))
                throw new ArgumentException($"Word cannot be null or empty.", nameof(gameInfo));

            List<int> remainingLetterIndices = new();

            for (int i = 0; i < gameInfo.Word.Length; i++)
            {
                bool hasUnlockedLetter = false;

                foreach (Letter letter in gameInfo.Letters)
                {
                    if (letter.Locked && letter.AttachedIndex == i)
                    {
                        hasUnlockedLetter = true;
                        break;
                    }
                }

                if (!hasUnlockedLetter)
                {
                    remainingLetterIndices.Add(i);
                }
            }

            int chosenIndex = remainingLetterIndices[Random.Range(0, remainingLetterIndices.Count)];
            char chosenChar = gameInfo.Word[chosenIndex];

            // Detach letter at that position
            foreach (Letter letter in gameInfo.Letters)
            {
                if (letter.Attached && letter.AttachedIndex.Value == chosenIndex)
                {
                    LetterManager.Detach(letter);
                }
            }

            foreach (Letter letter in gameInfo.Letters)
            {
                if (letter.Locked || letter.Removed)
                    continue;

                if (letter.Value == chosenChar)
                {
                    if (letter.Attached)
                    {
                        LetterManager.Detach(letter);
                    }

                    LetterManager.Unlock(letter, chosenIndex);
                    break;
                }
            }
        }
    }
}
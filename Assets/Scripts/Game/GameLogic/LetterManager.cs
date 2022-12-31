using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace FourPics
{
    public class LetterManager : ILetterManager
    {
        public List<Letter> SetupLetters(string lettersString)
        {
            if (string.IsNullOrEmpty(lettersString))
                throw new ArgumentException($"'{nameof(lettersString)}' cannot be null or empty.", nameof(lettersString));

            List<Letter> letters = new();

            for (int i = 0; i < lettersString.Length; i++)
            {
                letters.Add(new Letter(lettersString[i]) { Index = i });
            }

            return letters;
        }

        public void DetachAll(List<Letter> letters)
        {
            if (letters == null)
                throw new ArgumentNullException(nameof(letters));
            if (letters.Count == 0)
                throw new InvalidOperationException("Letters list is not initialized!");

            letters.ForEach(letter => Detach(letter));
        }

        public bool Detach(Letter letter)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));

            bool detached = false;

            if (!letter.Attached)
                throw new InvalidOperationException($"The given letter {letter.Value} is not attached");

            if (!letter.Locked)
            {
                detached = true;

                letter.AttachedIndex = null;
            }

            return detached;
        }

        public void Unlock(Letter letter, int attachIndex)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));
            if (letter.Attached && letter.Locked)
                throw new InvalidOperationException($"Letter {letter.Value}" +
                    $" is already attached to {letter.AttachedIndex.Value} target word index");

            letter.AttachedIndex = attachIndex;
            letter.Locked = true;
        }

        public bool TryAttach(Letter letter, int wordLength, List<Letter> letters)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));
            if (letter.Attached)
                throw new InvalidOperationException($"Letter {letter.Value}" +
                    $" is already attached to {letter.AttachedIndex.Value} target word index");
            if (letters == null)
                throw new ArgumentNullException(nameof(letters));
            if (wordLength < 1)
                throw new ArgumentException("Word length can't be less than 1", nameof(wordLength));

            bool attached = false;

            int? attachIndex = GetFirstUnattachedLetterIndex(letters, wordLength);

            if (attachIndex != null)
            {
                if (!letter.Locked)
                {
                    letter.AttachedIndex = attachIndex.Value;

                    attached = true;
                }
            }

            return attached;
        }

        public void Remove(Letter letter)
        {
            if (letter.Removed)
                throw new InvalidOperationException($"The given letter {letter.Value} is already removed");
            if (letter.Locked)
                throw new InvalidOperationException($"The given letter {letter.Value} is locked and cannot be removed");

            if (letter.Attached)
            {
                Detach(letter);
            }

            letter.Removed = true;
        }

        public bool AreAllLettersAttached(List<Letter> letters, int wordLength)
        {
            if (letters == null)
                throw new ArgumentNullException(nameof(letters));
            if (wordLength < 1)
                throw new ArgumentException("Word length can't be less than 1", nameof(wordLength));

            int attachedLettersCount = 0;

            foreach (Letter letter in letters)
            {
                if (letter.Attached)
                {
                    attachedLettersCount++;
                }
            }

            return wordLength == attachedLettersCount;
        }

        public List<Letter> GetLetterRemoveCandidates(List<Letter> letters, string word)
        {
            if (letters == null)
                throw new ArgumentNullException(nameof(letters));
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException($"'{nameof(word)}' cannot be null or empty.", nameof(word));

            List<Letter> result = new();

            // Add all letters except already removed ones
            foreach (Letter letter in letters)
            {
                if (letter.Removed)
                    continue;

                result.Add(letter);
            }

            // Shuffle the list
            for (int i = 0; i < result.Count; i++)
            {
                Letter temp = result[i];
                int randomIndex = Random.Range(i, result.Count);
                result[i] = result[randomIndex];
                result[randomIndex] = temp;
            }

            for (int i = 0; i < word.Length; i++)
            {
                int? usedLetterIndex = null;

                for (int j = 0; j < result.Count; j++)
                {
                    Letter letter = result[j];

                    // Remove the letter that is already used for hint
                    if (letter.Locked && letter.AttachedIndex == i)
                    {
                        usedLetterIndex = j;
                        break;
                    }
                    // Mark the letter if it fits the word
                    else if (letter.Value == word[i] && !letter.Locked)
                    {
                        usedLetterIndex = j;
                    }
                }

                if (usedLetterIndex != null)
                {
                    result.RemoveAt(usedLetterIndex.Value);
                }
            }

            return result;
        }

        private int? GetFirstUnattachedLetterIndex(List<Letter> letters, int wordLength)
        {
            if (letters == null)
                throw new ArgumentNullException(nameof(letters));
            if (letters.Count == 0)
                throw new ArgumentException("Letters list is not initialized!", nameof(letters));
            if (wordLength < 1)
                throw new ArgumentException("Word length can't be less than 1", nameof(wordLength));

            int? result = null;

            for (int i = 0; i < wordLength; i++)
            {
                bool hasAttachedLetter = false;

                foreach (Letter letter in letters)
                {
                    if (letter.AttachedIndex == i)
                    {
                        hasAttachedLetter = true;
                        break;
                    }
                }

                if (!hasAttachedLetter)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}
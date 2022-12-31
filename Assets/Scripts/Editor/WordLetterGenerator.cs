using System;
using System.Collections.Generic;

namespace FourPics
{
    public class WordLetterGenerator
    {
        private readonly string _alphabet;

        private readonly int _maxWordLength;

        private const int _minWordLength = 2;

        public WordLetterGenerator(string alphabet, int maxWordLength)
        {
            if (string.IsNullOrEmpty(alphabet))
                throw new ArgumentNullException(nameof(alphabet));

            if (maxWordLength < _minWordLength)
                throw new ArgumentException($"Word length can't be less than {_minWordLength}");

            _alphabet = alphabet;
            _maxWordLength = maxWordLength;
        }

        public string GenerateLetters(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word));

            if (word.Length > _maxWordLength)
                throw new ArgumentNullException($"Word length can't be greater than {_maxWordLength}");

            if (word.Length < _minWordLength)
                throw new ArgumentNullException($"Word length can't be lesser than {_minWordLength}");

            int additionalLettersNumber = _maxWordLength - word.Length;

            List<char> letters = new();

            for (int i = 0; i < word.Length; i++)
            {
                letters.Add(word[i]);
            }

            for (int i = 0; i < additionalLettersNumber; i++)
            {
                char randomChar = _alphabet[UnityEngine.Random.Range(0, _alphabet.Length)];
                letters.Add(randomChar);
            }

            for (int i = 0; i < letters.Count; i++)
            {
                char temp = letters[i];
                int randomIndex = UnityEngine.Random.Range(i, letters.Count);
                letters[i] = letters[randomIndex];
                letters[randomIndex] = temp;
            }

            return new string(letters.ToArray());
        }
    }
}
using System;
using System.Collections.Generic;

namespace FourPics
{
    public class GameInfo
    {
        public string Word { get; }

        public List<Letter> Letters { get; }

        public GameInfo(string word, List<Letter> letters)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException($"'{nameof(word)}' cannot be null or empty.", nameof(word));
            if (letters == null)
                throw new ArgumentNullException(nameof(letters));
            if (letters.Count == 0)
                throw new ArgumentException("Letters list shouldn't be empty", nameof(letters));

            Letters = letters;
            Word = word;
        }
    }
}
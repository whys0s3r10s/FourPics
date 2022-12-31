using System;
using System.Collections.Generic;

namespace FourPics
{
    public class CorrectWordChecker : ICorrectWordChecker
    {
        public bool Check(string word, List<Letter> letters)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word));
            if (letters == null)
                throw new ArgumentNullException(nameof(letters));
            if (letters.Count == 0)
                throw new ArgumentException("Letters list is empty", nameof(letters));
            if (word.Length > letters.Count)
                throw new ArgumentException("Word length cannot be greater than letters list", nameof(word));


            bool allLettersCorrect = true;

            for (int i = 0; i < word.Length; i++)
            {
                bool isLetterCorrect = false;

                foreach (Letter letter in letters)
                {
                    if (letter.AttachedIndex == i)
                    {
                        if (char.ToUpper(letter.Value) == char.ToUpper(word[i]))
                        {
                            isLetterCorrect = true;
                        }

                        break;
                    }
                }

                if (!isLetterCorrect)
                {
                    allLettersCorrect = false;
                    break;
                }
            }

            return allLettersCorrect;
        }
    }
}
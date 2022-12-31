using System.Collections.Generic;

namespace FourPics
{
    public interface ILetterManager
    {
        bool TryAttach(Letter letter, int wordLength, List<Letter> letters);

        bool Detach(Letter letter);

        void DetachAll(List<Letter> letters);

        void Remove(Letter letter);

        void Unlock(Letter letter, int attachIndex);

        bool AreAllLettersAttached(List<Letter> letters, int wordLength);

        List<Letter> GetLetterRemoveCandidates(List<Letter> letters, string word);

        List<Letter> SetupLetters(string lettersString);
    }
}
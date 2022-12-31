namespace FourPics
{
    public interface IGameManager
    {
        GameInfo GameInfo { get; }

        void StartGame(string word, string lettersString);

        bool AreAllLettersAttached();

        bool AttachLetter(Letter letter);

        bool DetachLetter(Letter letter);

        void DetachLetters();

        bool HasCorrectWord();        

        bool UseHint(HintType hintType);
    }
}
using System;
using System.Collections.Generic;

namespace FourPics
{
    public class GameManager : IGameManager
    {
        private readonly IHintManager _hintManager;
        private readonly ILetterManager _letterManager;
        private readonly ICorrectWordChecker _correctWordChecker;

        public GameInfo GameInfo { get; private set; }

        public GameManager(
            IHintManager hintManager,
            ILetterManager letterManager,
            ICorrectWordChecker correctWordChecker)
        {
            _hintManager = hintManager ?? throw new ArgumentNullException(nameof(hintManager));
            _letterManager = letterManager ?? throw new ArgumentNullException(nameof(letterManager));
            _correctWordChecker = correctWordChecker ?? throw new ArgumentNullException(nameof(correctWordChecker));
        }

        public void StartGame(string word, string lettersString)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException($"'{nameof(word)}' cannot be null or empty.", nameof(word));
            if (string.IsNullOrEmpty(lettersString))
                throw new ArgumentException($"'{nameof(lettersString)}' cannot be null or empty.", nameof(lettersString));

            List<Letter> letters = _letterManager.SetupLetters(lettersString);

            GameInfo = new(word, letters);
        }

        public void DetachLetters()
        {
            _letterManager.DetachAll(GameInfo.Letters);
        }

        public bool AttachLetter(Letter letter)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));

            return _letterManager.TryAttach(letter, GameInfo.Word.Length, GameInfo.Letters);
        }

        public bool DetachLetter(Letter letter)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));

            return _letterManager.Detach(letter);
        }

        public bool HasCorrectWord()
        {
            return _correctWordChecker.Check(GameInfo.Word, GameInfo.Letters);
        }

        public bool AreAllLettersAttached()
        {
            return _letterManager.AreAllLettersAttached(GameInfo.Letters, GameInfo.Word.Length);
        }

        public bool UseHint(HintType hintType)
        {
            return _hintManager.UseHint(hintType, GameInfo);
        }
    }
}
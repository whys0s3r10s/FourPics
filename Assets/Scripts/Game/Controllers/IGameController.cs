using System.Collections.Generic;

namespace FourPics
{
    public interface IGameController
    {
        CompletedLevelInfo CompletedLevelInfo { get; }

        void PlayCurrentLevel();

        AttachLetterResult AttachLetter(Letter letter);

        bool DetachLetter(Letter letter);

        List<Letter> GetCurrentLevelLetters();        

        UseHintResult UseHint(HintType hintType);
    }
}
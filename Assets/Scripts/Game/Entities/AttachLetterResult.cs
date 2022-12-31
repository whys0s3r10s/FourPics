using System;

namespace FourPics
{
    public class AttachLetterResult
    {
        public bool Attached { get; }

        public bool CompletedGame { get; }

        public bool AllLettersAttached { get; }

        public AttachLetterResult(bool attached, bool completedGame, bool allLettersAttached)
        {
            if (completedGame && (!attached || !allLettersAttached))
                throw new InvalidOperationException($"Can't have {nameof(completedGame)}' TRUE with" +
                    $" either '{nameof(attached)}' or '{nameof(allLettersAttached)}' having FALSE value");

            Attached = attached;
            CompletedGame = completedGame;
            AllLettersAttached = allLettersAttached;
        }
    }
}
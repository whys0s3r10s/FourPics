using System;

namespace FourPics
{
    public abstract class HintBase
    {
        public abstract HintType Type { get; }

        protected ILetterManager LetterManager { get; }

        public HintBase(ILetterManager letterManager)
        {
            LetterManager = letterManager ?? throw new ArgumentNullException(nameof(letterManager));
        }

        public abstract void Use(GameInfo gameInfo);

        public abstract bool CanBeUsed(GameInfo gameInfo);
    }
}
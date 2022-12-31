using System;
using System.Collections.Generic;

namespace FourPics
{
    public class HintManager : IHintManager
    {
        private readonly Dictionary<HintType, HintBase> _hints = new();

        public HintManager(List<HintBase> hints)
        {
            if (hints == null)
                throw new ArgumentNullException(nameof(hints));
            if (hints.Count == 0)
                throw new ArgumentException("Hints collection is empty", nameof(hints));

            foreach (HintBase hint in hints)
            {
                _hints.Add(hint.Type, hint);
            }
        }

        public bool UseHint(HintType hintType, GameInfo gameInfo)
        {
            if (!_hints.TryGetValue(hintType, out HintBase hint))
                throw new HintNotDefinedException(hintType);
            if (gameInfo == null)
                throw new ArgumentNullException(nameof(gameInfo));

            bool used = false;

            if (hint.CanBeUsed(gameInfo))
            {
                hint.Use(gameInfo);

                used = true;
            }

            return used;
        }
    }
}
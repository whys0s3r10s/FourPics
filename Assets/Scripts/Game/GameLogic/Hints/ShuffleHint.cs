using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace FourPics
{
    public class ShuffleHint : HintBase
    {
        public override HintType Type => HintType.Shuffle;

        public ShuffleHint(ILetterManager letterManager) : base(letterManager)
        {
        }

        public override bool CanBeUsed(GameInfo gameInfo)
        {
            return true;
        }

        public override void Use(GameInfo gameInfo)
        {
            if (gameInfo == null)
                throw new ArgumentNullException(nameof(gameInfo));
            if (gameInfo.Letters == null || gameInfo.Letters.Count == 0)
                throw new ArgumentException("Letters are null or empty", nameof(gameInfo));

            var indexList = new List<int>();

            for (int i = 0; i < gameInfo.Letters.Count; i++)
            {
                indexList.Add(i);
            }

            var shuffleIndexList = new List<int>();

            for (int i = 0; i < gameInfo.Letters.Count; i++)
            {
                int index = Random.Range(0, indexList.Count);

                int randomIndex = indexList[index];

                shuffleIndexList.Add(randomIndex);

                indexList.RemoveAt(index);
            }

            for (int i = 0; i < gameInfo.Letters.Count; i++)
            {
                gameInfo.Letters[i].Index = shuffleIndexList[i];
            }
        }
    }
}
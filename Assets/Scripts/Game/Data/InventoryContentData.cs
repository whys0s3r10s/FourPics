using System;
using System.Collections.Generic;
using UnityEngine;

namespace FourPics
{
    [Serializable]
    [CreateAssetMenu(fileName = "Inventory", menuName = "Content Data/Inventory")]
    public class InventoryContentData : ScriptableObject
    {
        public int StartingCoins = 100;

        public List<HintData> HintDatas = new()
        {
            new HintData { HintType = HintType.Shuffle },
            new HintData { HintType = HintType.RemoveLetter },
            new HintData { HintType = HintType.UnlockLetter },
        };        
    }
}
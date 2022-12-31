using System;
using System.Collections.Generic;

namespace FourPics
{
    public interface IInventoryController
    {
        Inventory Inventory { get; }

        event Action<int> OnCoinsChanged;

        void AddCoins(int amount);

        void RemoveCoins(int amount);

        HintData GetHintData(HintType hintType);

        Dictionary<HintType, int> GetHintPrices();

        bool HaveMoneyForHint(HintType hintType, out int requiredCoins);        
    }
}
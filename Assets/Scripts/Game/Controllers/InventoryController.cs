using System;
using System.Collections.Generic;

namespace FourPics
{
    public class InventoryController : IInventoryController
    {
        private readonly InventoryContentData _inventoryContentData;
        private readonly IInventoryRepository _inventoryRepository;

        public Inventory Inventory { get; private set; }

        public event Action<int> OnCoinsChanged;

        public InventoryController(InventoryContentData inventoryContentData, IInventoryRepository inventoryRepository)
        {
            _inventoryContentData = inventoryContentData ?? throw new ArgumentNullException(nameof(inventoryContentData));
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));

            Initialize();
        }

        public void AddCoins(int amount)
        {
            Inventory.Coins += amount;

            Save();

            OnCoinsChanged?.Invoke(Inventory.Coins);
        }

        public void RemoveCoins(int amount)
        {
            Inventory.Coins -= amount;

            Save();

            OnCoinsChanged?.Invoke(Inventory.Coins);
        }

        public bool HaveMoneyForHint(HintType hintType, out int requiredCoins)
        {
            HintData hintData = GetHintData(hintType);

            requiredCoins = hintData.Price;

            return hintData.Price <= Inventory.Coins;
        }

        public HintData GetHintData(HintType hintType)
        {
            if (_inventoryContentData.HintDatas == null)
                throw new InvalidOperationException($"Hints are not defined");

            HintData hintData = _inventoryContentData.HintDatas.Find(x => x.HintType == hintType);

            if (hintData == null)
                throw new HintNotDefinedException(hintType);

            return hintData;
        }

        public Dictionary<HintType, int> GetHintPrices()
        {
            Dictionary<HintType, int> result = new();

            foreach (HintData hintData in _inventoryContentData.HintDatas)
            {
                result.Add(hintData.HintType, hintData.Price);
            }

            return result;
        }

        private void Initialize()
        {
            Inventory = new()
            {
                Coins = _inventoryRepository.GetCoins()
            };
        }

        private void Save()
        {
            _inventoryRepository.SaveCoins(Inventory.Coins);
        }
    }
}
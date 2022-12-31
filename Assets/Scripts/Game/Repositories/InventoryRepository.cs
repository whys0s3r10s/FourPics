using System;

namespace FourPics
{
    public class InventoryRepository : RepositoryBase, IInventoryRepository
    {
        private const string CoinsKey = nameof(CoinsKey);

        private readonly InventoryContentData _inventoryContentData;

        public InventoryRepository(IStorage storage, InventoryContentData inventoryContentData) : base(storage)
        {
            _inventoryContentData = inventoryContentData ?? throw new ArgumentNullException(nameof(inventoryContentData));
        }

        public int GetCoins()
        {
            return Storage.GetInt(CoinsKey, _inventoryContentData.StartingCoins);
        }

        public void SaveCoins(int value)
        {
            Storage.SetInt(CoinsKey, value);
        }
    }
}
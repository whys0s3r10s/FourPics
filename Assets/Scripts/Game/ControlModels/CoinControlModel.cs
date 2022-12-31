using System;

namespace FourPics
{
    public class CoinControlModel
    {
        public int Coins { get; private set; }

        public event Action<int> OnCoinsChanged;

        private readonly NavigationController _navigationController;

        public CoinControlModel(InventoryController inventoryController, NavigationController navigationController)
        {
            if (inventoryController == null)
                throw new ArgumentNullException(nameof(inventoryController));
            if (inventoryController.Inventory == null)
                throw new ArgumentException("Inventory is not defined", nameof(inventoryController));

            _navigationController = navigationController ?? throw new ArgumentNullException(nameof(navigationController));

            Coins = inventoryController.Inventory.Coins;

            inventoryController.OnCoinsChanged += (coins) =>
            {
                Coins = coins;
                OnCoinsChanged?.Invoke(coins);
            };
        }

        public void OnShop()
        {
            _navigationController.NavigateTo(ViewNames.Shop);
        }
    }
}
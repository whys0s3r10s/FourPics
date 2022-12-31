using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace FourPics
{
    public class InitializationContainer : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField]
        private MainView mainView;
        [SerializeField]
        private GameView gameView;
        [SerializeField]
        private LevelCompletedView levelCompletedView;
        [SerializeField]
        private ShopView shopView;

        [Header("Data")]
        public LevelContentData levelContentData;
        public InventoryContentData inventoryContentData;
        public GameContentData gameContentData;
        public ShopContentData shopContentData;

        [Header("UI")]
        public CoinControl CoinControl;

        private NavigationController _navigationController;

        private void Awake()
        {
            Assert.IsNotNull(mainView);
            Assert.IsNotNull(gameView);
            Assert.IsNotNull(levelCompletedView);
            Assert.IsNotNull(shopView);
            Assert.IsNotNull(levelContentData);
            Assert.IsNotNull(inventoryContentData);
            Assert.IsNotNull(gameContentData);
            Assert.IsNotNull(shopContentData);
            Assert.IsNotNull(CoinControl);

            Initialize();
        }

        private void Start()
        {
            _navigationController.NavigateTo(ViewNames.Main);
        }

        private void Initialize()
        {
            // Storages
            PlayerPrefsStorage storage = new();

            // Game logic
            CorrectWordChecker correctWordChecker = new();
            LetterManager letterManager = new();
            HintManager hintManager = new(new List<HintBase>
            {
                new RemoveLetterHint(letterManager),
                new ShuffleHint(letterManager),
                new UnlockLetterHint(letterManager)
            });
            GameManager gameManager = new(hintManager, letterManager, correctWordChecker);

            // Repositories
            LevelRepository levelRepository = new(storage);
            InventoryRepository inventoryRepository = new(storage, inventoryContentData);

            // Controllers            
            _navigationController = new(new List<IView> { mainView, gameView, levelCompletedView, shopView });
            LevelController levelController = new(levelContentData, levelRepository);
            InventoryController inventoryController = new(inventoryContentData, inventoryRepository);
            ShopController shopController = new(shopContentData, inventoryController);
            GameController gameController = new(gameContentData, levelController, inventoryController, gameManager);

            // View Models
            MainViewModel mainViewModel = new(_navigationController, levelController, gameController);
            GameViewModel gameViewModel = new(_navigationController, levelController, gameController, inventoryController);
            LevelCompletedViewModel levelCompletedViewModel = new(_navigationController, levelController, gameController);
            ShopViewModel shopViewModel = new(_navigationController, shopController);

            // Views
            mainView.Initialize(mainViewModel);
            gameView.Initialize(gameViewModel);
            levelCompletedView.Initialize(levelCompletedViewModel);
            shopView.Initialize(shopViewModel);

            // Control Models
            CoinControlModel coinControlModel = new(inventoryController, _navigationController);

            // UI
            CoinControl.Initialize(coinControlModel);
        }
    }
}
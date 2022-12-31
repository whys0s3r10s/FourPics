using System;
using System.Collections.Generic;

namespace FourPics
{
    public class GameViewModel : ViewModelBase
    {
        private readonly LevelController _levelController;
        private readonly GameController _gameController;
        private readonly InventoryController _inventoryController;

        public int CurrentLevelNumber { get; private set; }
        public LevelData LevelData { get; private set; }
        public List<Letter> Letters { get; private set; }
        public Dictionary<HintType, int> HintPrices { get; private set; } = new();

        public event Action OnLettersChanged;
        public event Action OnWrongWord;

        public GameViewModel(
            NavigationController navigationController,
            LevelController levelController,
            GameController gameController,
            InventoryController inventoryController) : base(navigationController)
        {
            _levelController = levelController ?? throw new ArgumentNullException(nameof(levelController));
            _gameController = gameController ?? throw new ArgumentNullException(nameof(gameController));
            _inventoryController = inventoryController ?? throw new ArgumentNullException(nameof(inventoryController));
        }

        public override void OnShowing()
        {
            base.OnShowing();

            UpdateViewModel();
        }

        public void OnUseHint(HintType hintType)
        {
            UseHintResult useHintResult = _gameController.UseHint(hintType);

            if (useHintResult.NotEnoughCoins)
            {
                NavigateTo(ViewNames.Shop);
            }
            else if (useHintResult.Used)
            {
                OnLettersChanged?.Invoke();

                if (useHintResult.CompletedGame)
                {
                    NavigateTo(ViewNames.LevelCompleted);
                }
            }
        }

        public void AttachLetter(Letter letter)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));

            if (!letter.Attached)
            {
                AttachLetterResult result = _gameController.AttachLetter(letter);

                if (result.Attached)
                {
                    OnLettersChanged?.Invoke();
                }

                if (result.CompletedGame)
                {
                    NavigateTo(ViewNames.LevelCompleted);
                }
                else if (result.AllLettersAttached && result.Attached)
                {
                    OnWrongWord?.Invoke();
                }
            }
        }

        public void DetachLetter(Letter letter)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));

            if (letter.Attached && _gameController.DetachLetter(letter))
            {
                OnLettersChanged?.Invoke();
            }
        }

        private void UpdateViewModel()
        {
            CurrentLevelNumber = _levelController.UnlockedLevelNumber;

            LevelData = _levelController.GetCurrentLevel();

            Letters = _gameController.GetCurrentLevelLetters();

            HintPrices = _inventoryController.GetHintPrices();
        }
    }
}
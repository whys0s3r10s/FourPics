using System;
using System.Collections.Generic;

namespace FourPics
{
    public class GameController : IGameController
    {
        private readonly GameContentData _gameContentData;
        private readonly ILevelController _levelController;
        private readonly IInventoryController _inventoryController;
        private readonly IGameManager _gameManager;

        public CompletedLevelInfo CompletedLevelInfo { get; private set; }

        public GameController(
            GameContentData gameContentData,
            ILevelController levelController,
            IInventoryController inventoryController,
            IGameManager gameManager)
        {
            _gameContentData = gameContentData ?? throw new ArgumentNullException(nameof(gameContentData));
            _levelController = levelController ?? throw new ArgumentNullException(nameof(levelController));
            _inventoryController = inventoryController ?? throw new ArgumentNullException(nameof(inventoryController));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        public void PlayCurrentLevel()
        {
            LevelData currentLevel = _levelController.GetCurrentLevel();

            if (currentLevel == null)
                throw new InvalidOperationException("Could not get current level to play");

            _gameManager.StartGame(currentLevel.Word, currentLevel.Letters);
        }

        public List<Letter> GetCurrentLevelLetters()
        {
            if (_gameManager.GameInfo == null)
                throw new InvalidOperationException($"{nameof(GameInfo)} is not initialized");

            return _gameManager.GameInfo.Letters;
        }

        public AttachLetterResult AttachLetter(Letter letter)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));

            bool attached = _gameManager.AttachLetter(letter);
            bool completedGame = false;

            if (attached)
            {
                completedGame = TryCompleteLevel();
            }

            bool allLettersAttached = _gameManager.AreAllLettersAttached();

            return new(attached, completedGame, allLettersAttached);
        }

        public bool DetachLetter(Letter letter)
        {
            if (letter == null)
                throw new ArgumentNullException(nameof(letter));

            return _gameManager.DetachLetter(letter);
        }

        public UseHintResult UseHint(HintType hintType)
        {
            UseHintResult result = new();

            if (!_inventoryController.HaveMoneyForHint(hintType, out int requiredCoins))
            {
                result.NotEnoughCoins = true;
            }
            else
            {
                if (_gameManager.UseHint(hintType))
                {
                    _inventoryController.RemoveCoins(requiredCoins);

                    result.Used = true;

                    result.CompletedGame = TryCompleteLevel();
                }
            }

            return result;
        }

        private bool TryCompleteLevel()
        {
            bool completed = false;

            if (_gameManager.HasCorrectWord())
            {
                _levelController.MoveToNextLevel();

                _inventoryController.AddCoins(_gameContentData.CompletedLevelReward);

                CompletedLevelInfo = new(_gameContentData.CompletedLevelReward);

                completed = true;
            }

            return completed;
        }
    }
}
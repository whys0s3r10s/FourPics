using System;

namespace FourPics
{
    public class LevelCompletedViewModel : ViewModelBase
    {
        private LevelController _levelController;
        private GameController _gameController;

        public int Reward { get; private set; }

        public LevelCompletedViewModel(
            NavigationController navigationController,
            LevelController levelController,
            GameController gameController) :
            base(navigationController)
        {
            _levelController = levelController ?? throw new ArgumentNullException(nameof(levelController));
            _gameController = gameController ?? throw new ArgumentNullException(nameof(gameController));
        }

        public void OnNext()
        {
            if (_levelController.HasLevelsToPlay())
            {
                _gameController.PlayCurrentLevel();

                NavigateTo(ViewNames.Game);
            }
            else
            {
                NavigateTo(ViewNames.Main);
            }
        }

        public override void OnShowing()
        {
            base.OnShowing();

            UpdateViewModel();
        }

        public override void OnBack()
        {
        }

        private void UpdateViewModel()
        {
            if (_gameController.CompletedLevelInfo == null)
                throw new InvalidOperationException($"{nameof(CompletedLevelInfo)}" +
                    $" is not defined for {nameof(GameController)}");

            Reward = _gameController.CompletedLevelInfo.Reward;
        }
    }
}
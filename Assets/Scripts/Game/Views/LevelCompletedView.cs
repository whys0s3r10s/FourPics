using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class LevelCompletedView : ViewBase<LevelCompletedViewModel>
    {
        public override ViewNames ViewName => ViewNames.LevelCompleted;

        public Text reward;

        public override void Initialize(LevelCompletedViewModel viewModel)
        {
            base.Initialize(viewModel);

            Assert.IsNotNull(reward);
        }

        public void OnNext()
        {
            ViewModel.OnNext();
        }

        protected override void OnShowing()
        {
            base.OnShowing();

            reward.text = $"+{ViewModel.Reward}";
        }
    }
}
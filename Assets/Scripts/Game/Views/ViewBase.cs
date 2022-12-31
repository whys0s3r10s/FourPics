using System;

namespace FourPics
{
    public abstract class ViewBase<VM> : AdjustableContent, IView
        where VM : ViewModelBase
    {
        public abstract ViewNames ViewName { get; }

        protected VM ViewModel { get; private set; }

        public virtual void Initialize(VM viewModel)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void Show()
        {
            OnShowing();

            gameObject.SetActive(true);
        }       

        public void Hide()
        {
            OnHiding();

            gameObject.SetActive(false);
        }

        protected virtual void OnShowing()
        {
            ViewModel?.OnShowing();
        }

        protected virtual void OnHiding()
        {
            ViewModel?.OnHiding();
        }
    }
}
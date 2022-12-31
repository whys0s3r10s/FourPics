using System;

namespace FourPics
{
    public abstract class ViewModelBase
    {
        private readonly NavigationController _navigationController;

        public ViewModelBase(NavigationController navigationController)
        {
            _navigationController = navigationController ?? throw new ArgumentNullException(nameof(navigationController));
        }

        public virtual void OnShowing()
        {
        }

        public virtual void OnHiding()
        {
        }

        public virtual void OnBack()
        {
            GoBack();
        }

        protected void NavigateTo(ViewNames viewName)
        {
            _navigationController.NavigateTo(viewName);
        }

        protected void GoBack()
        {
            _navigationController.GoBack();
        }
    }
}
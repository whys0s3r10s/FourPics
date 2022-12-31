namespace FourPics
{
    public interface INavigationController
    {
        IView CurrentView { get; }

        void GoBack();

        void NavigateTo(ViewNames viewName);
    }
}
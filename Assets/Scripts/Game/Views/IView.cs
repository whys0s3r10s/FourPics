namespace FourPics
{
    public interface IView
    {
        ViewNames ViewName { get; }

        void Show();

        void Hide();
    }
}
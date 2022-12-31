using System;
using System.Collections.Generic;

namespace FourPics
{
    public class NavigationController : INavigationController
    {
        public IView CurrentView { get; private set; }

        private readonly Dictionary<ViewNames, IView> _viewDictionary = new();

        private IView _previousView;

        public NavigationController(List<IView> views)
        {
            if (views == null)
                throw new ArgumentNullException(nameof(views));
            if (views.Count == 0)
                throw new ArgumentException("Empty views list", nameof(views));

            foreach (IView view in views)
            {
                _viewDictionary.Add(view.ViewName, view);

                view.Hide();
            }
        }

        public void GoBack()
        {
            if (_previousView != null)
            {
                NavigateTo(_previousView.ViewName);
            }
        }

        public void NavigateTo(ViewNames viewName)
        {
            // Ignore navigating to the current view
            if (CurrentView == null || CurrentView.ViewName != viewName)
            {
                if (_viewDictionary.TryGetValue(viewName, out IView view))
                {
                    if (CurrentView != null)
                    {
                        CurrentView.Hide();
                        _previousView = CurrentView;
                    }

                    view.Show();

                    CurrentView = view;
                }
                else
                {
                    throw new NavigationException($"View with name {viewName} is not defined");
                }
            }
        }
    }
}
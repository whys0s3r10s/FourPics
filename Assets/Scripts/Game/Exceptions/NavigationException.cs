using System;

namespace FourPics
{
    public class NavigationException : Exception
    {
        public NavigationException(string message) :
            base(message)
        {
        }
    }
}
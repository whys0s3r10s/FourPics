using System;

namespace FourPics
{
    public class LevelNotFoundException : Exception
    {
        public LevelNotFoundException(string message) :
            base(message)
        {
        }
    }
}
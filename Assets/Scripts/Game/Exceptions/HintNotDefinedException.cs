using System;

namespace FourPics
{
    public class HintNotDefinedException : Exception
    {
        public HintNotDefinedException(HintType hintType) :
            base($"Hint {hintType} is not defined")
        {
        }
    }
}
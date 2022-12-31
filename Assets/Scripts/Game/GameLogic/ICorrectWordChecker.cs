using System.Collections.Generic;

namespace FourPics
{
    public interface ICorrectWordChecker
    {
        bool Check(string word, List<Letter> letters);
    }
}
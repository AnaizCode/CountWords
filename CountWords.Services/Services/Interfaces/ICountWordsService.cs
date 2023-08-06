using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords.Services.Services.Interfaces
{
    public interface ICountWordsService
    {

        int CountWords(string word, string text);
    }
}

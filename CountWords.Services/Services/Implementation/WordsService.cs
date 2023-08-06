using CountWords.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CountWords.Services.Services.Implementation
{
    public class WordsService : IWordsService

     
    {
        private IWikiClientService _findTextService;

        private ICountWordsService _countWordsService;

        public WordsService(IWikiClientService WikiClientService,ICountWordsService countWordsService)
        {
            _findTextService = WikiClientService;
            _countWordsService = countWordsService;
        }

        public int FindAndCountWords(string word )
        {
            
            string text = _findTextService.FindText(word);
            int result = _countWordsService.CountWords(word, text);
            return result;


        }
    }
}

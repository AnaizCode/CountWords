using CountWords.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords.Services.Services.Implementation
{
    public class CountWordsService:ICountWordsService
    {
       public CountWordsService()
        {
        }
        public int CountWords(string word, string text)
        {

            string[] arrayWord = this.DivideIntoArray(word);

            string[] textArrayLetters = this.DivideIntoArray(text);

            int wordLength = arrayWord.Length;
            int textLength = textArrayLetters.Length;
            int numberOfWords = 0;
            int numberOfRepeatedWords = 0;

            for (int i = 0; i < textLength - arrayWord.Length + 1; i++)
            {
                numberOfRepeatedWords += 1;
                string[] temporalWord = new string[wordLength];

                if (string.Equals(textArrayLetters[i].ToString(), arrayWord[0].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    Array.Copy(textArrayLetters, i, temporalWord, 0, wordLength);
                    string temporalWordString = string.Join("", temporalWord);


                    if (string.Equals(temporalWordString, word, StringComparison.OrdinalIgnoreCase))
                    {
                        numberOfWords += 1;
                    }
                    i += word.Length;
                    i -= 1;
                }
            }
            return numberOfWords;
        }
        private string[] DivideIntoArray(string text)
        {
            string[] result = new string[text.Length];
            var x = 0;
            foreach (var ch in text)
            {
                result[x] = ch.ToString();
                x++;
            }
            return result;
        }
    }
}

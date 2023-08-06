using CountWords.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CountWords.Api.Controllers
{

    [ApiController]
    public class CountWordsController : ControllerBase
    {
        private IWordsService _WordsService;

        public CountWordsController(IWordsService WordsService)
        {
            _WordsService = WordsService;
        }

        [HttpGet]
        [Route("api/word/{word}")]
        public IActionResult CountRepeatedWord([FromRoute] string word )
        {
            

            var result = _WordsService.FindAndCountWords(word);

            return Ok(result);
        }
    }
}

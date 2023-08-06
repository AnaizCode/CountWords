using Xunit;
using NSubstitute;
using AutoFixture.Xunit2;
using CountWords.Services.Services.Interfaces;
using CountWords.Api.Controllers;
using CountWords.Services.Services.Implementation;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using System.Threading;

namespace CountWords.Tests
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {



        private IWikiClientService _findTextService;
        private IWordsService _FindAndCountWords;
        private IHttpClientFactory _httpClientFactory;
        private ICountWordsService _CountWordsService;
        private HttpClient _httpClient;


        public UnitTest1()
        {
            this._findTextService = Substitute.For<IWikiClientService>(); ;
            this._FindAndCountWords = Substitute.For<IWordsService>();
            this._httpClientFactory = Substitute.For<IHttpClientFactory>();
            this._CountWordsService = Substitute.For<ICountWordsService>();
            this._httpClient = Substitute.For<HttpClient>();
        }
        [Theory]
        [InlineData("amor", "amor amor")]
        public async void whenCountWordsServiceReturnsRightInt(string word, string text)
        {
            // Arrange
            var countWordsService = new CountWordsService();

            // Act
            var result = countWordsService.CountWords(word, text);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.Equal(2, result);

        }
        [Theory]
        [InlineData("amor", "amor Amor")]
        public async void whenCountWordsServiceReturnsRightIntWithUpperCase(string word, string text)
        {
            // Arrange
            var countWordsService = new CountWordsService();

            // Act
            var result = countWordsService.CountWords(word, text);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.Equal(2, result);

        }

        [Theory]
        [InlineData("amor", "amor amor")]
        public async void whenFindAndCountWordsWorks(string word, string text)
        {
            // Arrange
            _findTextService.FindText(word).Returns(text);
            _CountWordsService.CountWords(word,text).Returns(2);

            var WordsService = new WordsService(_findTextService, _CountWordsService);

            // Act
            var result = WordsService.FindAndCountWords(word);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.Equal(2, result);

        }
        
        [Theory]
        [InlineData("amor","amor amor")]
        public async void WhenFindTextServiceWorks(string word,string text)
        {
            // Arrange
            Uri uri = new Uri("http://google.com");
            var messageHandler = new MockHttpMessageHandler(text, HttpStatusCode.OK,uri);
            var httpClient = new HttpClient(messageHandler);
            httpClient.BaseAddress = uri;
            _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);
             ;
            var WikiClientService = new WikiClientService(_httpClientFactory);

            // Act
            var result = WikiClientService.FindText(word);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(text, result);
        }        
    }
}
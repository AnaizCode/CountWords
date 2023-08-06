using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountWords.Tests
{
    class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly HttpStatusCode _statusCode;
        private readonly Uri BaseAddress;

        public MockHttpMessageHandler(string response, HttpStatusCode statusCode, Uri _BaseAddress)
        {
            _response = response;
            _statusCode = statusCode;
            BaseAddress = _BaseAddress;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
        {
            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_response)
            };
        }
    }
}

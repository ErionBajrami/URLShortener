using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using URLShortener.Controllers;
using URLShortener.Models;
using URLShortener.Service;
using URLShortener.Service.Url;
using Xunit;

namespace URLShortener.Test
{
    public class ShortenUrlTest
    {
        private readonly Mock<IUrlService> _urlServiceMock;
        private readonly Mock<IUrlValidationService> _urlValidationServiceMock;
        private readonly URLController _controller;

        public ShortenUrlTest()
        {
            _urlServiceMock = new Mock<IUrlService>();
            _urlValidationServiceMock = new Mock<IUrlValidationService>();
            _controller = new URLController(_urlServiceMock.Object, _urlValidationServiceMock.Object);
        }

        [Fact]
        public void ShortenUrl_InvalidUrl_ReturnsBadRequest()
        {
            // Arrange
            _urlValidationServiceMock.Setup(v => v.IsValidUrl(It.IsAny<string>())).Returns(false);

            // Act
            var result = _controller.ShortenUrl("invalidurl", "token", "description");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid URL format Please provide a valid URL starting with 'http://' or 'https://'.", badRequestResult.Value);
        }

        [Fact]
        public void ShortenUrl_ValidUrl_ReturnsShortUrl()
        {
            // Arrange
            var url = "https://example.com/";
            var token = "token";
            var description = "description";
            var shortUrl = "https://short.url/abc123";

            _urlValidationServiceMock.Setup(v => v.IsValidUrl(It.IsAny<string>())).Returns(true);
            _urlServiceMock.Setup(s => s.ShortenUrl(url, token, description)).Returns(shortUrl);

            // Act
            var result = _controller.ShortenUrl(url, token, description);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(shortUrl, okResult.Value);
        }

    }
}
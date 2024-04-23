using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using URLShortener.Controllers;
using URLShortener.Models;
using URLShortener.Service;
using URLShortener.Service.Url;
using Xunit;

namespace URLShortener.Test
{
    public class URLControllerTests
    {
        private readonly Mock<IUrlService> _urlServiceMock;
        private readonly Mock<IUrlValidationService> _urlValidationServiceMock;
        private readonly URLController _controller;

        public URLControllerTests()
        {
            _urlServiceMock = new Mock<IUrlService>();
            _urlValidationServiceMock = new Mock<IUrlValidationService>();
            _controller = new URLController(_urlServiceMock.Object, _urlValidationServiceMock.Object);
        }

        [Fact]
        public void GetUrl_WithValidId_ReturnsUrl()
        {
            // Arrange
            var urlId = 1;
            var url = new URL { Id = urlId };
            _urlServiceMock.Setup(service => service.GetById(urlId)).Returns(url);

            // Act
            var result = _controller.GetUrl(urlId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUrl = Assert.IsType<URL>(okResult.Value);
            Assert.Equal(urlId, returnedUrl.Id);
        }

        [Fact]
        public void GetUrl_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var urlId = 1;
            _urlServiceMock.Setup(service => service.GetById(urlId)).Returns((URL)null);

            // Act
            var result = _controller.GetUrl(urlId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Couldn't find url with the specified id: {urlId}", notFoundResult.Value);
        }

        [Fact]
        public void ShortenUrl_WithValidUrl_ReturnsOk()
        {
            // Arrange
            var url = "https://example.com";
            var token = "validtoken";
            var description = "Example description";
            var shortUrl = "https://short.url/abc";

            _urlValidationServiceMock.Setup(service => service.IsValidUrl(url)).Returns(true);
            _urlServiceMock.Setup(service => service.ShortenUrl(url, token, description)).Returns(shortUrl);

            // Act
            var result = _controller.ShortenUrl(url, token, description);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(shortUrl, okResult.Value);
        }

        [Fact]
        public void ShortenUrl_WithInvalidUrl_ReturnsBadRequest()
        {
            // Arrange
            var url = "invalidurl";
            var token = "validtoken";
            var description = "Example description";

            _urlValidationServiceMock.Setup(service => service.IsValidUrl(url)).Returns(false);

            // Act
            var result = _controller.ShortenUrl(url, token, description);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid URL format Please provide a valid URL starting with 'http://' or 'https://'.", badRequestResult.Value);
        }

        [Fact]
        public void Remove_WithValidId_ReturnsOk()
        {
            // Arrange
            var urlId = 1;

            // Act
            var result = _controller.Remove(urlId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("URL Deleted Succesfully", okResult.Value);
            _urlServiceMock.Verify(service => service.DeleteUrl(urlId), Times.Once);
        }

        [Fact]
        public void UpdateUrl_WithValidId_ReturnsOk()
        {
            // Arrange
            var urlId = 1;
            var description = "Updated description";

            // Act
            var result = _controller.UpdateUrl(urlId, description);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("URL updated Successfully", okResult.Value);
            _urlServiceMock.Verify(service => service.UpdateUrl(urlId, description), Times.Once);
        }
    }
}

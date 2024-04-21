using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.Database;
using URLShortener.ModelHelpers;
using URLShortener.Models;
using URLShortener.Service;
using URLShortener.Service.Url;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class URLController : Controller
    {
        private readonly IUrlService _urlService;
        private readonly IUrlValidationService _urlValidationService;
        public URLController(IUrlService urlService, IUrlValidationService urlValidationService)
        {
            _urlService = urlService;
            _urlValidationService = urlValidationService;
        }


        [HttpGet]
        public IActionResult GetUrls()
        {
            var allUrls = _urlService.GetAllUrls();
            if(allUrls.Any())
            {
                return Ok(allUrls);
            }
            return NotFound("Database is empty");
        }

        [HttpGet("{id}")]
        public IActionResult GetUrl(int id)
        {

            var url = _urlService.GetById(id);
            if(url == null)
            {
                return NotFound("Couldn't find url with the specified id: " + id);
            }
            return Ok(url);
        }

        
        [HttpPost]
        public IActionResult ShortenUrl(string url, int userId)
        {
            if (!_urlValidationService.IsValidUrl(url))
            {
                return BadRequest("Invalid URL format Please provide a valid URL starting with 'http://' or 'https://'.");
            }
            try
            {
                var shortUrl = _urlService.ShortenUrl(url, userId);
                return Ok(shortUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult Remove(int id)
        {
           _urlService.DeleteUrl(id);
            return Ok("URL Deleted Succesfully");
        }
        
   

        [HttpPut("{id}")]
        public IActionResult UpdateUrl(int id, [FromBody] UrlUpdate updated)
        {
            try
            {
                _urlService.UpdateUrl(id, updated);
                return Ok("URL updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}

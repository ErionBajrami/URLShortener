using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Database;
using URLShortener.ModelHelpers;
using URLShortener.Models;

namespace URLShortener.Controllers;


[ApiController]
[Route("api/search")]
public class URLSearchController : ControllerBase
{

    private readonly UrlShortenerDbContext _context;

    public URLSearchController(UrlShortenerDbContext context)
    {
        _context = context;
    }
    
    
    [HttpGet]
    public IActionResult SearchUrl(string UrlName)
    {
        if (string.IsNullOrEmpty(UrlName))
        {
            return BadRequest("Please type something...");
        }

        var results = _context.Urls
            .Where(url => url.OriginalUrl.Contains(UrlName.ToLower()))
            .ToList()
            .Select(url => new DisplayURL()
            {
                UserId = url.UserId,
                OriginalUrl = url.OriginalUrl,
                ShortUrl = url.ShortUrl,
                NrOfClicks = url.NrOfClicks
            })
            .ToList();

        if(results.Any())
        {
            return Ok(results);
        }

        return NotFound("No matching Urls");
    }
}
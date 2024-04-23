using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using URLShortener.Database;
using URLShortener.ModelHelpers;
using URLShortener.Models;
using URLShortener.Service;

namespace URLShortener.Controllers;


[ApiController]
[Route("api/search")]
[EnableCors]
public class URLSearchController : ControllerBase
{

    private readonly UrlShortenerDbContext _context;

    public URLSearchController(UrlShortenerDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult SearchUrl(string UrlName, string token)
    {

        if (string.IsNullOrEmpty(UrlName))
        {
            return BadRequest("Please type something...");
        }

        int userId = Authentication.GetUserIdFromToken(token);

        // Search for URLs belonging to the authenticated user
        var results = _context.Urls
            .Where(url => url.UserId == userId && url.OriginalUrl.Contains(UrlName.ToLower()))
            .ToList()
            .Select(url => new UrlResponseDto()
            {
                UserId = url.UserId,
                OriginalUrl = url.OriginalUrl,
                ShortUrl = url.ShortUrl,
                Description = url.Description
            })
            .ToList();

        if (results.Any())
        {
            return Ok(results);
        }

        return NotFound("No matching Urls");
    }
}
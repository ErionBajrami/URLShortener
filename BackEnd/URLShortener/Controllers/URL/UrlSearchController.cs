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
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Token is missing");
        }

        // Verify the token
        var principal = TokenService.VerifyToken(token);
        if (principal == null)
        {
            return Unauthorized("Invalid token");
        }

        if (string.IsNullOrEmpty(UrlName))
        {
            return BadRequest("Please type something...");
        }

        // Extract the user ID from the token
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token");
        }
        var userId = int.Parse(userIdClaim.Value);

        // Search for URLs belonging to the authenticated user
        var results = _context.Urls
            .Where(url => url.UserId == userId && url.OriginalUrl.Contains(UrlName.ToLower()))
            .ToList()
            .Select(url => new UrlResponseDto()
            {
                UserId = url.UserId,
                OriginalUrl = url.OriginalUrl,
                ShortUrl = url.ShortUrl
            })
            .ToList();

        if (results.Any())
        {
            return Ok(results);
        }

        return NotFound("No matching Urls");
    }


    /**
    [HttpGet(Name="AdminSearch")]
    public IActionResult SearchUrlByAdmin(string UrlName)
    {


        //if (string.IsNullOrEmpty(token))
        //{
        //    return Unauthorized("Token is missing");
        //}

        //// Verify the token
        ///
        var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var principal = TokenService.VerifyToken(token);
        if (principal == null)
        {
            return Unauthorized("Invalid token");
        }





        if (string.IsNullOrEmpty(UrlName))
        {
            return BadRequest("Please type something...");
        }

        var userId = principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value; //




        var results = _context.Urls
            .Where(url => url.OriginalUrl.Contains(UrlName.ToLower()))
            .ToList()
            .Select(url => new UrlResponseDto()
            {
                UserId = url.UserId,
                OriginalUrl = url.OriginalUrl,
                ShortUrl = url.ShortUrl,
                NrOfClicks = url.NrOfClicks
            })
            .ToList();

        if (results.Any())
        {
            return Ok(results);
        }

        return NotFound("No matching Urls");
    } */
}
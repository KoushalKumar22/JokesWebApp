using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using JokesWebApp.Models;

public static class AuthorizationExtensions
{
    public static bool CanEditJoke(this ClaimsPrincipal user, Joke joke)
    {
        if(! user.Identity?.IsAuthenticated ?? false)
        return false;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        return joke.CreatorId == userId;
    }
    public static bool CanDeleteJoke(this ClaimsPrincipal user, Joke joke)
    {
        if(! user.Identity?.IsAuthenticated ?? false)
        return false;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        return joke.CreatorId == userId || user.IsInRole("Admin");
    }
}
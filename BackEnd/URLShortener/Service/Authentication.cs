using System.Security.Claims;

namespace URLShortener.Service
{
    public static class Authentication
    {
        public static int GetUserIdFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return -1;
            }

            // Verify the token
            var principal = TokenService.VerifyToken(token);
            if (principal == null)
            {
                return -1;
            }

            // Extract the user ID from the token
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return -1;
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return -1;
            }

            return userId;
        }
    }
}


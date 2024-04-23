using System;
using System.Security.Claims;
using URLShortener.Service;

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

        public static bool IsAdminFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Verify the token
            var principal = TokenService.VerifyToken(token);
            if (principal == null)
            {
                return false;
            }

            // Extract the isAdmin claim from the token
            var isAdminClaim = principal.FindFirst("IsAdmin");
            if (isAdminClaim == null)
            {
                return false;
            }

            if (!bool.TryParse(isAdminClaim.Value, out bool isAdmin))
            {
                return false;
            }

            return isAdmin;
        }
    }
}


namespace P7CreateRestApi.Models
{
    public class TokenRefresh
    {
        /// <summary>
        /// Le token JWT expiré.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Le refresh token utilisé pour renouveler le JWT.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}

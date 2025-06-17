namespace booklend.Application.Services.Token
{
    public class JwtSettings
    {
        public string Key { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int ExpiresInDays { get; init; }
    }
}
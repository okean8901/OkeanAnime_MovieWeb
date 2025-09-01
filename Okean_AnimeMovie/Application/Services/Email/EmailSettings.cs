namespace Okean_AnimeMovie.Application.Services.Email
{
    public class EmailSettings
    {
        public string From { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}



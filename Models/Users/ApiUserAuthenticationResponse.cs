namespace Backend_Task.Models.Users
{
    public class ApiUserAuthenticationResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

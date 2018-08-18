namespace vyger.Core.Models
{
    public class AuthenticationToken
    {
        public string Email { get; set; }
        public string EmailVerified { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Locale { get; set; }
        public string Token { get; set; }
    }
}

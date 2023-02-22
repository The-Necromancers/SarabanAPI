namespace JWTAuthentication.Models.ConsoleCreateUser
{
    public class ConsoleCreateUserRq
    {
        public string requestId { get; set; }
        public string reqType { get; set; }
        public string appID { get; set; }
        public RqDetail empProfile { get; set; }
    }
}

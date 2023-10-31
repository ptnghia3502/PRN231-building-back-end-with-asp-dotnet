namespace eBookStore.Client
{
    public class StaticDetails
    {
        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public const string SERVICE_BASE_URL = "https://localhost:7211/api";
        public const string TOKEN_COOKIE = "JWTToken";
        public enum State
        {
            Sucess,
            Error
        }

    }
}

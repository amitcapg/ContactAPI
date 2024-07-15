namespace WebApplication11
{
    public class TypedCustomClient
    {
        private readonly HttpClient httpClient;

        public TypedCustomClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

       
    }
}

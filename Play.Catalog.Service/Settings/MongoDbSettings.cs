namespace Play.Catalog.Service.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }

        public string Password { get; init; }
        public string ConnectionString => $"mongodb+srv://{Host}:{Password}@cluster0.poygm.mongodb.net/?retryWrites=true&w=majority";
    }
}
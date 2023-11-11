using AllProject.Server.Services.Interfaces;

namespace AllProject.Server.Services.Concreates
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
    }
}

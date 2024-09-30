namespace Restaurants.Infrastructure.Configuration
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; }

        public string LogosContainerName { get; set; }
    }
}

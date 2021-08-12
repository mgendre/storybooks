using Microsoft.EntityFrameworkCore;
using StoryBooks.Models;

namespace StoryBooks.Api.Infra.CosmosDb
{
    public class CosmosDbContext: DbContext
    {
        private readonly CosmosDbSettings _cosmosDbSettings;

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Story> Stories { get; set; }

        public CosmosDbContext(CosmosDbSettings cosmosDbSettings)
        {
            _cosmosDbSettings = cosmosDbSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  
        {     
            var accountEndpoint = _cosmosDbSettings.EndpointUrl;  
            var accountKey = _cosmosDbSettings.PrimaryKey;  
            var dbName = _cosmosDbSettings.DatabaseName;  
            optionsBuilder.UseCosmos(accountEndpoint, accountKey, dbName);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);
            builder.ConfigureCosmosEntities();
        }
    }
}
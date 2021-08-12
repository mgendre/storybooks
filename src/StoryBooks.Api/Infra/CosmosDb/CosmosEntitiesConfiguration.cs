using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryBooks.Models;

namespace StoryBooks.Api.Infra.CosmosDb
{
    public static class EntitiesConfiguration
    {

        public static void ConfigureCosmosEntities(this ModelBuilder mb)
        {
            mb.ApplyConfiguration(new CampaignEntityConfiguration());
        }

        private class CampaignEntityConfiguration:IEntityTypeConfiguration<Campaign>  
        {   
            public void Configure(EntityTypeBuilder<Campaign> builder)
            {
                builder.ToContainer(nameof(Campaign));
                builder.HasKey(x => x.Id);  
                builder.HasPartitionKey(x => x.PartitionKey);
                builder.OwnsMany(x => x.Stories);
            }  
        } 
    }
}
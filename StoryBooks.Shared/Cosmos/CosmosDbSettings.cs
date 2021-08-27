namespace StoryBooks.Shared.Cosmos
{
    public class CosmosDbSettings
    {
        /// <summary>
        ///     CosmosDb Account - The Azure Cosmos DB endpoint
        /// </summary>
        public string EndpointUrl { get; set; } = string.Empty;
        /// <summary>
        ///     Key - The primary key for the Azure DocumentDB account.
        /// </summary>
        public string PrimaryKey { get; set; } = string.Empty;
        /// <summary>
        ///     Database name
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;

    }
}
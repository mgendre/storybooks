using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Models;

namespace StoryBooks.Api.Repository
{
    public class ScenarioRepository : AbstractCosmosRepository<Scenario>, IScenarioRepository
    {
        public ScenarioRepository(ICosmosContainer container) : base(container.Container)
        {
        }
    }
}

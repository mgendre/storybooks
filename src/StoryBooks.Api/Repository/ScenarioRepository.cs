using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Models;
using StoryBooks.Shared.Repository;

namespace StoryBooks.Api.Repository
{
    public class ScenarioRepository : AbstractCosmosRepository<Scenario>, IScenarioRepository
    {
        public ScenarioRepository(ScenarioContainer container) : base(container.Container)
        {
        }
    }
}

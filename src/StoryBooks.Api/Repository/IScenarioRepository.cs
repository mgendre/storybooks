using StoryBooks.Models;
using StoryBooks.Shared.Repository;

namespace StoryBooks.Api.Repository
{
    public interface IScenarioRepository : ICosmosRepository<Scenario>
    {
    }
}
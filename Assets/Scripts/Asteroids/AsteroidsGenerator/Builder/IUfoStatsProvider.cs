using Asteroids.Stats;

namespace Asteroids.AsteroidsGenerator.Builder
{
    public interface IUfoStatsProvider
    {
        UfoStats Stats { get; }
    }
}
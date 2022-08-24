using Asteroids.AsteroidsGenerator.Builder;

namespace Asteroids.Stats.Decorators
{
    //переделать на generic
    public abstract class AsteroidsStatsDecorator : IAsteroidsStatsProvider
    {
        protected readonly IAsteroidsStatsProvider BallStatsProvider;

        protected AsteroidsStatsDecorator(IAsteroidsStatsProvider ballStatsProvider)
        {
            BallStatsProvider = ballStatsProvider;
        }

        public AsteroidsStats Stats => GetStatsInternal();

        protected abstract AsteroidsStats GetStatsInternal();
    }
    
    public abstract class UfoStatsDecorator : IUfoStatsProvider
    {
        protected readonly IUfoStatsProvider UfoStatsProvider;

        protected UfoStatsDecorator(IUfoStatsProvider ufoStatsProvider)
        {
            UfoStatsProvider = ufoStatsProvider;
        }

        public UfoStats Stats => GetStatsInternal();

        protected abstract UfoStats GetStatsInternal();
    }
}
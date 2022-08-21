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
}
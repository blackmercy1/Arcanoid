namespace Asteroids.Stats.Decorators
{
    public class EmptyAsteroidDecorator : AsteroidsStatsDecorator
    {
        public EmptyAsteroidDecorator(IAsteroidsStatsProvider ballStatsProvider) : base(ballStatsProvider)
        {
        }

        protected override AsteroidsStats GetStatsInternal()
        {
            return BallStatsProvider.Stats;
        }
    }
}
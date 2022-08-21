namespace MainPlayer.Stats.Decorator
{
    public abstract class PlayerStatsDecorator
    {
        protected readonly IPlayerStatsDecorator BallStatsProvider;

        protected PlayerStatsDecorator(IPlayerStatsDecorator ballStatsProvider)
        {
            BallStatsProvider = ballStatsProvider;
        }

        public PlayerStats Stats => GetStatsInternal();

        protected abstract PlayerStats GetStatsInternal();
    }
}
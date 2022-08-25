namespace Ufos.UfoGenerator.Stats.Decorators
{
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
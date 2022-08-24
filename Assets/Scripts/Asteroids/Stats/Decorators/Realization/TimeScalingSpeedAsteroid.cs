using Asteroids.AsteroidsGenerator.Builder;
using Common;
using UnityEngine;

namespace Asteroids.Stats.Decorators.Realization
{
    public class TimeScalingSpeedAsteroid : AsteroidsStatsDecorator
    {
        private readonly AnimationCurve _curve;
        private readonly StopWatch _stopWatch;

        public TimeScalingSpeedAsteroid(IAsteroidsStatsProvider ballStatsProvider, AnimationCurve curve, StopWatch stopWatch) : base(ballStatsProvider)
        {
            _curve = curve;
            _stopWatch = stopWatch;
        }

        protected override AsteroidsStats GetStatsInternal()
        {
            var speed = _curve.Evaluate(_stopWatch.PassedTimeInSeconds);
            return BallStatsProvider.Stats + new AsteroidsStats(speed);
        }
    }

    public class TimeScalingSpeedUfo : UfoStatsDecorator
    {
        private readonly AnimationCurve _curve;
        private readonly StopWatch _stopWatch;
        
        public TimeScalingSpeedUfo(IUfoStatsProvider ufoStatsProvider, AnimationCurve curve, StopWatch stopWatch) : base(ufoStatsProvider)
        {
            _curve = curve;
            _stopWatch = stopWatch;
        }

        protected override UfoStats GetStatsInternal()
        {
            var speed = _curve.Evaluate(_stopWatch.PassedTimeInSeconds);
            return UfoStatsProvider.Stats + new UfoStats(speed);
        }
    }
}
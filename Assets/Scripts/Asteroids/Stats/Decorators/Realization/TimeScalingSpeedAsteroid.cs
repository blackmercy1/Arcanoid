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
}
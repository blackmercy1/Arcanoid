using Common;
using UnityEngine;

namespace Ufos.UfoGenerator.Stats.Decorators.Realization
{
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
using System;
using Ranges;
using UnityEngine;

namespace Ufos.UfoGenerator.Stats
{
    [Serializable]
    public class UfoStats
    {
        [SerializeField] private IntRange _killPoints;
        [SerializeField] private IntRange _damage;
        [SerializeField] private IntRange _hitPoints;
        [SerializeField] private FloatRange _speed;

        public IntRange KillPoints => _killPoints;
        public IntRange Damage => _damage;
        public IntRange HitPoints => _hitPoints;
        public FloatRange Speed => _speed;

        public UfoStats(IntRange killPoints, IntRange damage, IntRange hitPoints, FloatRange speed)
        {
            _killPoints = killPoints;
            _damage = damage;
            _hitPoints = hitPoints;
            _speed = speed;
        }

        public UfoStats(float speed)
        {
            _speed = new FloatRange(speed, speed);
        }

        public static UfoStats operator +(UfoStats ufoStats, UfoStats ufoStats1)
        {
            var speed = ufoStats.Speed + ufoStats1.Speed;
            var damage = ufoStats.Damage + ufoStats1.Damage;
            var hitPoints = ufoStats.HitPoints + ufoStats1.HitPoints;
            var killPoints = ufoStats.KillPoints + ufoStats1.KillPoints;

            return new UfoStats(killPoints, damage, hitPoints, speed);
        }
    }
}
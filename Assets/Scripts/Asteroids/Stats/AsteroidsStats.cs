using System;
using Asteroids.Ranges;
using UnityEngine;

namespace Asteroids.Stats
{
    [Serializable]
    public class AsteroidsStats
    {
        [SerializeField] private IntRange _killPoints;
        [SerializeField] private IntRange _damage;
        [SerializeField] private IntRange _hitPoints;
        [SerializeField] private FloatRange _speed;

        public IntRange KillPoints => _killPoints;
        public IntRange Damage => _damage;
        public IntRange HitPoints => _hitPoints;
        public FloatRange Speed => _speed;

        public AsteroidsStats(IntRange killPoints, IntRange damage, IntRange hitPoints, FloatRange speed)
        {
            _killPoints = killPoints;
            _damage = damage;
            _hitPoints = hitPoints;
            _speed = speed;
        }

        public AsteroidsStats(float speed)
        {
            _speed = new FloatRange(speed, speed);
        }

        public static AsteroidsStats operator +(AsteroidsStats ballStats1, AsteroidsStats ballStats2)
        {
            var speed = ballStats1.Speed + ballStats2.Speed;
            var damage = ballStats1.Damage + ballStats2.Damage;
            var hitPoints = ballStats1.HitPoints + ballStats2.HitPoints;
            var killPoints = ballStats1.KillPoints + ballStats2.KillPoints;

            return new AsteroidsStats(killPoints, damage, hitPoints, speed);
        }
    }
}
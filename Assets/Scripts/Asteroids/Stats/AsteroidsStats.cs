using System;
using Ranges;
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

        public static AsteroidsStats operator +(AsteroidsStats asteroidStats1, AsteroidsStats asteroidStats2)
        {
            var speed = asteroidStats1.Speed + asteroidStats2.Speed;
            var damage = asteroidStats1.Damage + asteroidStats2.Damage;
            var hitPoints = asteroidStats1.HitPoints + asteroidStats2.HitPoints;
            var killPoints = asteroidStats1.KillPoints + asteroidStats2.KillPoints;

            return new AsteroidsStats(killPoints, damage, hitPoints, speed);
        }
    }
}